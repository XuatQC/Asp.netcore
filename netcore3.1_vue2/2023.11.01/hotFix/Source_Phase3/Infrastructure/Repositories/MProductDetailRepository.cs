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

    public class MProductDetailRepository : BaseRepository, IMProductDetailRepository
	{
		public MProductDetailRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		#region Mproductdetailを追加する
		/// <summary>
		/// Mproductdetailを追加する
		/// </summary>
		/// <param name="mproductdetail"></param>
		/// <returns></returns>
		public async Task<long> AddAsync(MProductDetail mproductdetail)
		{
			try
			{
				return await this.DbConn.InsertAsync(mproductdetail);
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

		public async Task<long>  AddListAsync(List<MProductDetail> mproductdetail)
        {
			long result = ParamConst.ResultInsertProduct.INSERT_FAIL;
			try
			{
				using (IDbConnection con = this.DbConn)
				{
					result = await con.InsertAsync(mproductdetail);
					return result;
				}
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return result;
			}
		}

        public async Task<bool> DeleteAsync(List<MProductDetail> mproductdetail)
		{
			bool result = false;
			try
			{
				using (IDbConnection con = this.DbConn)
				{
					result = await con.DeleteAsync(mproductdetail);
					return result;
				}
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return result;
			}
		}

		#region 条件によるMProductDetail一覧を取得する
		/// <summary>
		/// 条件によるMProductDetail一覧を取得する
		/// </summary>
		/// <param name="mproductdetail"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MProductDetail>> GetAllAsync(MProductDetail mproductdetail, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mproductdetailParam = new ExpandoObject();
				

				if (!string.IsNullOrEmpty(mproductdetail.Sku))
				{
					condition.Append(" AND Sku = @Sku");
					mproductdetailParam.Sku = mproductdetail.Sku;
				}
				
				if (!string.IsNullOrEmpty(mproductdetail.ProductCd))
				{
					condition.Append(" AND ProductCd = @ProductCd");
					mproductdetailParam.ProductCd = mproductdetail.ProductCd;
				}

				if (!string.IsNullOrEmpty(mproductdetail.LstProductCd))
				{
					condition.Append($" AND ProductCd IN ({ mproductdetail.LstProductCd })");
				}
				
				if (!string.IsNullOrEmpty(mproductdetail.ColorCd))
				{
					condition.Append(" AND ColorCd = @ColorCd");
					mproductdetailParam.ColorCd = mproductdetail.ColorCd;
				}
				
				
				if (!string.IsNullOrEmpty(mproductdetail.SizeCd))
				{
					condition.Append(" AND SizeCd = @SizeCd");
					mproductdetailParam.SizeCd = mproductdetail.SizeCd;
				}

				condition.Append($" AND mproductdetail.DelFlg = {ParamConst.DelFlg.OFF}");
				
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
										Sku
										,ProductCd
										,mcolor.ColorCd
										,InventoryNumber
										,msize.SizeCd
										,mproductdetail.CreateUserCd
										,mproductdetail.CreateDtTm
										,mproductdetail.UpdateUserCd
										,mproductdetail.UpdateDtTm
										,mproductdetail.DelFlg
									FROM mproductdetail
									JOIN
										mcolor ON mcolor.ColorCd = mproductdetail.ColorCd
											AND mcolor.DelFlg = {ParamConst.DelFlg.OFF}
											JOIN
										msize ON msize.SizeCd = mproductdetail.SizeCd
											AND msize.DelFlg = {ParamConst.DelFlg.OFF}
									WHERE 1=1
									{condition}");
				return (List<MProductDetail>)await this.DbConn.QueryAsync<MProductDetail>(sqlGetData.ToString(), (object)mproductdetailParam);
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

		public Tuple<List<MProductDetail>, int> GetDataPagination(int currentpage, int pageSize, MProductDetail mproductdetail, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		#region Mproductdetailを更新する
		/// <summary>
		/// Mproductdetailを更新する
		/// </summary>
		/// <param name="lstMproductdetail"></param>
		/// <returns></returns>
		public async Task<bool> UpdateAsync(List<MProductDetail> lstMproductdetail)
		{
			try
			{
				return await this.DbConn.UpdateAsync(lstMproductdetail);
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
