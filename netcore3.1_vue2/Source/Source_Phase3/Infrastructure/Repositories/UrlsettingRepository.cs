using Dapper;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Infrastructure.Repositories
{

	public class UrlsettingRepository : BaseRepository, IUrlsettingRepository
	{
		public UrlsettingRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		public Task<long> AddAsync(UrlSetting entity)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<UrlSetting> entity)
		{
			throw new NotImplementedException();
		}

		#region 条件によるURL一覧を取得する
		/// <summary>
		/// 条件によるURL一覧を取得する
		/// </summary>
		/// <param name="urlsetting"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<UrlSetting>> GetAllAsync(UrlSetting urlsetting, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic urlsettingParam = new ExpandoObject();

				if (!string.IsNullOrEmpty(urlsetting.Url))
				{
					condition.Append(" AND Url = @Url");
					urlsettingParam.Url = urlsetting.Url;
				}

				if (!string.IsNullOrEmpty(urlsetting.BussinessCd))
				{
					condition.Append(" AND BussinessCd = @BussinessCd");
					urlsettingParam.BussinessCd = urlsetting.BussinessCd;
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
					condition.Append(" order by " + sortColumnName + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT	 
										Url
										,BussinessCd
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM Urlsetting
									WHERE 1=1
									{condition}");
					return (List<UrlSetting>)await this.DbConn.QueryAsync<UrlSetting>(sqlGetData.ToString(), (object)urlsettingParam);
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

		public Tuple<List<UrlSetting>, int> GetDataPagination(int currentpage, int pageSize, UrlSetting entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<UrlSetting> entity)
		{
			throw new NotImplementedException();
		}
		#region URLを保存、変更する
		/// <summary>
		/// URLを保存、変更する
		/// </summary>
		/// <param name="urlsettings"></param>
		/// <returns></returns>
		public bool AddOrUpdate(UrlSetting urlsettings)
		{
			try
			{
				string sql =  @$"INSERT INTO urlsetting (Url, BussinessCd, CreateUserCd) 
									VALUES ('{urlsettings.Url}', {urlsettings.BussinessCd}, '{urlsettings.CreateUserCd}')
										ON DUPLICATE KEY UPDATE Url = '{urlsettings.Url}', UpdateUserCd = '{urlsettings.UpdateUserCd}'";

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
		#endregion

		#region 存在をチェックする
		/// <summary>
		/// 存在をチェックする
		/// </summary>
		/// <param name="urlsettings"></param>
		/// <returns></returns>
		public bool IsDuplicateUrl(UrlSetting urlsettings)
		{
			try
			{
				string sql = @$"SELECT count(1) FROM urlsetting WHERE Url = @Url AND BussinessCd != @BussinessCd AND DelFlg = {ParamConst.DelFlg.OFF}";

				return this.DbConn.QueryFirstOrDefault<int>(sql, new { Url = urlsettings.Url, BussinessCd = urlsettings.BussinessCd }) > 0;
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
