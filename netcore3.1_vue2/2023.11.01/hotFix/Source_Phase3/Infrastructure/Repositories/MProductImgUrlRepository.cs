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
    public class MProductImgUrlRepository : BaseRepository, IMProductImgUrlRepository
	{
		public MProductImgUrlRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		public Task<long> AddAsync(MProductImgUrl entity)
		{
			throw new NotImplementedException();
		}

        public async Task<long> AddListAsync(List<MProductImgUrl> mProductImgUrl)
        {
			long result = 0;
			try
			{
				using (IDbConnection con = this.DbConn)
				{
					result = await con.InsertAsync(mProductImgUrl);
					return result;
				}
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return result;
			}
		}

        public Task<bool> DeleteAsync(List<MProductImgUrl> entity)
		{
			throw new NotImplementedException();
		}

		#region 条件によるMProductImgUrl一覧を取得する
		/// <summary>
		/// 条件によるMProductImgUrl一覧を取得する
		/// </summary>
		/// <param name="mProductImgUrl"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MProductImgUrl>> GetAllAsync(MProductImgUrl mProductImgUrl, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mProductImgUrlParam = new ExpandoObject();

				if (!string.IsNullOrEmpty(mProductImgUrl.ProductCd))
				{
					condition.Append(" AND ProductCd = @ProductCd");
					mProductImgUrlParam.ProductCd = mProductImgUrl.ProductCd;
				}

				if (!string.IsNullOrEmpty(mProductImgUrl.ImageUrl))
				{
					condition.Append(" AND ImageUrl = @ImageUrl");
					mProductImgUrlParam.ImageUrl = mProductImgUrl.ImageUrl;
				}

				if (mProductImgUrl.IsMainInListPage)
				{
					condition.Append(" AND IsMainInDetailPage = false");
				}

				if (mProductImgUrl.IsMainInDetailPage)
				{
					condition.Append(" AND IsMainInListPage = false");
				}

				condition.Append(" AND DelFlg = " + ParamConst.DelFlg.OFF);

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
										ProductImgUrlId
										,ProductCd
										,ImageUrl
										,IsMainInListPage
										,IsMainInDetailPage
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM Mproductimgurl
									WHERE 1=1
									{condition}");
				return (List<MProductImgUrl>)await this.DbConn.QueryAsync<MProductImgUrl>(sqlGetData.ToString(), (object)mProductImgUrlParam);
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

		public Tuple<List<MProductImgUrl>, int> GetDataPagination(int currentpage, int pageSize, MProductImgUrl entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> UpdateAsync(List<MProductImgUrl> lstMProductImgUrl)
		{
			bool result = false;
			try
			{
				using (IDbConnection con = this.DbConn)
				{
					result = await con.UpdateAsync(lstMProductImgUrl);
					return result;
				}
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return result;
			}
		}
	}
}
