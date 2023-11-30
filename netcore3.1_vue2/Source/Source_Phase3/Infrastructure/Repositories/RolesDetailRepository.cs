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
	public class RolesDetailRepository : BaseRepository, IRolesDetailRepository
	{
		public RolesDetailRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		public Task<long> AddAsync(RolesDetail rolesdetail)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<RolesDetail> rolesdetail)
		{
			throw new NotImplementedException();
		}

		#region 条件によるRolesDetail一覧を取得する
		/// <summary>
		/// 条件によるRolesDetail一覧を取得する
		/// </summary>
		/// <param name="rolesdetail"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<RolesDetail>> GetAllAsync(RolesDetail rolesdetail, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic rolesdetailParam = new ExpandoObject();


				if (rolesdetail.RoleDtlId > 0)
				{
					condition.Append(" AND RoleDtlId = @RoleDtlId");
					rolesdetailParam.RoleDtlId = rolesdetail.RoleDtlId;
				}

				if (rolesdetail.RoleId > 0)
				{
					condition.Append(" AND RoleId = @RoleId");
					rolesdetailParam.RoleId = rolesdetail.RoleId;
				}

				if (!string.IsNullOrEmpty(rolesdetail.ActionType))
				{
					condition.Append(" AND ActionType = @ActionType");
					rolesdetailParam.ActionType = rolesdetail.ActionType;
				}

				if (!rolesdetail.DelFlg)
				{
					condition.Append($" AND DelFlg = {ParamConst.DelFlg.OFF}");
				}

				if (!string.IsNullOrEmpty(sortColumnName))
				{
					if (!string.IsNullOrEmpty(sortType))
					{
						sortType = "ASC";
					}
					else
					{
						sortType = "DESC";
					}
					condition.Append(" order by " + sortColumnName + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT	
										RoleDtlId
										,RoleId
										,ActionType
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM Rolesdetail
									WHERE 1=1
									{condition}");
				return (List<RolesDetail>)await this.DbConn.QueryAsync<RolesDetail>(sqlGetData.ToString(), (object)rolesdetailParam);
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
		#endregion

		public Tuple<List<RolesDetail>, int> GetDataPagination(int currentpage, int pageSize, RolesDetail rolesdetail, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		#region RolesDetailを更新する
		/// <summary>
		/// RolesDetailを更新する
		/// </summary>
		/// <param name="rolesDetails"></param>
		/// <returns></returns>
		public async Task<bool> UpdateAsync(List<RolesDetail> rolesDetails)
		{
			try
			{
				return await this.DbConn.UpdateAsync(rolesDetails);
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return false;
			}
			finally
			{
				this.DbConn.Close();
			}
		}
		#endregion

		#region RolesDetailを追加する
		/// <summary>
		/// RolesDetailを追加する
		/// </summary>
		/// <param name="rolesDetails"></param>
		/// <returns></returns>
		public async Task<long> AddListAsync(List<RolesDetail> rolesDetails)
		{
			try
			{
				return await this.DbConn.InsertAsync(rolesDetails);
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