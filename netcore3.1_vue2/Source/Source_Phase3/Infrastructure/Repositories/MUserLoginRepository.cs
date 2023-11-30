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
	public class MUserLoginRepository : BaseRepository, IMUserLoginRepository
	{
		public MUserLoginRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		public async Task<long> AddAsync(MUserLogin entity)
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

		public Task<bool> DeleteAsync(List<MUserLogin> entity)
		{
			throw new NotImplementedException();
		}

		#region ログインする 
		/// <summary>
		/// ログインする
		/// </summary>
		/// <param name="muserLogin"></param>
		/// <returns></returns>
		public MuserLoginSub Login(string userCd, string password)
		{
			MuserLoginSub muserLoginSub = new MuserLoginSub();
			muserLoginSub.UserCd = userCd;
			muserLoginSub.Password = password;
			return this.GetMuserLoginSub(muserLoginSub);
		}
		#endregion

		#region 条件によるMuserLoginSubを取得する
		/// <summary>
		/// 条件によるMuserLoginSubを取得する
		/// </summary>
		/// <param name="muserLoginSub"></param>
		/// <returns></returns>
		public MuserLoginSub GetMuserLoginSub(MuserLoginSub muserLoginSub)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder joinTable = new StringBuilder("");
				StringBuilder tableSubCols = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");

				dynamic muserLoginParam = new ExpandoObject();

				if (string.IsNullOrEmpty(muserLoginSub.UserCd))
				{
					return null;
				}
				else
				{
					condition.Append(" AND login.UserCd = @UserCd");
					muserLoginParam.UserCd = muserLoginSub.UserCd;
				}

				if (!string.IsNullOrEmpty(muserLoginSub.Password))
				{
					condition.Append(" AND login.Password = @Password");
					muserLoginParam.Password = muserLoginSub.Password;
				}
				
				if (muserLoginSub.Department > 0)
				{
					condition.Append(" AND login.Department = @Department");
					muserLoginParam.Department = muserLoginSub.Department;
				}
				if (!string.IsNullOrEmpty(muserLoginSub.RefreshToken))
				{
					condition.Append(" AND login.RefreshToken = @RefreshToken");
					muserLoginParam.RefreshToken = muserLoginSub.RefreshToken;
				}
				if (muserLoginSub.RoleId > 0)
				{
					condition.Append(" AND r.RoleId = @RoleId");
					muserLoginParam.RoleId = muserLoginSub.RoleId;
				}

				// If is user admin
				if (muserLoginSub.UserCd.Length > 6)
				{
					tableSubCols.Append(@" r.RoleId, 
										group_concat(rd.ActionType) AS Permissions, 
										admin.UserNameKanji AS UserNameKanji,
										admin.UserNameKana AS UserNameKana,
										admin.BussinessCd AS BussinessCd");

					joinTable.Append(@$"
									INNER JOIN madminuser admin
										ON login.UserCd = admin.UserCd
										AND admin.DelFlg = {ParamConst.DelFlg.OFF}
									INNER JOIN userrolerelation ur 
										ON login.UserCd = ur.UserCd
										AND ur.DelFlg = {ParamConst.DelFlg.OFF}
									INNER JOIN roles r 
										ON r.RoleId = ur.RoleId
										AND r.DelFlg = {ParamConst.DelFlg.OFF}
									INNER JOIN rolesdetail rd 
										ON rd.RoleId = r.RoleId
										AND rd.DelFlg = {ParamConst.DelFlg.OFF}
									");
				}
				else
				{
					tableSubCols.Append(@$" 0 as RoleId, 
										'' AS Permissions,
										shop.ShopName AS UserNameKanji,
										'' AS UserNameKana,
										shop.BussinessCd AS BussinessCd");

					joinTable.Append(@$"
									INNER JOIN mshop shop 
										ON login.UserCd = shop.ShopCd
										AND shop.DelFlg = {ParamConst.DelFlg.OFF}
									");
				}

				condition.Append($" AND login.DelFlg = {ParamConst.DelFlg.OFF}");
				condition.Append(" GROUP BY login.UserCd");

				sqlGetData.Append(@$"SELECT login.UserCd ,
											login.Password ,
											login.Department ,
											login.RefreshToken ,
											login.RefreshTokenExpiryTime ,
											login.CreateUserCd ,
											login.CreateDtTm ,
											login.UpdateUserCd ,
											login.UpdateDtTm ,
											login.DelFlg,
											{tableSubCols}
									FROM muserlogin login
									{joinTable}
									WHERE 1=1
									{condition}");
				return this.DbConn.QueryFirst<MuserLoginSub>(sqlGetData.ToString(), (object)muserLoginParam);
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

		#region 条件によるMUserLogin一覧を取得する
		/// <summary>
		/// 条件によるMUserLogin一覧を取得する
		/// </summary>
		/// <param name="muserlogin"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MUserLogin>> GetAllAsync(MUserLogin muserlogin, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic muserloginParam = new ExpandoObject();
				

				if (!string.IsNullOrEmpty(muserlogin.UserCd))
				{
					condition.Append(" AND UserCd = @UserCd");
					muserloginParam.UserCd = muserlogin.UserCd;
				}
				
				if (!string.IsNullOrEmpty(muserlogin.Password))
				{
					condition.Append(" AND Password = @Password");
					muserloginParam.Password = muserlogin.Password;
				}
				
				if (muserlogin.Department > 0)
				{
					condition.Append(" AND Department = @Department");
					muserloginParam.Department = muserlogin.Department;
				}
				
				if (!string.IsNullOrEmpty(muserlogin.RefreshToken))
				{
					condition.Append(" AND RefreshToken = @RefreshToken");
					muserloginParam.RefreshToken = muserlogin.RefreshToken;
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
										,Password
										,Department
										,RefreshToken
										,RefreshTokenExpiryTime
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM Muserlogin
									WHERE 1=1
									{condition}");
				return (List<MUserLogin>)await this.DbConn.QueryAsync<MUserLogin>(sqlGetData.ToString(), (object)muserloginParam);
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

		public Tuple<List<MUserLogin>, int> GetDataPagination(int currentpage, int pageSize, MUserLogin entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> UpdateAsync(List<MUserLogin> entity)
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

		public async Task<long> AddListAsync(List<MUserLogin> userLogins)
		{
			try
			{
				return await this.DbConn.InsertAsync(userLogins);
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
	}
}