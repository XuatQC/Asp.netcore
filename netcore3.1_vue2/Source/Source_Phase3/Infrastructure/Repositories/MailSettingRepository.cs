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

	public class MailSettingRepository : BaseRepository, IMailSettingRepository
	{
		public MailSettingRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		#region MailSettingを追加する
		/// <summary>
		/// MailSettingを追加する
		/// </summary>
		/// <param name="mailSetting"></param>
		/// <returns></returns>
		public async Task<long> AddAsync(MailSetting mailSetting)
		{
			string sql = $@"INSERT INTO mailsetting (
													MailFrom
													, MailFromName
													, MailBounceAccessKeyID
													, MailBounceSecretAccessKey
													, MailBounceQueueUrl
													, SmtpServer
													, SmtpAccount
													, SmtpPass
													, BussinessCd
													, CreateUserCd
													, UpdateUserCd
													)
											VALUES (
													  '{ mailSetting.MailFrom }'
													, '{ mailSetting.MailFromName }'
													, '{ mailSetting.MailBounceAccessKeyID }'
													, '{ mailSetting.MailBounceSecretAccessKey }'
													, '{ mailSetting.MailBounceQueueUrl }'
													, '{ mailSetting.SmtpServer }'
													, '{ mailSetting.SmtpAccount }'
													, '{ mailSetting.SmtpPass }'
													, '{ mailSetting.BussinessCd }'
													, '{ mailSetting.UpdateUserCd }'
													, '{ mailSetting.UpdateUserCd }'
													);";
			try
			{
				return (long)this.DbConn.ExecuteAsync(sql).Result;
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

		public Task<bool> DeleteAsync(List<MailSetting> mailSettings)
		{
			throw new NotImplementedException();
		}

		#region 条件によるMailSetting一覧を取得する
		/// <summary>
		/// 条件によるMailSetting一覧を取得する
		/// </summary>
		/// <param name="mailsetting"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MailSetting>> GetAllAsync(MailSetting mailsetting, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mailsettingParam = new ExpandoObject();

				if (!string.IsNullOrEmpty(mailsetting.BussinessCd))
				{
					condition.Append(" AND BussinessCd = @BussinessCd");
					mailsettingParam.BussinessCd = mailsetting.BussinessCd;
				}

				condition.Append($" AND DelFlg = {ParamConst.DelFlg.OFF}");

				if (!string.IsNullOrEmpty(sortColumnName))
				{
					condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT
										MailFrom
										, MailFromName
										, MailTo
										, MailBounceAccessKeyID
										, MailBounceSecretAccessKey
										, MailBounceQueueUrl
										, SmtpServer
										, SmtpAccount
										, SmtpPass
										, BussinessCd
										, CreateUserCd
										, CreateDtTm
										, UpdateUserCd
										, UpdateDtTm
										, DelFlg
									FROM mailsetting
									WHERE 1=1
									{condition}");
				return (List<MailSetting>)await this.DbConn.QueryAsync<MailSetting>(sqlGetData.ToString(), (object)mailsettingParam);
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

		public Tuple<List<MailSetting>, int> GetDataPagination(int currentpage, int pageSize, MailSetting mailSetting, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		#region MailSettingを更新する
		/// <summary>
		/// MailSettingを更新する
		/// </summary>
		/// <param name="mailSettings"></param>
		/// <returns></returns>
		public async Task<bool> UpdateAsync(List<MailSetting> mailSettings)
		{
			try
			{
				return await this.DbConn.UpdateAsync(mailSettings);
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