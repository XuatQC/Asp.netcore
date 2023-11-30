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
	public class RolesRepository : BaseRepository, IRolesRepository
	{
		public RolesRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		#region Rolesを追加する
		/// <summary>
		/// Rolesを追加する
		/// </summary>
		/// <param name="roles"></param>
		/// <returns></returns>
		public async Task<long> AddAsync(Roles roles)
		{
			try
			{
				return await this.DbConn.InsertAsync(roles);
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

		public Task<bool> DeleteAsync(List<Roles> entity)
		{
			throw new NotImplementedException();
		}

		#region 条件によるRoles一覧を取得する
		/// <summary>
		/// 条件によるRoles一覧を取得する
		/// </summary>
		/// <param name="roles"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<Roles>> GetAllAsync(Roles roles, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic rolesParam = new ExpandoObject();
				

				if (roles.RoleId > 0)
				{
					condition.Append(" AND RoleId = @RoleId");
					rolesParam.RoleId = roles.RoleId;
				}
				
				if (!string.IsNullOrEmpty(roles.RoleName))
				{
					condition.Append(" AND RoleName = @RoleName");
					rolesParam.RoleName = roles.RoleName;
				}

				condition.Append($" AND DelFlg = {ParamConst.DelFlg.OFF}");

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
										RoleId
										,RoleName
										,RoleDescript
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM roles
									WHERE 1=1
									{condition}");
				return (List<Roles>)await this.DbConn.QueryAsync<Roles>(sqlGetData.ToString(), (object)rolesParam);
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

		#region 条件によるRoles一覧を取得する
		/// <summary>
		/// 条件によるRoles一覧を取得する
		/// </summary>
		/// <param name="rolesSub"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<RolesSub>> GetAllSubAsync(RolesSub rolesSub, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic rolesParam = new ExpandoObject();

				if (rolesSub.RoleId > 0)
				{
					condition.Append(" AND r.RoleId = @RoleId");
					rolesParam.RoleId = rolesSub.RoleId;
				}
				condition.Append($" AND r.DelFlg = {ParamConst.DelFlg.OFF}");

				if (!string.IsNullOrEmpty(sortColumnName))
				{
					condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
				}
				condition.Append(" GROUP BY r.RoleId");

				sqlGetData.Append(@$"SELECT
										r.RoleId
										, r.RoleName
										, r.RoleDescript
										, r.CreateUserCd
										, r.UpdateDtTm
										, GROUP_CONCAT(rdt.ActionType) AS ActionType
									FROM roles r
									INNER JOIN rolesdetail rdt
										ON rdt.RoleId = r.RoleId
										AND rdt.DelFlg = { ParamConst.DelFlg.OFF }
									WHERE 1=1
									{condition}");
				return (List<RolesSub>)await this.DbConn.QueryAsync<RolesSub>(sqlGetData.ToString(), (object)rolesParam);
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

		public Tuple<List<Roles>, int> GetDataPagination(int currentpage, int pageSize, Roles entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		#region Rolesを更新する
		/// <summary>
		/// Rolesを更新する
		/// </summary>
		/// <param name="roles"></param>
		/// <returns></returns>
		public async Task<bool> UpdateAsync(List<Roles> roles)
		{
			try
			{
				return await this.DbConn.UpdateAsync(roles);
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
	}
}