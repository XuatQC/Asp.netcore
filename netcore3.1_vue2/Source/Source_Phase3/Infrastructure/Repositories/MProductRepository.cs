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
	public class MProductRepository : BaseRepository, IMProductRepository
	{
		public MProductRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		#region 条件によるMProductSub一覧を取得する
		/// <summary>
		/// 条件によるMProductSub一覧を取得する
		/// </summary>
		/// <param name="mProductSub"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MProductSub>> GetAllSubAsync(MProductSub mProductSub, string sortColumnName, string sortType)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic productpParam = new ExpandoObject();

				condition.Append($" AND DisplayFlg = { ParamConst.ShowHiddenItem.SHOW } ");
				condition.Append($" AND P.DelFlg = { ParamConst.DelFlg.OFF } ");
				if (!string.IsNullOrEmpty(mProductSub.BussinessCd))
				{
					condition.Append(@$" AND P.BrandRltId IN (SELECT
																	BrandRltId
																FROM
																	mbrandrelation
																WHERE
																	BussinessCd = '{ mProductSub.BussinessCd }' )");
				}
				if (mProductSub.IsMainInListPage)
				{
					condition.Append(" AND PU.IsMainInListPage = @IsMainInListPage");
					productpParam.IsMainInListPage = mProductSub.IsMainInListPage;
				}
				if (!string.IsNullOrEmpty(sortColumnName))
				{
					condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT
										P.ProductCd
										, P.ProductName
										, P.ProductName1
										, PU.ImageUrl
										, P.Year
										, P.BrandRltId
										, P.Price
										, P.DisplayFlg
										, P.MaxSizeCanOrder
										, P.MaxAmountCanOrder
									FROM
										mproduct P
									INNER JOIN mproductimgurl PU ON P.ProductCd = PU.ProductCd
									WHERE 1=1
									{condition}");
				List<MProductSub> lstProduct = new List<MProductSub>();
				using (IDbConnection con = this.DbConn)
				{
					lstProduct = (List<MProductSub>)await con.QueryAsync<MProductSub>(sqlGetData.ToString(), (object)productpParam);
				}
				return lstProduct;
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

		public async Task<List<MProduct>> GetAllAsync(MProduct mProduct, string sortColumnName, string sortType)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mproductParam = new ExpandoObject();


				if (!string.IsNullOrEmpty(mProduct.ProductCd))
				{
					condition.Append(" AND ProductCd = @ProductCd");
					mproductParam.ProductCd = mProduct.ProductCd;
				}

				if (!string.IsNullOrEmpty(mProduct.ProductName))
				{
					condition.Append(" AND ProductName = @ProductName");
					mproductParam.ProductName = mProduct.ProductName;
				}

				if (!string.IsNullOrEmpty(mProduct.ProductName1))
				{
					condition.Append(" AND ProductName1 = @ProductName1");
					mproductParam.ProductName1 = mProduct.ProductName1;
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
										ProductCd
										,ProductName
										,ProductName1
										,Year
										,BrandRltId
										,Price
										,DisplayFlg
										,DisplayFlg
										,MaxSizeCanOrder
										,MaxAmountCanOrder
										,SubBrandId
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM mproduct
									WHERE 1=1
									{condition}");
				return (List<MProduct>)await this.DbConn.QueryAsync<MProduct>(sqlGetData.ToString(), (object)mproductParam);
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

		/// <summary>
		/// Mproductを追加する
		/// </summary>
		/// <param name="mProduct"></param>
		/// <returns></returns>
		public async Task<long> AddAsync(MProduct mProduct)
		{
			try
			{
				return await this.DbConn.InsertAsync(mProduct);
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
		/// <summary>
		/// 在庫商品一覧
		/// </summary>
		/// <param name="mProductSub"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MProductSub>> GetListProductMaintain(MProductSub mProductSub, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mproductParam = new ExpandoObject();

				condition.Append($" AND mproduct.DelFlg = { ParamConst.DelFlg.OFF } ");
				if (!string.IsNullOrEmpty(mProductSub.BussinessCd))
				{
					condition.Append(" AND BussinessCd = @BussinessCd");
					mproductParam.BussinessCd = mProductSub.BussinessCd;
				}
				sqlGetData.Append(@$"SELECT 
										mproduct.ProductCd,
										ProductName,
										ProductName1,
										SUM(mproductdetail.InventoryNumber) AS TotalInventoryNumber,
										year,
										Price,
										MaxAmountCanOrder,
										MaxSizeCanOrder,
										mbrand.BrandName,
										mbrand.SubBrand,
										mproduct.SubBrandId,
										mproduct.BrandRltId,
										mproduct.CreateUserCd,
										DisplayFlg,
										PU.ImageUrl,
										mproductdetail.Sku,
										mproduct.UpdateDtTm,
										mproduct.CreateDtTm			
									FROM
										mproduct
											INNER JOIN
										mproductdetail ON mproductdetail.ProductCd = mproduct.ProductCd
											AND mproductdetail.DelFlg = {ParamConst.DelFlg.OFF}
											INNER JOIN
										mbrandrelation ON mproduct.BrandRltId = mbrandrelation.BrandRltId
											AND mbrandrelation.DelFlg = {ParamConst.DelFlg.OFF}
											INNER JOIN
										mbrand ON mbrand.BrandCd = mbrandrelation.BrandCd
											AND mbrand.DelFlg = {ParamConst.DelFlg.OFF}
											LEFT JOIN mproductimgurl PU ON mproduct.ProductCd = PU.ProductCd
											AND PU.IsMainInListPage = 1
											AND PU.DelFlg = {ParamConst.DelFlg.OFF}
									WHERE 1=1
									{condition}
									GROUP BY mproduct.ProductCd
									ORDER BY UpdateDtTm DESC");

				return (List<MProductSub>)await this.DbConn.QueryAsync<MProductSub>(sqlGetData.ToString(), (object)mproductParam);
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
		/// <summary>
		/// Mproductを更新する
		/// </summary>
		/// <param name="mProducts"></param>
		/// <returns></returns>
		public async Task<bool> UpdateAsync(List<MProduct> mProducts)
		{
			try
			{
				return await this.DbConn.UpdateAsync(mProducts);
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

		public Task<bool> DeleteAsync(List<MProduct> mProducts)
		{
			throw new NotImplementedException();
		}

		#region MProductSub一覧を取得する
		/// <summary>
		/// MProductSub一覧を取得する
		/// </summary>
		/// <param name="productCd"></param>
		/// <returns></returns>
		public List<MProductSub> GetlistProductForScreenDetail(string productCd)
		{
			try
			{
				string sqlLstProduct = $@"SELECT 
											mproduct.ProductCd,
											mproduct.ProductName,
											mproduct.Price * {ParamConst.PRICE_TAX} as Price,
											mproduct.MaxAmountCanOrder,
											mproduct.MaxSizeCanOrder,
											GROUP_CONCAT(mproductdetail.InventoryNumber) AS InventoryNumberSub,
											mbrand.BrandName,
											mbrandrelation.BrandCd,
											msize.SizeName,
											msize.SizeCd,
											GROUP_CONCAT(mcolor.ColorName ORDER BY mcolor.SortIndex) AS ColorName,
											GROUP_CONCAT(mcolor.ColorCd ORDER BY mcolor.SortIndex) AS ColorCd
										FROM
											mproduct
												JOIN
											mbrandrelation ON mproduct.BrandRltId = mbrandrelation.BrandRltId
												AND mbrandrelation.DelFlg = {ParamConst.DelFlg.OFF}
												JOIN
											mbrand ON mbrand.BrandCd = mbrandrelation.BrandCd
												AND mbrand.DelFlg = {ParamConst.DelFlg.OFF}
												JOIN
											mproductdetail ON mproductdetail.ProductCd = mproduct.ProductCd
												AND mproductdetail.DelFlg = {ParamConst.DelFlg.OFF}
												JOIN
											mcolor ON mcolor.ColorCd = mproductdetail.ColorCd
												AND mcolor.DelFlg = {ParamConst.DelFlg.OFF}
												JOIN
											msize ON msize.SizeCd = mproductdetail.SizeCd
												AND msize.DelFlg = {ParamConst.DelFlg.OFF}
										WHERE
											mproduct.ProductCd = '{productCd}'
												AND mproduct.DelFlg = {ParamConst.DelFlg.OFF}
										GROUP BY msize.SizeName
										ORDER BY msize.SortIndex";

				using (IDbConnection con = this.DbConn)
				{
					return (List<MProductSub>)con.Query<MProductSub>(sqlLstProduct);
				}
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return null;
			}
		}

		public Tuple<List<MProduct>, int> GetDataPagination(int currentpage, int pageSize, MProduct entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region CSVファイルから商品を追加する
		/// <summary>
		/// CSVファイルから商品を追加する
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
	}
}
