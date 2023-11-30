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
	public class FrontScreenImgUrlRepository : BaseRepository, IFrontScreenImgUrlRepository
	{
		public FrontScreenImgUrlRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		public Task<long> AddAsync(FrontScreenImgUrl frontScreenImgUrl)
		{
			throw new NotImplementedException();
		}

        public async Task<long> AddListAsync(List<FrontScreenImgUrl> lstFrontScreenImgUrl)
        {
			long result = 0;
			try
			{
				using (IDbConnection con = this.DbConn)
				{
					result = await con.InsertAsync(lstFrontScreenImgUrl);
					return result;
				}
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return result;
			}
		}

        public Task<bool> DeleteAsync(List<FrontScreenImgUrl> frontScreenImgUrl)
		{
			throw new NotImplementedException();
		}

		#region 条件によるFrontScreenImgUrl一覧を取得する
		/// <summary>
		/// 条件によるFrontScreenImgUrl一覧を取得する
		/// </summary>
		/// <param name="frontScreenImgUrl"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<FrontScreenImgUrl>> GetAllAsync(FrontScreenImgUrl frontScreenImgUrl, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic frontScreenImgUrlParam = new ExpandoObject();

				if (frontScreenImgUrl.ScrImgUrlId > 0)
				{
					condition.Append(" AND ScrImgUrlId = @ScrImgUrlId");
					frontScreenImgUrlParam.ScrType = frontScreenImgUrl.ScrImgUrlId;
				}
				if (frontScreenImgUrl.ScrId > 0)
				{
					condition.Append(" AND ScrId = @ScrId");
					frontScreenImgUrlParam.ScrId = frontScreenImgUrl.ScrId;
				}
				condition.Append($" AND DelFlg = { ParamConst.DelFlg.OFF } ");

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
					condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT 
										ScrImgUrlId
										,ScrId
										,ImageUrl
										,ImagePosition
										,DspIndex
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM frontscreenimgurl
									WHERE 1=1
									{condition}");
				return (List<FrontScreenImgUrl>)await this.DbConn.QueryAsync<FrontScreenImgUrl>(sqlGetData.ToString(), (object)frontScreenImgUrlParam);
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

		public Tuple<List<FrontScreenImgUrl>, int> GetDataPagination(int currentpage, int pageSize, FrontScreenImgUrl frontScreenImgUrl, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> UpdateAsync(List<FrontScreenImgUrl> frontScreenImgUrl)
		{
			try
			{
				return await this.DbConn.UpdateAsync(frontScreenImgUrl);
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
