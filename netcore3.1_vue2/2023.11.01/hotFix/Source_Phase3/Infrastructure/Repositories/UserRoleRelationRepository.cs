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
	public class UserRoleRelationRepository : BaseRepository, IUserRoleRelationRepository
	{
		public UserRoleRelationRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		public async Task<long> AddAsync(UserRoleRelation entity)
		{
			try
			{
				return await this.DbConn.InsertAsync(entity);
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

		public async Task<long> AddListAsync(List<UserRoleRelation> userRoleRelations)
		{
			try
			{
				return await this.DbConn.InsertAsync(userRoleRelations);
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

		public Task<bool> DeleteAsync(List<UserRoleRelation> entity)
		{
			throw new NotImplementedException();
		}

		#region Get list Userrolerelation with param
		/// <summary>
		/// Get list Userrolerelation with param
		/// </summary>
		/// <param name="userrolerelation"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<UserRoleRelation>> GetAllAsync(UserRoleRelation userrolerelation, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic userrolerelationParam = new ExpandoObject();
				

				if (userrolerelation.UserRoleRltId > 0)
				{
					condition.Append(" AND UserRoleRltId = @UserRoleRltId");
					userrolerelationParam.UserRoleRltId = userrolerelation.UserRoleRltId;
				}
				
				if (userrolerelation.RoleId > 0)
				{
					condition.Append(" AND RoleId = @RoleId");
					userrolerelationParam.RoleId = userrolerelation.RoleId;
				}
				
				if (!string.IsNullOrEmpty(userrolerelation.UserCd))
				{
					condition.Append(" AND UserCd = @UserCd");
					userrolerelationParam.UserCd = userrolerelation.UserCd;
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
					condition.Append(" order by " + sortColumnName + sortType);
				}

				sqlGetData.Append(@$"SELECT	 
											UserRoleRltId
											,RoleId
											,UserCd
											,CreateUserCd
											,CreateDtTm
											,UpdateUserCd
											,UpdateDtTm
											,DelFlg
											FROM userrolerelation
											WHERE 1=1
											{condition}");
				return (List<UserRoleRelation>)await this.DbConn.QueryAsync<UserRoleRelation>(sqlGetData.ToString(), (object)userrolerelationParam);
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

		public Tuple<List<UserRoleRelation>, int> GetDataPagination(int currentpage, int pageSize, UserRoleRelation entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> UpdateAsync(List<UserRoleRelation> entity)
		{
			try
			{
				return await this.DbConn.UpdateAsync(entity);
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
	}
}