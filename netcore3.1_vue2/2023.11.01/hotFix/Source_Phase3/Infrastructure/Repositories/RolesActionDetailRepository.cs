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
	public class RolesActionDetailRepository : BaseRepository, IRolesActionDetailRepository
	{
		public RolesActionDetailRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		public Task<long> AddAsync(RolesActionDetail rolesdetail)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<RolesActionDetail> rolesdetail)
		{
			throw new NotImplementedException();
		}

		#region 条件によるRolesActionDetail一覧を取得する
		/// <summary>
		/// 条件によるRolesActionDetail一覧を取得する
		/// </summary>
		/// <param name="roleActionsDetail"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<RolesActionDetail>> GetAllAsync(RolesActionDetail roleActionsDetail, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic rolesdetailParam = new ExpandoObject();


				if (roleActionsDetail.RoleActDtlId > 0)
				{
					condition.Append(" AND RoleDtlId = @RoleDtlId");
					rolesdetailParam.RoleDtlId = roleActionsDetail.RoleActDtlId;
				}
				
				if (!string.IsNullOrEmpty(roleActionsDetail.ActionType))
				{
					condition.Append(" AND ActionType = @ActionType");
					rolesdetailParam.ActionType = roleActionsDetail.ActionType;
				}
				if (!string.IsNullOrEmpty(roleActionsDetail.ActionUrl))
				{
					condition.Append(" AND ActionUrl = @ActionUrl");
					rolesdetailParam.ActionUrl = roleActionsDetail.ActionUrl;
				}

				condition.Append($" AND DelFlg = {ParamConst.DelFlg.OFF}");

				if (!string.IsNullOrEmpty(sortColumnName))
				{
					if (!string.IsNullOrEmpty(sortType))
					{
						sortType = " ASC";
					}
					else
					{
						sortType = " DESC";
					}
					condition.Append(" order by " + sortColumnName + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT	
										RoleActDtlId
										,ActionType
										,ActionUrl
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM rolesactiondetail
									WHERE 1=1
									{condition}");
				return (List<RolesActionDetail>)await this.DbConn.QueryAsync<RolesActionDetail>(sqlGetData.ToString(), (object)rolesdetailParam);
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

		public Tuple<List<RolesActionDetail>, int> GetDataPagination(int currentpage, int pageSize, RolesActionDetail rolesdetail, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<RolesActionDetail> entity)
		{
			throw new NotImplementedException();
		}
	}
}