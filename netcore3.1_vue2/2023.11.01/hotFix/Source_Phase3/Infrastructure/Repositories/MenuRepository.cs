using Dapper;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Infrastructure.Repositories
{
	public class MenuRepository : BaseRepository, IMenuRepository
	{
		public MenuRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		/// <summary>
		/// Get list Menu with roleID
		/// </summary>
		/// <param name="roleId"></param>
		/// <returns></returns>
		public async Task<List<Menu>> GetMenuByRoleAsync(int roleId)
		{
			try
			{
				StringBuilder sqlGetData = new StringBuilder("");

				sqlGetData.Append(@$"SELECT	 
										 MN.MenuId
										,MN.Title
										,MN.DisplayOrder
										,MN.ParrentId
										,MN.Href
										,MN.Icon
										,MN.CreateUserCd
										,MN.CreateDtTm
										,MN.UpdateUserCd
										,MN.UpdateDtTm
										,MN.DelFlg
									FROM menu MN
									INNER JOIN menurolerelation MNR
									ON MN.MenuId = MNR.MenuId
									AND MNR.DelFlg = {ParamConst.DelFlg.OFF}
									WHERE MNR.RoleId = {roleId}
									AND MN.DelFlg = {ParamConst.DelFlg.OFF}");

				return (List<Menu>) await this.DbConn.QueryAsync<Menu>(sqlGetData.ToString());
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return null;
			}
			finally
			{
				this.DbConn.Close();
			}
		}

		/// <summary>
		/// Get list Roles by user cd
		/// </summary>
		/// <param name="muserLogin"></param>
		/// <param name="menu"></param>
		/// <param name="sortByColumn"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<Menu>> GetMenusByUserAsync(MUserLogin muserLogin, Menu menu, string sortByColumn, string sortType)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic menuParam = new ExpandoObject();

				if (menu.MenuId > 0)
				{
					condition.Append(" AND MN.MenuId = @MenuId");
					menuParam.MenuId = menu.MenuId;
				}
				if (!string.IsNullOrEmpty(menu.Href))
				{
					condition.Append(" AND MN.Href = @Href");
					menuParam.Href = menu.Href;
				}

				if (!string.IsNullOrEmpty(muserLogin.UserCd))
				{
					condition.Append(" AND MU.UserCd = @UserCd");
					menuParam.UserCd = muserLogin.UserCd;
				}
				if (muserLogin.Department > 0)
				{
					// Admin: 1, Shop: 2
					condition.Append(" AND MU.Department = @Department");
					menuParam.Department = muserLogin.Department;
				}
				//if (muserLogin.RoleId > 0)
				//{
				//	condition.Append(" AND MU.RoleId = @RoleId");
				//	menuParam.RoleId = muserLogin.RoleId;
				//}

				condition.Append($" AND MN.DelFlg = { ParamConst.DelFlg.OFF}");	

				if (!string.IsNullOrEmpty(sortByColumn))
				{
					if (!string.IsNullOrEmpty(sortType))
					{
						sortType = "ASC";
					}
					else
					{
						sortType = "DESC";
					}
					condition.Append(" order by " + sortByColumn + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT	 
											MN.MenuId
											,MN.Title
											,MN.DisplayOrder
											,MN.ParrentId
											,MN.Href
											,MN.Icon
											,MN.CreateUserCd
											,MN.CreateDtTm
											,MN.UpdateUserCd
											,MN.UpdateDtTm
											,MN.DelFlg
									FROM menu MN 
									INNER JOIN menurolerelation MNR
										ON MN.MenuId = MNR.MenuId
										AND MNR.DelFlg = {ParamConst.DelFlg.OFF}
									INNER JOIN userrolerelation UR 
										ON MNR.RoleId = UR.RoleId
										AND UR.DelFlg = {ParamConst.DelFlg.OFF}
									INNER JOIN muserlogin MU 
										ON MU.UserCd = UR.UserCd
										AND Mu.DelFlg = {ParamConst.DelFlg.OFF}
									WHERE 1=1
									{condition}");
				using (IDbConnection con = this.DbConn)
				{
					return (List<Menu>)await this.DbConn.QueryAsync<Menu>(sqlGetData.ToString(), (object)menuParam);
				}
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return null ;
			}
			finally
			{
				this.DbConn.Close();
			}
		}

		/// <summary>
		/// èåèÇ…ÇÊÇÈMenuàÍóóÇéÊìæÇ∑ÇÈ
		/// </summary>
		/// <param name="menu"></param>
		/// <param name="propetyOrder"></param>
		/// <param name="typeOrder"></param>
		/// <returns></returns>
		public async Task<List<Menu>> GetAllAsync(Menu menu, string propetyOrder, string typeOrder)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic menuParam = new ExpandoObject();

				if (menu.MenuId > 0)
				{
					condition.Append(" AND MenuId = @MenuId");
					menuParam.MenuId = menu.MenuId;
				}
				if (!string.IsNullOrEmpty(menu.Href))
				{
					condition.Append(" AND Href = @Href");
					menuParam.Href = menu.Href;
				}
				if (menu.Department > 0)
				{
					condition.Append(" AND Department = @Department");
					menuParam.Department = menu.Department;
				}
				menuParam.DelFlg = ParamConst.DelFlg.OFF;

				if (!string.IsNullOrEmpty(propetyOrder))
				{
					if (!string.IsNullOrEmpty(typeOrder))
					{
						typeOrder = "ASC";
					}
					else
					{
						typeOrder = "DESC";
					}
					condition.Append(" order by " + propetyOrder + " " + typeOrder);
				}

				sqlGetData.Append(@$"SELECT	 
											MenuId
											,Title
											,DisplayOrder
											,ParrentId
											,Href
											,Icon
											,CreateUserCd
											,CreateDtTm
											,UpdateUserCd
											,UpdateDtTm
											,DelFlg
									FROM menu
									WHERE 1=1
									{condition}");
				return (List<Menu>)await this.DbConn.QueryAsync<Menu>(sqlGetData.ToString(), (object)menuParam);
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return null;
			}
			finally
			{
				this.DbConn.Close();
			}
		}

		public Tuple<List<Menu>, int> GetDataPagination(int currentpage, int pageSize, Menu entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<long> AddAsync(Menu entity)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<Menu> entity)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<Menu> entity)
		{
			throw new NotImplementedException();
		}
	}
}