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

	public class DatetimeSettingRepository : BaseRepository, IDatetimeSettingRepository
	{
		public DatetimeSettingRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		public Task<long> AddAsync(DatetimeSetting datetimesetting)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<DatetimeSetting> datetimesetting)
		{
			throw new NotImplementedException();
		}

		#region ğŒ‚É‚æ‚é—\–ñ“úİ’èˆê——‚ğæ“¾‚·‚é(async)
		/// <summary>
		/// ğŒ‚É‚æ‚é—\–ñ“úİ’èˆê——‚ğæ“¾‚·‚é(async)
		/// </summary>
		/// <param name="datetimesetting"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<DatetimeSetting>> GetAllAsync(DatetimeSetting datetimesetting, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic datetimesettingParam = new ExpandoObject();

				if (!string.IsNullOrEmpty(datetimesetting.BussinessCd))
				{
					condition.Append(" AND BussinessCd = @BussinessCd");
					datetimesettingParam.BussinessCd = datetimesetting.BussinessCd;
				}

				if (!string.IsNullOrEmpty(datetimesetting.CreateUserCd))
				{
					condition.Append(" AND CreateUserCd = @CreateUserCd");
					datetimesettingParam.CreateUserCd = datetimesetting.CreateUserCd;
				}
				condition.Append(" AND DelFlg = " + ParamConst.DelFlg.OFF);

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
					condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT	 
										OrdStartDtTm
										,OrdEndDtTm
										,RcvMoneyStartDtTm
										,RcvMoneyEndDtTm
										,BussinessCd
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM Datetimesetting
									WHERE 1=1
									{condition}");
				using (IDbConnection con = this.DbConn)
				{
					return (List<DatetimeSetting>)await this.DbConn.QueryAsync<DatetimeSetting>(sqlGetData.ToString(), (object)datetimesettingParam);
				}
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
		#endregion(async)

		#region ğŒ‚É‚æ‚é—\–ñ“úİ’èˆê——‚ğæ“¾‚·‚é
		/// <summary>
		/// ğŒ‚É‚æ‚é—\–ñ“úİ’èˆê——‚ğæ“¾‚·‚é
		/// </summary>
		/// <param name="datetimesetting"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public List<DatetimeSetting> GetAll(DatetimeSetting datetimesetting, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic datetimesettingParam = new ExpandoObject();

				if (!string.IsNullOrEmpty(datetimesetting.BussinessCd))
				{
					condition.Append(" AND BussinessCd = @BussinessCd");
					datetimesettingParam.BussinessCd = datetimesetting.BussinessCd;
				}

				if (!string.IsNullOrEmpty(datetimesetting.CreateUserCd))
				{
					condition.Append(" AND CreateUserCd = @CreateUserCd");
					datetimesettingParam.CreateUserCd = datetimesetting.CreateUserCd;
				}
				condition.Append(" AND DelFlg = " + ParamConst.DelFlg.OFF);

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
					condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT	 
										OrdStartDtTm
										,OrdEndDtTm
										,RcvMoneyStartDtTm
										,RcvMoneyEndDtTm
										,BussinessCd
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM Datetimesetting
									WHERE 1=1
									{condition}");
				using (IDbConnection con = this.DbConn)
				{
					return (List<DatetimeSetting>) this.DbConn.Query<DatetimeSetting>(sqlGetData.ToString(), (object)datetimesettingParam);
				}
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

		public Tuple<List<DatetimeSetting>, int> GetDataPagination(int currentpage, int pageSize, DatetimeSetting datetimesetting, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// DateTime‚ğXV‚·‚é
		/// </summary>
		/// <param name="datetimesetting"></param>
		/// <returns></returns>
		public async Task<bool> UpdateAsync(List<DatetimeSetting> datetimesetting)
		{
			try
			{
				using (IDbConnection con = this.DbConn)
				{
					return await con.UpdateAsync(datetimesetting);
				}
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return false;
			}
		}

        public bool AddOrUpdate(DatetimeSetting datetimeSetting)
        {
			try
			{
				string sql = @$"INSERT INTO datetimesetting (OrdStartDtTm,OrdEndDtTm,RcvMoneyStartDtTm,RcvMoneyEndDtTm, BussinessCd, CreateUserCd) 
									VALUES ('{datetimeSetting.OrdStartDtTm.ToString("yyyy-MM-dd HH:mm:ss")}',
											'{datetimeSetting.OrdEndDtTm.ToString("yyyy-MM-dd HH:mm:ss")}',
											'{datetimeSetting.RcvMoneyStartDtTm.ToString("yyyy-MM-dd HH:mm:ss")}',
											'{datetimeSetting.RcvMoneyEndDtTm.ToString("yyyy-MM-dd HH:mm:ss")}',
											'{datetimeSetting.BussinessCd}', 
											'{datetimeSetting.CreateUserCd}')
										ON DUPLICATE KEY UPDATE BussinessCd = '{datetimeSetting.BussinessCd}',
																OrdStartDtTm='{datetimeSetting.OrdStartDtTm.ToString("yyyy-MM-dd HH:mm:ss")}',
																OrdEndDtTm='{datetimeSetting.OrdEndDtTm.ToString("yyyy-MM-dd HH:mm:ss")}',
																RcvMoneyStartDtTm='{datetimeSetting.RcvMoneyStartDtTm.ToString("yyyy-MM-dd HH:mm:ss")}',
																RcvMoneyEndDtTm='{datetimeSetting.RcvMoneyEndDtTm.ToString("yyyy-MM-dd HH:mm:ss")}',
																UpdateUserCd = '{datetimeSetting.UpdateUserCd}'";

				this.DbConn.QueryFirstOrDefault<int>(sql);

				return true;
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