using Dapper;
using Dapper.Contrib.Extensions;
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
	public class MenuRoleRelationRepository : BaseRepository, IMenuRoleRelationRepository
	{
		public MenuRoleRelationRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}


		///// <summary>
		///// Get list Roles by user cd
		///// </summary>
		///// <param name="userCd"></param>
		///// <returns>List<string> (roles)</returns>
		//public async Task<bool> IsUserCanAccessMenuAsync(string userCd, string menuUrl)
		//{
		//	try
		//	{
		//		StringBuilder sqlGetData = new StringBuilder("");

		//		if (string.IsNullOrEmpty(userCd) && string.IsNullOrEmpty(menuUrl))
		//		{
		//			return false;
		//		}

		//		sqlGetData.Append(@$"SELECT count(MU.UserCd)
		//									FROM muserlogin MU
		//									JOIN userrolerelation UR ON MU.UserCd = UR.UserCd
		//									JOIN menurolerelation MNR ON ur.RoleId = MNR.RoleId
		//									JOIN menu MN ON MNR.MenuId = MN.MenuId
		//									WHERE MU.UserCd = @UserCd
		//									  AND MN.Href = @MenuUrl
		//									AND MU.DelFlg = {ParamConst.DelFlg.OFF}");

		//		using (IDbConnection con = this.DbConn)
		//		{
		//			return (int)await this.DbConn.QueryAsync<int>(sqlGetData.ToString(), new { UserCd = userCd, MenuUrl = menuUrl });
		//		}
		//	}
		//	catch (Exception e)
		//	{
		//		Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
		//		return false;
		//	}
		//}


		/// <summary>
		/// èåèÇ…ÇÊÇÈMenuRoleRelationàÍóóÇéÊìæÇ∑ÇÈ
		/// </summary>
		/// <param name="menuRoleRelation"></param>
		/// <param name="propetyOrder"></param>
		/// <param name="typeOrder"></param>
		/// <returns></returns>
		public async Task<List<MenuRoleRelation>> GetAllAsync(MenuRoleRelation menuRoleRelation, string propetyOrder, string typeOrder)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic menuRoleRelationParam = new ExpandoObject();


				if (menuRoleRelation.MenuRoleDtlId > 0)
				{
					condition.Append(" AND MenuRoleDtlId = @MenuRoleDtlId");
					menuRoleRelationParam.MenuRoleDtlId = menuRoleRelation.MenuRoleDtlId;
				}

				if (menuRoleRelation.RoleId > 0)
				{
					condition.Append(" AND RoleId = @RoleId");
					menuRoleRelationParam.RoleId = menuRoleRelation.RoleId;
				}

				if (menuRoleRelation.MenuId > 0)
				{
					condition.Append(" AND MenuId = @MenuId");
					menuRoleRelationParam.MenuId = menuRoleRelation.MenuId;
				}

				menuRoleRelationParam.DelFlg = ParamConst.DelFlg.OFF;

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
											MenuRoleDtlId
											,RoleId
											,MenuId
											,CreateUserCd
											,CreateDtTm
											,UpdateUserCd
											,UpdateDtTm
											,DelFlg
									FROM menurolerelation
									WHERE 1=1
									{condition}");
				return (List<MenuRoleRelation>)await this.DbConn.QueryAsync<MenuRoleRelation>(sqlGetData.ToString(), (object)menuRoleRelationParam);
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

		public Tuple<List<MenuRoleRelation>, int> GetDataPagination(int currentpage, int pageSize, MenuRoleRelation data, string sortPropety, string sortType)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MenuRoleRelation> lstMenuRoleRelation)
		{
			throw new NotImplementedException();
		}

		public Task<long> AddAsync(MenuRoleRelation MenuRoleRelation)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<MenuRoleRelation> MenuRoleRelation)
		{
			throw new NotImplementedException();
		}

		#region MenuRoleRelationÇí«â¡Ç∑ÇÈ
		/// <summary>
		/// MenuRoleRelationÇí«â¡Ç∑ÇÈ
		/// </summary>
		/// <param name="menuRoleRelations"></param>
		/// <returns></returns>
		public async Task<long> AddListAsync(List<MenuRoleRelation> menuRoleRelations)
		{
			try
			{
				return await this.DbConn.InsertAsync(menuRoleRelations);
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return 0;
			}
			finally
			{
				this.DbConn.Close();
			}
		}
		#endregion
	}
}