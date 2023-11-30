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

	public class MAdminUserRepository : BaseRepository, IMAdminUserRepository
	{
		public MAdminUserRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		public async Task<long> AddAsync(MAdminUser entity)
		{
			try
			{
				return await this.DbConn.InsertAsync(entity);
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return -1;
			}
			finally
			{
				this.DbConn.Close();
			}
		}

		public async Task<long> AddListAsync(List<MAdminUser> mAdminUsers)
		{
			try
			{
				return await this.DbConn.InsertAsync(mAdminUsers);
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

		#region CSVからユーザーを追加する
		/// <summary>
		/// CSVからユーザーを追加する
		/// </summary>
		/// <param name="sqlQuery"></param>
		/// <returns></returns>
		public async Task<long> InsertOrUpdateDataCSV(string sqlQuery)
		{
			try
			{
				return (long)this.DbConn.ExecuteAsync(sqlQuery).Result;
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

		public Task<bool> DeleteAsync(List<MAdminUser> entity)
		{
			throw new NotImplementedException();
		}

		#region 条件によるMAdminUser一覧を取得する
		/// <summary>
		/// 条件によるMAdminUser一覧を取得する
		/// </summary>
		/// <param name="madminuser"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MAdminUser>> GetAllAsync(MAdminUser madminuser, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic madminuserParam = new ExpandoObject();


				if (!string.IsNullOrEmpty(madminuser.UserCd))
				{
					condition.Append(" AND UserCd = @UserCd");
					madminuserParam.UserCd = madminuser.UserCd;
				}

				if (!string.IsNullOrEmpty(madminuser.UserNameKanji))
				{
					condition.Append(" AND UserNameKanji = @UserNameKanji");
					madminuserParam.UserNameKanji = madminuser.UserNameKanji;
				}

				if (!string.IsNullOrEmpty(madminuser.UserNameKana))
				{
					condition.Append(" AND UserNameKana = @UserNameKana");
					madminuserParam.UserNameKana = madminuser.UserNameKana;
				}

				if (!string.IsNullOrEmpty(madminuser.EmailAddress))
				{
					condition.Append(" AND EmailAddress = @EmailAddress");
					madminuserParam.EmailAddress = madminuser.EmailAddress;
				}

				if (!string.IsNullOrEmpty(madminuser.BussinessCd))
				{
					condition.Append(" AND BussinessCd = @BussinessCd");
					madminuserParam.BussinessCd = madminuser.BussinessCd;
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
										UserCd
										,UserNameKanji
										,UserNameKana
										,EmailAddress
										,BussinessCd
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM madminuser
									WHERE 1=1
									{condition}");
				return (List<MAdminUser>)await this.DbConn.QueryAsync<MAdminUser>(sqlGetData.ToString(), (object)madminuserParam);
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

		public async Task<List<MAdminUserSub>> GetAllSubAsync(MAdminUser mAdminUser, string sortColumnName = null, string sortType = null)
		{
			try
			{

				string sqlGetData = @$"SELECT 
										 madminuser.UserCd
										,UserNameKanji
										,UserNameKana
										,EmailAddress
										,roles.RoleId
										,RoleName
										,Password
										,BussinessCd
										,madminuser.CreateUserCd
										,madminuser.DelFlg
									FROM
										madminuser
											JOIN
										userrolerelation ON userrolerelation.UserCd = madminuser.UserCd
											JOIN
										roles ON roles.RoleId = userrolerelation.RoleId
											AND roles.DelFlg = {ParamConst.DelFlg.OFF}
											JOIN
										muserlogin ON muserlogin.UserCd = madminuser.UserCd
									ORDER BY madminuser.UpdateDtTm DESC;";
				return (List<MAdminUserSub>)await this.DbConn.QueryAsync<MAdminUserSub>(sqlGetData.ToString());
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
		public Tuple<List<MAdminUser>, int> GetDataPagination(int currentpage, int pageSize, MAdminUser entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> UpdateAsync(List<MAdminUser> entity)
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