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

	public class OrderDetailRepository : BaseRepository, IOrderDetailRepository
	{
		public OrderDetailRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		#region 条件によるOrderDetail一覧を取得する
		/// <summary>
		/// 条件によるOrderDetail一覧を取得する
		/// </summary>
		/// <param name="orderdetail"></param>
		/// <param name="propetyOrder"></param>
		/// <param name="typeOrder"></param>
		/// <returns></returns>
		public async Task<List<OrderDetail>> GetAllAsync(OrderDetail orderdetail, string propetyOrder, string typeOrder)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic orderdetailParam = new ExpandoObject();


				if (!string.IsNullOrEmpty(orderdetail.OrderId))
				{
					condition.Append(" AND ordd.OrderId = @OrderId");
					orderdetailParam.OrderId = orderdetail.OrderId;
				}

				if (!string.IsNullOrEmpty(orderdetail.Sku))
				{
					condition.Append(" AND ordd.Sku = @Sku");
					orderdetailParam.Sku = orderdetail.Sku;
				}

				if (!string.IsNullOrEmpty(orderdetail.LstOrderId))
				{
					condition.Append($" AND ordd.OrderId IN ({ orderdetail.LstOrderId })");
				}
				condition.Append($" AND ordd.DelFlg = { ParamConst.DelFlg.OFF } ");
				condition.Append(" GROUP BY ordd.OrderId, ordd.Sku");

				if (!string.IsNullOrEmpty(propetyOrder))
				{
					
					condition.Append(" ORDER BY " + propetyOrder + " " + typeOrder);
				}

				sqlGetData.Append(@$"SELECT
										ordd.OrderDtlId
										, ordd.OrderId
										, ordd.Sku
										, ordd.Quantity
										, ordd.Price
										, ordd.SubTotal
										, mp.ProductCd
										, mp.ProductName
										, mc.ColorCd
										, mc.ColorName
										, ms.SizeCd
										, ms.SizeName
										, GROUP_CONCAT(distinct ms_grp.SizeCd ORDER BY ms_grp.SortIndex) AS SizeCdArr
										, GROUP_CONCAT(distinct ms_grp.SizeName ORDER BY ms_grp.SortIndex) AS SizeNameArr
										, (SELECT distinct GROUP_CONCAT(sub_prdDetail.ColorCd) 
												FROM mproductdetail sub_prdDetail
												INNER JOIN mcolor sub_mc
												ON sub_prdDetail.ColorCd = sub_mc.ColorCd
											WHERE sub_prdDetail.ProductCd = mpd.ProductCd 
											AND sub_prdDetail.SizeCd = mpd.SizeCd AND sub_prdDetail.InventoryNumber > 0 
											ORDER BY sub_mc.SortIndex) AS ColorCdArr
										, (SELECT distinct GROUP_CONCAT(sub_mc.ColorName) 
												FROM mproductdetail sub_prdDetail
												INNER JOIN mcolor sub_mc
												ON sub_prdDetail.ColorCd = sub_mc.ColorCd
											WHERE sub_prdDetail.ProductCd = mpd.ProductCd 
											AND sub_prdDetail.SizeCd = mpd.SizeCd AND sub_prdDetail.InventoryNumber > 0 
											ORDER BY sub_mc.SortIndex) AS ColorNameArr
										, mp.MaxSizeCanOrder
										, mp.MaxAmountCanOrder
										, ordd.CreateUserCd
										, ordd.UpdateDtTm
										, ordd.UpdateUserCd
									FROM orderdetail ordd
									INNER JOIN mproductdetail mpd 
										ON ordd.Sku = mpd.Sku
										AND mpd.DelFlg = { ParamConst.DelFlg.OFF }
									INNER JOIN mproduct mp
										ON mpd.ProductCd = mp.ProductCd
										AND mp.DelFlg = { ParamConst.DelFlg.OFF }
									INNER JOIN msize ms
										ON mpd.SizeCd = ms.SizeCd
										AND ms.DelFlg = { ParamConst.DelFlg.OFF }
									INNER JOIN mcolor mc
										ON mpd.ColorCd = mc.ColorCd
										AND mc.DelFlg = { ParamConst.DelFlg.OFF }
									RIGHT join msize ms_grp
										ON ms_grp.SizeCd in (SELECT SizeCd FROM mproductdetail WHERE
																ProductCd = mpd.ProductCd)
										AND ms_grp.DelFlg = { ParamConst.DelFlg.OFF }
									WHERE 1=1
										{condition}");
				return (List<OrderDetail>)await this.DbConn.QueryAsync<OrderDetail>(sqlGetData.ToString(), (object)orderdetailParam);
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

		#region 条件によるColor一覧を取得する
		/// <summary>
		/// 条件によるOrderDetail一覧を取得する
		/// </summary>
		/// <param name="orderdetail"></param>
		/// <param name="propetyOrder"></param>
		/// <param name="typeOrder"></param>
		/// <returns></returns>
		public async Task<List<OrderDetail>> GetLstColor(OrderDetail orderdetail, string propetyOrder, string typeOrder)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");

				if (!string.IsNullOrEmpty(propetyOrder))
				{

					condition.Append(" ORDER BY " + propetyOrder + " " + typeOrder);
				}

				sqlGetData.Append(@$"SELECT  GROUP_CONCAT(mp.ColorCd) as ColorCdArr,
											GROUP_CONCAT(mc.ColorName) as ColorNameArr
									FROM mproductdetail mp
									INNER JOIN  mcolor mc
										ON mp.ColorCd = mc.ColorCd
									WHERE mp.ProductCd = '{orderdetail.ProductCd}' 
									AND mp.SizeCd = '{orderdetail.SizeCd}' 
									AND mp.InventoryNumber > 0 {condition}");
				return (List<OrderDetail>)await this.DbConn.QueryAsync<OrderDetail>(sqlGetData.ToString());
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

		public Tuple<List<OrderDetail>, int> GetDataPagination(int currentpage, int pageSize, OrderDetail orderdetail, string propetyOrder, string typeOrder)
		{
			throw new NotImplementedException();
		}

		#region OrderDetailを更新する
		/// <summary>
		/// OrderDetailを更新する
		/// </summary>
		/// <param name="lstOrderDetail"></param>
		/// <returns></returns>
		public async Task<bool> UpdateAsync(List<OrderDetail> lstOrderDetail)
		{
			try
			{
				return await this.DbConn.UpdateAsync(lstOrderDetail);
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

		#region OrderDetailを追加する
		/// <summary>
		/// OrderDetailを追加する
		/// </summary>
		/// <param name="orderdetail"></param>
		/// <returns></returns>
		public async Task<long> AddAsync(OrderDetail orderdetail)
		{
			try
			{
				return await this.DbConn.InsertAsync(orderdetail);
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
		public Task<bool> DeleteAsync(List<OrderDetail> orderdetail)
		{

			throw new NotImplementedException();
		}

		#region OrderDetail一覧を追加する
		/// <summary>
		/// OrderDetail一覧を追加する
		/// </summary>
		/// <param name="orderdetail"></param>
		/// <returns></returns>
		public async Task<long> AddListAsync(List<OrderDetail> orderdetail)
		{
			try
			{
					return await this.DbConn.InsertAsync(orderdetail);
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