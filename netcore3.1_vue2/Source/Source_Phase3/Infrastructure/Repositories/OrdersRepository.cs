using Dapper;
using Dapper.Contrib.Extensions;
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

	public class OrdersRepository : BaseRepository, IOrdersRepository
	{
		public OrdersRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		#region 条件によるOrders一覧を取得する
		/// <summary>
		/// 条件によるOrders一覧を取得する
		/// </summary>
		/// <param name="orders"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<Orders>> GetAllAsync(Orders orders, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic ordersParam = new ExpandoObject();

				if (!string.IsNullOrEmpty(orders.OrderId))
				{
					condition.Append(" AND OrderId = @OrderId");
					ordersParam.OrderId = orders.OrderId;
				}

				if (!string.IsNullOrEmpty(orders.Status))
				{
					condition.Append(" AND Status = @Status");
					ordersParam.Status = orders.Status;
				}

				if (!string.IsNullOrEmpty(orders.ShopCd))
				{
					condition.Append(" AND ShopCd = @ShopCd");
					ordersParam.ShopCd = orders.ShopCd;
				}

				if (!string.IsNullOrEmpty(orders.BarCd))
				{
					condition.Append(" AND BarCd = @BarCd");
					ordersParam.BarCd = orders.BarCd;
				}

				if (!string.IsNullOrEmpty(orders.ReceiveWay))
				{
					condition.Append(" AND ReceiveWay = @ReceiveWay");
					ordersParam.ReceiveWay = orders.ReceiveWay;
				}

				if (!string.IsNullOrEmpty(orders.BussinessCd))
				{
					condition.Append(" AND BussinessCd = @BussinessCd");
					ordersParam.BussinessCd = orders.BussinessCd;
				}

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
											OrderId
											,CustId
											,Status
											,ShopCd
											,BarCd
											,ReceiveWay
											,IsDiscount
											,Total
											,TotalQuantity
											,Memo
											,BussinessCd
											,CreateUserCd
											,CreateDtTm
											,UpdateUserCd
											,UpdateDtTm
											,DelFlg
										FROM
											orders
										WHERE 1=1
											{condition}");
				return (List<Orders>)await this.DbConn.QueryAsync<Orders>(sqlGetData.ToString(), (object)ordersParam);
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

		#region 条件によるOrdersSub一覧を取得する
		/// <summary>
		/// 条件によるOrdersSub一覧を取得する
		/// </summary>
		/// <param name="ordersSub"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<OrdersSub>> GetAllSubAsync(OrdersSub ordersSub, string sortColumnName = null, string sortType = null)
		{
			try
			{
				Tuple<string, string, object> data = GetSqlDataOrderAndCsvAll(0, 0, ordersSub, sortColumnName, sortType, false, true, true);
				return (List<OrdersSub>)await this.DbConn.QueryAsync<OrdersSub>(data.Item1, data.Item3);
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

		public Tuple<List<Orders>, int> GetDataPagination(int currentpage, int pageSize, Orders orders, string propetyOrder, string typeOrder)
		{
			throw new NotImplementedException();
		}

		#region Ordersを更新する
		/// <summary>
		/// Ordersを更新する
		/// </summary>
		/// <param name="lstOrders"></param>
		/// <returns></returns>
		public async Task<bool> UpdateAsync(List<Orders> lstOrders)
		{
			try
			{
				return await this.DbConn.UpdateAsync(lstOrders);
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

		#region Ordersを追加する
		/// <summary>
		/// Ordersを追加する
		/// </summary>
		/// <param name="orders"></param>
		/// <returns></returns>
		public async Task<long> AddAsync(Orders orders)
		{
			try
			{
				return await this.DbConn.InsertAsync(orders);
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
		public Task<bool> DeleteAsync(List<Orders> orders)
		{
			throw new NotImplementedException();
		}

		#region 現在の予約番号を取得する
		/// <summary>
		/// 現在の予約番号を取得する
		/// </summary>
		/// <param name="reservation"></param>
		/// <returns></returns>
		public string GetRsvIdMax(string bussinessCd)
		{
			string sqlGetRsvId = (@$"SELECT 
										Max(SUBSTRING(OrderId, 2, 10))
									FROM
										orders
									WHERE 
										bussinessCd = '{bussinessCd}' 
									AND DelFlg = { ParamConst.DelFlg.OFF } ;");
			try
			{
				return this.DbConn.QuerySingle<string>(sqlGetRsvId);
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return string.Empty;
			}
			finally
			{
				this.DbConn.Close();
			}
		}
		#endregion

		#region 条件による注文のページングデータ
		/// <summary>
		/// 条件による注文のページングデータ
		/// </summary>
		/// <param name="currentpage"></param>
		/// <param name="pageSize"></param>
		/// <param name="ordersSub"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Tuple<List<OrdersSub>, int> GetDataOrdersPagination(int currentpage, int pageSize, OrdersSub ordersSub, string sortColumnName = null, string sortType = null)
		{
			try
			{
				Tuple<string, string, object> data = GetSqlDataOrderAndCsvAll(currentpage, pageSize, ordersSub, sortColumnName, sortType, false, true, false);

				List<OrdersSub> lstOrdersSub = this.DbConn.Query<OrdersSub>(data.Item1, data.Item3).ToList();
				return new Tuple<List<OrdersSub>, int>(lstOrdersSub, GetCount(data.Item2, data.Item3));
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

		#region 検索条件による予約履歴（店舗）
		/// <summary>
		/// 検索条件による予約履歴（店舗）
		/// </summary>
		/// <param name="currentpage"></param>
		/// <param name="pageSize"></param>
		/// <param name="ordersSub"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Tuple<List<OrdersSub>, int> GetDataOrdersHitoryPagination(int currentpage, int pageSize, OrdersSub ordersSub, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder conditionSubMail = new StringBuilder("");
				StringBuilder conditionSubCustReceive = new StringBuilder("");
				StringBuilder select = new StringBuilder("");
				StringBuilder table = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic ordersHistoryParam = new ExpandoObject();

				if (!string.IsNullOrEmpty(ordersSub.ShopCd))
				{
					condition.Append(" AND ord.ShopCd = @ShopCd");
					ordersHistoryParam.ShopCd = ordersSub.ShopCd;
				}
				if (!string.IsNullOrEmpty(ordersSub.OrderId))
				{
					condition.Append($" AND ord.OrderId LIKE '%{ ordersSub.OrderId }%'");
					conditionSubMail.Append($" AND OrderId LIKE '%{ ordersSub.OrderId }%'");
					conditionSubCustReceive.Append($" AND mcr.OrderId LIKE '%{ ordersSub.OrderId }%'");
				}
				if (!string.IsNullOrEmpty(ordersSub.PhoneNumber))
				{
					condition.Append($" AND mc.PhoneNumber LIKE '%{ ordersSub.PhoneNumber }%'");
				}
				if (!string.IsNullOrEmpty(ordersSub.MailAddress))
				{
					condition.Append($" AND mc.MailAddress LIKE '%{ ordersSub.MailAddress }%'");
				}
				if (!string.IsNullOrEmpty(ordersSub.CustName))
				{
					condition.Append(@$" AND
										( 
											CONCAT(mc.KanjiFamilyName, mc.KanjiFirstName) Like '%{ ordersSub.CustName }%'
											OR CONCAT(mc.KanaFamilyName, mc.KanaFirstName) Like '%{ ordersSub.CustName }%'
										)");
				}
				if (!string.IsNullOrEmpty(ordersSub.BussinessCd))
				{
					condition.Append(" AND ord.BussinessCd = @BussinessCd");
					ordersHistoryParam.BussinessCd = ordersSub.BussinessCd;
				}
				condition.Append($" AND ord.DelFlg = { ParamConst.DelFlg.OFF } ");

				if (!string.IsNullOrEmpty(sortColumnName))
				{
					condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
				}

				select.Append(@$"SELECT
									ord.OrderId
									, ord.CustId
									, ord.Status
									, CASE ord.Status
										WHEN '{ ParamConst.ReserveStatus.ORDER }' THEN '{ ParamConst.ReserveStatus.ORDER_TEXT }'
										WHEN '{ ParamConst.ReserveStatus.PAID }' THEN '{ ParamConst.ReserveStatus.PAID_TEXT }'
										WHEN '{ ParamConst.ReserveStatus.COMPLETED_DELIVERY }' THEN '{ ParamConst.ReserveStatus.COMPLETED_DELIVERY_TEXT }'
										WHEN '{ ParamConst.ReserveStatus.CANCEL }' THEN '{ ParamConst.ReserveStatus.CANCEL_TEXT }'
									END AS StatusName
									, ord.ShopCd
									, ord.ReceiveWay
									, ord.PaymentWay
									, ord.Total
									, ord.TotalQuantity
									, ord.Memo
									, mc.KanjiFamilyName
									, mc.KanjiFirstName
									, mc.KanaFamilyName
									, mc.KanaFirstName
									, mc.PhoneNumber
									, mc.MailAddress
									, ms.ShopName
									, adrr.KanjiFamilyNameMCustReceive
									, adrr.KanjiFirstNameMCustReceive
									, adrr.KanaFamilyNameMCustReceive
									, adrr.KanaFirstNameMCustReceive
									, adrr.PhoneNumberMCustReceive
									, adrr.ZipCdMCustReceive
									, adrr.ZipCodeDspMCustReceive
									, adrr.ProvinceMCustReceive
									, adrr.ProvinceNameMCustReceive
									, adrr.AdrCd1MCustReceive
									, adrr.AdrCd2MCustReceive
									, adrr.AdrCd3MCustReceive
									, CASE WHEN sm.MailStatus IS NOT NULL
												THEN sm.MailStatus
											ELSE '{ ParamConst.MailStatus.STATUS_NOT_SEND }'
										END AS OrderMailStatus
									, CASE WHEN  sm.MailStatus = '{ ParamConst.MailStatus.STATUS_SUCCESS }' 
												THEN '{ ParamConst.MailStatus.STATUS_SUCCESS_TEXT }'
											WHEN  sm.MailStatus = '{ ParamConst.MailStatus.STATUS_ERROR }' 
												THEN '{ ParamConst.MailStatus.STATUS_ERROR_TEXT }'
											WHEN  sm.MailStatus = '{ ParamConst.MailStatus.STATUS_RESEND }'
												THEN '{ ParamConst.MailStatus.STATUS_RESEND_TEXT }'
											ELSE '{ ParamConst.MailStatus.STATUS_NOT_SEND_TEXT }'
										END AS OrderMailStatusName
								");

				table.Append($@" FROM orders ord
								INNER JOIN mcust mc
									ON ord.CustId = mc.CustId
									AND mc.DelFlg = { ParamConst.DelFlg.OFF }
								INNER JOIN mshop ms
									ON ord.ShopCd = ms.ShopCd
									AND ms.DelFlg = { ParamConst.DelFlg.OFF }
								INNER JOIN sendmail sm_order
									ON ord.OrderId = sm_order.OrderId
									AND sm_order.MailType = '{ ParamConst.MailType.MAIL_ORDER }'
									AND sm_order.SendTo = '{ ParamConst.SendTo.SENDTO_CUSTOMER }'
									AND sm_order.DelFlg = { ParamConst.DelFlg.OFF }
								LEFT JOIN (SELECT
												sm_lastest.OrderId
												, sm_lastest.RsvStatus
												, sm_lastest.SendTo
												, sm_lastest.MailStatus
												, sm_lastest.UpdateDtTm
											FROM
												(SELECT
													OrderId
													, SendTo
													, MailStatus
													, ErrorCd
													, ErrorDescription
													, MAX(UpdateDtTm) UpdateDtTm
												FROM
													sendmail
												WHERE SendTo = '{ParamConst.SendTo.SENDTO_CUSTOMER}'
												AND MailTemplateId NOT IN (
													SELECT MailTemplateId FROM mailtemplate WHERE Type IN ({ParamConst.MailType.MAIL_CHANGE + ',' + ParamConst.MailType.MAIL_REMIND})
												)
													{conditionSubMail}
												GROUP BY OrderId) sm_max
											INNER JOIN
												sendmail sm_lastest USING (OrderId, SendTo, UpdateDtTm)
											) sm
									ON ord.OrderId = sm.OrderId 
									AND ord.Status = sm.RsvStatus
								LEFT JOIN (SELECT
												mcr.OrderId
												, mcr.KanjiFamilyName AS KanjiFamilyNameMCustReceive
												, mcr.KanjiFirstName AS KanjiFirstNameMCustReceive
												, mcr.KanaFamilyName AS KanaFamilyNameMCustReceive
												, mcr.KanaFirstName AS KanaFirstNameMCustReceive
												, mcr.PhoneNumber AS PhoneNumberMCustReceive
												, adr.ZipCd AS ZipCdMCustReceive
												, adr.ZipCodeDsp AS ZipCodeDspMCustReceive
												, adr.Province AS ProvinceMCustReceive
												, kbnProvince.KbnValueName AS ProvinceNameMCustReceive
												, adr.AdrCd1 AS AdrCd1MCustReceive
												, adr.AdrCd2 AS AdrCd2MCustReceive
												, adr.AdrCd3 AS AdrCd3MCustReceive
											FROM mcustrecieve mcr
											INNER JOIN maddress adr
												ON mcr.AddrId = adr.AddrId
													AND adr.DelFlg = { ParamConst.DelFlg.OFF }
											INNER JOIN mkbnvalue kbnProvince
												ON kbnProvince.KbnCd = '{ ParamConst.Province.KBN_CD }'
												AND kbnProvince.KbnValue = adr.Province
												AND kbnProvince.DelFlg = { ParamConst.DelFlg.OFF }
											WHERE adr.AdrType = '{ ParamConst.AdrType.ADR_RECIEVE }'
												AND adr.DelFlg = { ParamConst.DelFlg.OFF }
												{ conditionSubCustReceive }
											) AS adrr
									ON ord.OrderId = adrr.OrderId
								WHERE 1=1
									{ condition }");

				sqlGetData.Append($@"{ select } { table }
									 LIMIT { pageSize } OFFSET { pageSize * (currentpage - 1) }");

				List<OrdersSub> lstOrdersSub = this.DbConn.Query<OrdersSub>(sqlGetData.ToString(), (object)ordersHistoryParam).ToList();
				return new Tuple<List<OrdersSub>, int>(lstOrdersSub, GetCount(table.ToString(), (object)ordersHistoryParam));
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

		#region Get Count OrdersSub
		/// <summary>
		/// Get Count OrdersSub
		/// </summary>
		/// <param name="table"></param>
		/// <param name="param"></param>
		/// <returns></returns>
		private int GetCount(string table, object param)
		{
			try
			{
				string sqlGetCountData = string.Empty;
				sqlGetCountData = $@"SELECT
										ord.OrderId
									{ table }";

				return this.DbConn.Query<string>(sqlGetCountData, param).Count();
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

		#region CSVダウンロード
		/// <summary>
		/// CSVダウンロード
		/// </summary>
		/// <param name="ordersSub"></param>
		/// <param name="isDownloadAll"></param>
		/// <returns></returns>
		public string DownloadCSV(OrdersSub ordersSub, bool isDownloadAll)
		{
			StringBuilder csvExport = new StringBuilder("");
			csvExport.Append("予約番号,");
			csvExport.Append("品番(SKU),");
			csvExport.Append("数量,");
			csvExport.Append("サイズ名,");
			csvExport.Append("色名,");
			csvExport.Append("商品受け取り方法,");
			csvExport.Append("決済方法,");
			csvExport.Append("早期割引特典,");
			csvExport.Append("店舗コード,");
			csvExport.Append("店舗名,");
			csvExport.Append("お客様お名前（漢字）,");
			csvExport.Append("お客様お名前（かな）,");
			csvExport.Append("お客様ご住所　郵便番号,");
			csvExport.Append("お客様ご住所　都道府県,");
			csvExport.Append("お客様ご住所　住所１,");
			csvExport.Append("お客様ご住所　住所２,");
			csvExport.Append("お客様ご住所　住所３,");
			csvExport.Append("お客様電話番号,");
			csvExport.Append("お客様メールアドレス,");
			csvExport.Append("お届け先（漢字）,");
			csvExport.Append("お届け先（かな）,");
			csvExport.Append("お届け先（電話番号）,");
			csvExport.Append("配送先住所,");
			csvExport.Append("予約ステータス\r\n");

			try
			{
				Tuple<string, string, object> data = GetSqlDataOrderAndCsvAll(0, 0, ordersSub, null, null, true, isDownloadAll ? true : false, false);
				csvExport.Append(this.DbConn.ExecuteScalar<string>(data.Item1, data.Item3));
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

			return csvExport.ToString();
		}
		#endregion


		#region  指定された条件で予約一覧（お客様情報を含め）を取得する
		/// <summary>
		/// 指定された条件で予約一覧（お客様情報を含め）を取得する
		/// </summary>
		/// <param name="rsvId"></param>
		/// <param name="custId"></param>
		/// <param name="status"></param>
		/// <param name="shopCode"></param>
		/// <param name="barCode"></param>
		/// <param name="isGetOneMailSts"></param>
		/// <returns>List<MUser></returns>
		public List<OrdersSub> GetListCustReservation(Orders orders)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder conditionSubMail = new StringBuilder("");
				StringBuilder sqlGetReserves = new StringBuilder("");
				StringBuilder sqlLeftJoinMshop = new StringBuilder("");
				dynamic reserveParam = new ExpandoObject();
				string shopColumn = string.Empty;

				string queryBrandCd = this.GetBrand("rsv.BussinessCd", "rsv.OrderId", "BrandCd");
				string queryBrandName = this.GetBrand("rsv.BussinessCd", "rsv.OrderId", "BrandName");

				if (!string.IsNullOrEmpty(orders.ShopCd))
				{
					sqlLeftJoinMshop.Append($"LEFT JOIN mshop ms ON rsv.ShopCd = ms.ShopCd");
					shopColumn = ",ms.ShopName";
				}
				if (!string.IsNullOrEmpty(orders.OrderId))
				{
					condition.Append(" AND rsv.OrderId = @OrderId");
					conditionSubMail.Append($" AND OrderId = '{ orders.OrderId }'");
					reserveParam.OrderId = orders.OrderId;
				}
				if (orders.CustId > 0)
				{
					condition.Append(" AND rsv.CustId = @CustId");
					reserveParam.CustId = orders.CustId;
				}
				if (!string.IsNullOrEmpty(orders.Status))
				{
					condition.Append(" AND rsv.Status = @Status");
					reserveParam.Status = orders.Status;
				}
				if (!string.IsNullOrEmpty(orders.ShopCd))
				{
					condition.Append(" AND rsv.ShopCd = @ShopCd");
					reserveParam.ShopCd = orders.ShopCd;
				}
				if (!string.IsNullOrEmpty(orders.BarCd))
				{
					condition.Append(" AND (TRIM(BOTH '*' FROM rsv.BarCd))= @BarCode");
					reserveParam.BarCode = orders.BarCd;
				}

				string mailColumn = string.Empty;
				string sendMailJoinTbl = string.Empty;

				mailColumn = @" ,sm.SendTo
												,sm.MailStatus AS ReserveMailStatus
												,sm.UpdateDtTm AS UpdateDtTmSendMail";
				sendMailJoinTbl = @$" LEFT JOIN (SELECT
														sm_lastest.OrderId
														, sm_lastest.RsvStatus
														, sm_lastest.SendTo
														, sm_lastest.MailStatus
														, sm_lastest.UpdateDtTm
													FROM
														(SELECT
															OrderId
															, SendTo
															, MailStatus
															, ErrorCd
															, ErrorDescription
															, MAX(UpdateDtTm) UpdateDtTm
														FROM
															sendmail
														WHERE SendTo = '{ParamConst.SendTo.SENDTO_CUSTOMER}'
														AND MailTemplateId NOT IN (
															SELECT MailTemplateId FROM mailtemplate WHERE Type IN ({ParamConst.MailType.MAIL_CHANGE + ',' + ParamConst.MailType.MAIL_REMIND})
														)
															{conditionSubMail}
														GROUP BY OrderId) sm_max
													INNER JOIN
														sendmail sm_lastest USING (OrderId, SendTo, UpdateDtTm)
													) sm
											ON rsv.OrderId = sm.OrderId 
											AND sm.RsvStatus = rsv.Status";

				condition.Append(" order by rsv.UpdateDtTm DESC");

				sqlGetReserves.Append(@$"SELECT rsv.OrderId
												,rsv.CustId
												,rsv.Status
												,rsv.ShopCd
												,rsv.BarCd
												,rsv.Total
												,rsv.TotalQuantity
												,rsv.CreateUserCd
												,rsv.CreateDtTm
												,rsv.UpdateUserCd
												,rsv.UpdateDtTm
												,rsv.Memo
												,rsv.BussinessCd
												, (SELECT BussinessName FROM mbussiness 
													WHERE BussinessCd = rsv.BussinessCd ) AS BussinessName
												,rsv.ReceiveWay
												,rsv.PaymentWay
												,CASE rsv.ReceiveWay
													WHEN '{ ParamConst.ReceiveWay.IN_SHOP_CD }' THEN '{ ParamConst.ReceiveWay.IN_SHOP }'
													WHEN '{ ParamConst.ReceiveWay.SHIPPING_CD }' THEN '{ ParamConst.ReceiveWay.SHIPPING }'
													ELSE ''
												END AS ReceiveWayName
												,rsv.IsDiscount
												,CASE rsv.IsDiscount
													WHEN { ParamConst.Discount.NOT_DISCOUNT_CD } THEN '{ ParamConst.Discount.NOT_DISCOUNT }'
													WHEN { ParamConst.Discount.HAVE_DISCOUNT_CD } THEN '{ ParamConst.Discount.HAVE_DISCOUNT }'
													ELSE ''
												END AS DiscountName
												,{queryBrandCd}
												,{queryBrandName}
												,mc.KanjiFamilyName 
												,mc.KanjiFirstName
												,mc.KanaFamilyName
												,mc.KanaFirstName
												,mc.PhoneNumber
												,mc.MailAddress
												,adr.ZipCd
												,adr.ZipCodeDsp
												,adr.Province
												,adr.AdrCd1
												,adr.AdrCd2
												,adr.AdrCd3
												{shopColumn}
												{mailColumn}
												,kbn.KbnValueName AS StatusName
												,kbn_Province.KbnValueName AS ProvinceName
											FROM orders rsv
											INNER JOIN mcust mc
												ON rsv.CustId = mc.CustId
											INNER JOIN maddress adr
												ON mc.AddrId = adr.AddrId
												AND mc.DelFlg = { ParamConst.DelFlg.OFF }
											{sqlLeftJoinMshop}					
											{sendMailJoinTbl}
											LEFT JOIN  mkbnvalue kbn 
												ON kbn.KbnCd = '{ParamConst.ReserveStatus.KBN_CD}'
												AND kbn.KbnValue = rsv.Status
											LEFT JOIN mkbnvalue kbn_Province ON kbn_Province.KbnCd = '{ParamConst.Province.KBN_CD}'
												AND kbn_Province.KbnValue = adr.Province
											WHERE rsv.BussinessCd = '{orders.BussinessCd}'
											{condition}");
				return (List<OrdersSub>)this.DbConn.Query<OrdersSub>(sqlGetReserves.ToString(), (object)reserveParam);
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

		#region GetBrand
		/// <summary>
		/// GetBrand
		/// </summary>
		/// <param name="bussinessCd"></param>
		/// <param name="orderId"></param>
		/// <param name="columnName"></param>
		/// <returns></returns>
		private string GetBrand(string bussinessCd, string orderId, string columnName)
		{
			return @$"(SELECT mb.{columnName}
						FROM orderdetail ordd
						INNER JOIN mproductdetail mpd
							ON ordd.Sku = mpd.Sku
							AND mpd.DelFlg = { ParamConst.DelFlg.OFF }
						INNER JOIN mproduct mp
							ON mpd.ProductCd = mp.ProductCd
							AND mp.DelFlg = { ParamConst.DelFlg.OFF }
						INNER JOIN mbrandrelation mbr
							ON mbr.BrandRltId = mp.BrandRltId
							AND mbr.DelFlg = { ParamConst.DelFlg.OFF }
						INNER JOIN mbrand mb
							ON mb.BrandCd = mbr.BrandCd
							AND mb.DelFlg = { ParamConst.DelFlg.OFF }
						WHERE mbr.BussinessCd = {bussinessCd}
						AND ordd.OrderId = {orderId}
						ORDER BY ordd.OrderDtlId DESC
							LIMIT 1) AS {columnName}";
		}
		#endregion


		#region SQL注文データ
		/// <summary>
		/// SQL注文データ
		/// </summary>
		/// <param name="currentpage"></param>
		/// <param name="pageSize"></param>
		/// <param name="ordersSub"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <param name="isDownloadCsv"></param>
		/// <param name="isGetOrderAndDownCsvAll"></param>
		/// <param name="isGetAllOrder"></param>
		/// <returns></returns>
		private Tuple<string, string, object> GetSqlDataOrderAndCsvAll(int currentpage
																		, int pageSize
																		, OrdersSub ordersSub
																		, string sortColumnName
																		, string sortType
																		, bool isDownloadCsv
																		, bool isGetOrderAndDownCsvAll
																		, bool isGetAllOrder)
		{
			StringBuilder condition = new StringBuilder("");
			StringBuilder conditionSubCustReceive = new StringBuilder("");
			StringBuilder conditionSubSearchBrandName = new StringBuilder("");
			StringBuilder conditionSubOrderDetail = new StringBuilder("");
			StringBuilder conditionSubBrand = new StringBuilder("");
			StringBuilder conditionSubMailbounce = new StringBuilder("");
			StringBuilder select = new StringBuilder("");
			StringBuilder table = new StringBuilder("");
			StringBuilder sqlGetData = new StringBuilder("");
			dynamic ordersParam = new ExpandoObject();

			if (!string.IsNullOrEmpty(ordersSub.OrderId))
			{
				condition.Append($" AND ord.OrderId LIKE '%{ ordersSub.OrderId }%'");
				conditionSubCustReceive.Append($" AND mcr.OrderId LIKE '%{ ordersSub.OrderId }%'");
				conditionSubSearchBrandName.Append($" AND ord.OrderId LIKE '%{ ordersSub.OrderId }%'");
				conditionSubOrderDetail.Append($" WHERE ordd.OrderId LIKE '%{ ordersSub.OrderId }%'");
				conditionSubMailbounce.Append($" AND OrderId LIKE '%{ ordersSub.OrderId }%'");
			}
			if (!string.IsNullOrEmpty(ordersSub.LstOrderId))
			{
				condition.Append($" AND ord.OrderId IN ({ ordersSub.LstOrderId })");
			}
			if (!string.IsNullOrEmpty(ordersSub.LstOrderIdUnCheck))
			{
				condition.Append($" AND ord.OrderId NOT IN ({ ordersSub.LstOrderIdUnCheck })");
			}
			if (!string.IsNullOrEmpty(ordersSub.PhoneNumber))
			{
				condition.Append($" AND mc.PhoneNumber LIKE '%{ ordersSub.PhoneNumber }%'");
			}
			if (!string.IsNullOrEmpty(ordersSub.MailAddress))
			{
				condition.Append($" AND mc.MailAddress LIKE '%{ ordersSub.MailAddress }%'");
			}
			if (!string.IsNullOrEmpty(ordersSub.BrandCd))
			{
				condition.Append(@$" AND ( mb.BrandCd = @BrandCd
											OR ord.OrderId IN (SELECT
																	ord.OrderId
																FROM orders ord
																INNER JOIN orderdetail orddt
																	ON ord.OrderId = orddt.OrderId
																	AND orddt.DelFlg = { ParamConst.DelFlg.OFF }
																INNER JOIN mproductdetail mpd
																	ON orddt.Sku = mpd.Sku
																	AND mpd.DelFlg = { ParamConst.DelFlg.OFF }
																 INNER JOIN mproduct mp
																	ON mp.ProductCd = mpd.ProductCd
																	AND mp.DelFlg = { ParamConst.DelFlg.OFF }
																INNER JOIN mbrandrelation mbr
																	ON mbr.BrandRltId = mp.BrandRltId
																	AND mbr.DelFlg = { ParamConst.DelFlg.OFF }
																INNER JOIN mbrand mb
																	ON mb.BrandCd = mbr.BrandCd
																	AND mb.DelFlg = { ParamConst.DelFlg.OFF }
																WHERE
																	mb.BrandCd = @BrandCd
																	{conditionSubSearchBrandName})
										)");
				conditionSubBrand.Append(" AND mb.BrandCd = @BrandCd");
				ordersParam.BrandCd = ordersSub.BrandCd;
			}
			if (!string.IsNullOrEmpty(ordersSub.ReceiveWay))
			{
				if (ordersSub.ReceiveWay == ParamConst.ReceiveWay.IN_SHOP_CD)
				{
					condition.Append(" AND (ord.ReceiveWay = @ReceiveWay OR ord.ReceiveWay IS NULL)");
				}
				else
				{
					condition.Append(" AND ord.ReceiveWay = @ReceiveWay");
				}
				ordersParam.ReceiveWay = ordersSub.ReceiveWay;
			}
			if (ordersSub.IsDiscount != null)
			{
				condition.Append(" AND ord.IsDiscount = @IsDiscount");
				ordersParam.IsDiscount = ordersSub.IsDiscount;
			}
			if (!string.IsNullOrEmpty(ordersSub.ShopName))
			{
				condition.Append($" AND ms.ShopName LIKE '%{ ordersSub.ShopName }%'");
			}
			if (!string.IsNullOrEmpty(ordersSub.CustName))
			{
				condition.Append(@$" AND
										( 
											CONCAT(mc.KanjiFamilyName, mc.KanjiFirstName) Like '%{ ordersSub.CustName }%'
											OR CONCAT(mc.KanaFamilyName, mc.KanaFirstName) Like '%{ ordersSub.CustName }%'
										)");
			}
			if (!string.IsNullOrEmpty(ordersSub.OrderMailStatus))
			{
				if (ordersSub.OrderMailStatus != ParamConst.MailStatus.STATUS_NOT_SEND)
				{
					condition.Append(" AND sm_order.MailStatus = @MailStatus");
					ordersParam.MailStatus = ordersSub.OrderMailStatus;
				}
				else
				{
					condition.Append(" AND (sm_order.MailStatus IS NULL OR sm_order.MailStatus = '')");
				}
			}
			if (!string.IsNullOrEmpty(ordersSub.RemindMailStatus))
			{
				if (ordersSub.RemindMailStatus != ParamConst.MailStatus.STATUS_NOT_SEND)
				{
					condition.Append(" AND sm_remind.MailStatus = @MailStatus");
					ordersParam.MailStatus = ordersSub.RemindMailStatus;
				}
				else
				{
					condition.Append(" AND (sm_remind.MailStatus IS NULL OR sm_remind.MailStatus = '')");
				}
			}
			if (!string.IsNullOrEmpty(ordersSub.FinishedMailStatus))
			{
				if (ordersSub.FinishedMailStatus != ParamConst.MailStatus.STATUS_NOT_SEND)
				{
					condition.Append(" AND sm_finished.MailStatus = @MailStatus");
					ordersParam.MailStatus = ordersSub.FinishedMailStatus;
				}
				else
				{
					condition.Append(" AND (sm_finished.MailStatus IS NULL OR sm_finished.MailStatus = '')");
				}
			}
			if (!string.IsNullOrEmpty(ordersSub.Status))
			{
				condition.Append(" AND ord.Status = @Status");
				ordersParam.Status = ordersSub.Status;
			}
			if (!string.IsNullOrEmpty(ordersSub.BussinessCd))
			{
				condition.Append(" AND ord.BussinessCd = @BussinessCd");
				conditionSubCustReceive.Append($" AND ordSub.BussinessCd = @BussinessCd");
				ordersParam.BussinessCd = ordersSub.BussinessCd;
			}
			condition.Append($" AND ord.DelFlg = { ParamConst.DelFlg.OFF } ");

			if (isGetOrderAndDownCsvAll && !isDownloadCsv)
			{
				condition.Append(" GROUP BY ord.OrderId ");
			}

			if (!string.IsNullOrEmpty(sortColumnName))
			{
				condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
			}

			string mbrandJoinTbl = string.Empty;
			string sendMailTbl = string.Empty;
			if (isGetOrderAndDownCsvAll)
			{
				mbrandJoinTbl = $@" INNER JOIN mproduct mp
										ON mp.ProductCd = mpd.ProductCd
										AND mp.DelFlg = { ParamConst.DelFlg.OFF }
									INNER JOIN mbrandrelation mbr
										ON mbr.BrandRltId = mp.BrandRltId
										AND mbr.DelFlg = { ParamConst.DelFlg.OFF }
									INNER JOIN mbrand mb
										ON mb.BrandCd = mbr.BrandCd
										AND mb.DelFlg = { ParamConst.DelFlg.OFF } ";

				sendMailTbl = $@" LEFT JOIN sendmail sm_remind
									ON ord.OrderId = sm_remind.OrderId
									AND sm_remind.MailType = '{ ParamConst.MailType.MAIL_REMIND }'
									AND sm_remind.SendTo = '{ ParamConst.SendTo.SENDTO_CUSTOMER }'
									AND sm_remind.DelFlg = { ParamConst.DelFlg.OFF }
								LEFT JOIN sendmail sm_finished
									ON ord.OrderId = sm_finished.OrderId
									AND sm_finished.MailType = '{ ParamConst.MailType.MAIL_FINISH }'
									AND sm_finished.SendTo = '{ ParamConst.SendTo.SENDTO_CUSTOMER }'
									AND sm_finished.DelFlg = { ParamConst.DelFlg.OFF }";
			}

			string mailBounceJoinTbl = string.Empty;
			string csvJoinTbl = string.Empty;
			string limitData = string.Empty;
			if (isDownloadCsv)
			{
				csvJoinTbl = $@" INNER JOIN msize msz
									ON mpd.SizeCd = msz.SizeCd
									AND msz.DelFlg = { ParamConst.DelFlg.OFF }
								INNER JOIN mcolor mcl
									ON mpd.ColorCd = mcl.ColorCd
									AND mcl.DelFlg = { ParamConst.DelFlg.OFF }";
			}
			else
			{
				mailBounceJoinTbl = $@" LEFT JOIN (SELECT
														sm_lastest.OrderId
														, sm_lastest.MailStatus
														, sm_lastest.ErrorCd
														, sm_lastest.ErrorDescription
													FROM
														(SELECT
															OrderId
															, SendTo
															, MailStatus
															, ErrorCd
															, ErrorDescription
															, MAX(UpdateDtTm) UpdateDtTm
														FROM
															sendmail
														WHERE SendTo = '{ ParamConst.SendTo.SENDTO_CUSTOMER }'
															{ conditionSubMailbounce }
														GROUP BY OrderId) sm_max
													INNER JOIN
														sendmail sm_lastest USING (OrderId, SendTo, UpdateDtTm)
													) sm_mailbounce
											ON ord.OrderId = sm_mailbounce.OrderId
											AND sm_mailbounce.MailStatus = '{ ParamConst.MailStatus.STATUS_ERROR }'";

				if (!isGetAllOrder)
				{
					limitData = $@" LIMIT { pageSize } OFFSET { pageSize * (currentpage - 1) }";
				}
			}

			select.Append(DataSelectSql(isDownloadCsv));

			table.Append($@" FROM orders ord
							INNER JOIN orderdetail orddt
								ON ord.OrderId = orddt.OrderId
								AND orddt.DelFlg = { ParamConst.DelFlg.OFF }
							INNER JOIN mproductdetail mpd
								ON orddt.Sku = mpd.Sku
								AND mpd.DelFlg = { ParamConst.DelFlg.OFF }
							{ mbrandJoinTbl }
							{ csvJoinTbl }
							INNER JOIN mcust mc
								ON ord.CustId = mc.CustId 
									AND mc.DelFlg = { ParamConst.DelFlg.OFF }
							INNER JOIN maddress adrMc
								ON mc.AddrId = adrMc.AddrId
								AND adrMc.AdrType = '{ ParamConst.AdrType.ADR_CUST }'
								AND adrMc.DelFlg = { ParamConst.DelFlg.OFF }
							INNER JOIN mkbnvalue kbnProvinceMc
								ON kbnProvinceMc.KbnCd = '{ ParamConst.Province.KBN_CD }'
								AND kbnProvinceMc.KbnValue = adrMc.Province
								AND kbnProvinceMc.DelFlg = { ParamConst.DelFlg.OFF }
							INNER JOIN sendmail sm_order
								ON ord.OrderId = sm_order.OrderId
								AND sm_order.MailType = '{ ParamConst.MailType.MAIL_ORDER }'
								AND sm_order.SendTo = '{ ParamConst.SendTo.SENDTO_CUSTOMER }'
								AND sm_order.DelFlg = { ParamConst.DelFlg.OFF }
							LEFT JOIN mshop ms
								ON ord.ShopCd = ms.ShopCd
									AND ms.DelFlg = { ParamConst.DelFlg.OFF }
							LEFT JOIN (SELECT
											mcr.OrderId
											, mcr.KanjiFamilyName AS KanjiFamilyNameMCustReceive
											, mcr.KanjiFirstName AS KanjiFirstNameMCustReceive
											, mcr.KanaFamilyName AS KanaFamilyNameMCustReceive
											, mcr.KanaFirstName AS KanaFirstNameMCustReceive
											, mcr.PhoneNumber AS PhoneNumberMCustReceive
											, adr.ZipCd AS ZipCdMCustReceive
											, adr.ZipCodeDsp AS ZipCodeDspMCustReceive
											, adr.Province AS ProvinceMCustReceive
											, kbnProvince.KbnValueName AS ProvinceNameMCustReceive
											, adr.AdrCd1 AS AdrCd1MCustReceive
											, adr.AdrCd2 AS AdrCd2MCustReceive
											, adr.AdrCd3 AS AdrCd3MCustReceive
											, mcr.CreateUserCd AS CreateUserCdCustRecieve
											, mcr.UpdateDtTm AS UpdateDtTmCustRecieve
											, adr.UpdateDtTm AS UpdateDtTmAddress
										FROM mcustrecieve mcr
										INNER JOIN orders ordSub
											ON mcr.OrderId = ordSub.OrderId
											AND ordSub.DelFlg = { ParamConst.DelFlg.OFF }
										INNER JOIN maddress adr
											ON mcr.AddrId = adr.AddrId
												AND adr.DelFlg = { ParamConst.DelFlg.OFF }
										INNER JOIN mkbnvalue kbnProvince
											ON kbnProvince.KbnCd = '{ ParamConst.Province.KBN_CD }'
											AND kbnProvince.KbnValue = adr.Province
											AND kbnProvince.DelFlg = { ParamConst.DelFlg.OFF }
										WHERE adr.AdrType = '{ ParamConst.AdrType.ADR_RECIEVE }'
											{ conditionSubCustReceive }
											AND adr.DelFlg = { ParamConst.DelFlg.OFF }) AS adrr
								ON ord.OrderId = adrr.OrderId
							{ sendMailTbl }
							{ mailBounceJoinTbl }
							WHERE 1=1
								{ condition }");

			sqlGetData.Append($@"{ select } { table } { limitData }");


			return new Tuple<string, string, object>(sqlGetData.ToString(), table.ToString(), (object)ordersParam);
		}
		#endregion

		#region データ選択
		/// <summary>
		/// データ選択
		/// </summary>
		/// <param name="isDownloadCsv"></param>
		/// <returns></returns>
		private string DataSelectSql(bool isDownloadCsv)
		{
			StringBuilder selectData = new StringBuilder("");

			if (isDownloadCsv)
			{
				selectData.Append("SET SESSION group_concat_max_len = 10000000000000000000; ");
				selectData.Append("SELECT ");
				selectData.Append("GROUP_CONCAT(");
				selectData.Append("CONCAT('\"', ord.OrderId ,'\",'");
				selectData.Append(",'\"', orddt.Sku ,'\",'");
				selectData.Append(",'\"', orddt.Quantity ,'\",'");
				selectData.Append(",'\"', msz.SizeName ,'\",'");
				selectData.Append(",'\"', mcl.ColorName ,'\",'");
				selectData.Append(",'\"', CASE");
				selectData.Append($"		WHEN ord.ReceiveWay = '{ ParamConst.ReceiveWay.IN_SHOP_CD}' OR ord.ReceiveWay IS NULL");
				selectData.Append($"			THEN '{ ParamConst.ReceiveWay.IN_SHOP }'");
				selectData.Append($"		WHEN ord.ReceiveWay = '{ ParamConst.ReceiveWay.SHIPPING_CD }'");
				selectData.Append($"			THEN '{ ParamConst.ReceiveWay.SHIPPING }'");
				selectData.Append("			ELSE '' ");
				selectData.Append("		END,'\",'");
				selectData.Append(",'\"', CASE");
				selectData.Append($"		WHEN ord.PaymentWay = '{ ParamConst.PaymentWay.PAY_IN_SHOP_CD}'");
				selectData.Append($"			THEN '{ ParamConst.PaymentWay.PAY_IN_SHOP }'");
				selectData.Append($"		WHEN ord.PaymentWay = '{ ParamConst.PaymentWay.TRANSFER_CD }'");
				selectData.Append($"			THEN '{ ParamConst.PaymentWay.TRANSFER }'");
				selectData.Append("			ELSE '' ");
				selectData.Append("		END,'\",'");
				selectData.Append(",'\"', CASE ord.IsDiscount");
				selectData.Append($"		WHEN { ParamConst.Discount.NOT_DISCOUNT_CD }");
				selectData.Append($"			THEN '{ ParamConst.Discount.NOT_DISCOUNT }'");
				selectData.Append($"		WHEN { ParamConst.Discount.HAVE_DISCOUNT_CD } ");
				selectData.Append($"			THEN '{ ParamConst.Discount.HAVE_DISCOUNT }'");
				selectData.Append("			ELSE '' ");
				selectData.Append("		END,'\",'");
				selectData.Append($",'\"', CASE WHEN ord.ShopCd IS NOT NULL AND ord.ShopCd != ''");
				selectData.Append("					THEN ord.ShopCd");
				selectData.Append("				ELSE ''");
				selectData.Append("		END,'\",'");
				selectData.Append($",'\"', CASE WHEN ord.ShopCd IS NOT NULL AND ord.ShopCd != ''");
				selectData.Append("					THEN ms.ShopName ");
				selectData.Append("				ELSE '' ");
				selectData.Append("		END,'\",'");
				selectData.Append(",'\"', CONCAT(mc.KanjiFamilyName , mc.KanjiFirstName) ,'\",'");
				selectData.Append(",'\"', CONCAT(mc.KanaFamilyName , mc.KanaFirstName) ,'\",'");
				selectData.Append(",'\"', adrMc.ZipCd ,'\",'");
				selectData.Append(",'\"', kbnProvinceMc.KbnValueName ,'\",'");
				selectData.Append(",'\"', adrMc.AdrCd1 ,'\",'");
				selectData.Append(",'\"', adrMc.AdrCd2 ,'\",'");
				selectData.Append(",'\"', CASE WHEN adrMc.AdrCd3 IS NOT NULL AND adrMc.AdrCd3 != '' ");
				selectData.Append("					THEN adrMc.AdrCd3");
				selectData.Append("				ELSE '' ");
				selectData.Append("			END,'\",'");
				selectData.Append(",'\"', mc.PhoneNumber ,'\",'");
				selectData.Append(",'\"', mc.MailAddress ,'\",'");
				selectData.Append($",'\"', CASE WHEN ord.ReceiveWay = '{ ParamConst.ReceiveWay.SHIPPING_CD }'");
				selectData.Append("					THEN CONCAT(adrr.KanjiFamilyNameMCustReceive , adrr.KanjiFirstNameMCustReceive)");
				selectData.Append("				ELSE '' ");
				selectData.Append("			END,'\",'");
				selectData.Append($",'\"', CASE WHEN ord.ReceiveWay = '{ ParamConst.ReceiveWay.SHIPPING_CD }'");
				selectData.Append("					THEN CONCAT(adrr.KanaFamilyNameMCustReceive , adrr.KanaFirstNameMCustReceive)");
				selectData.Append("				ELSE '' ");
				selectData.Append("			END,'\",'");
				selectData.Append($",'\"', CASE WHEN ord.ReceiveWay = '{ ParamConst.ReceiveWay.SHIPPING_CD }'");
				selectData.Append("					THEN adrr.PhoneNumberMCustReceive");
				selectData.Append("				ELSE '' ");
				selectData.Append("			END,'\",'");
				selectData.Append($",'\"', CASE WHEN ord.ReceiveWay = '{ ParamConst.ReceiveWay.SHIPPING_CD }' ");
				selectData.Append("					THEN CONCAT('〒',adrr.ZipCodeDspMCustReceive, adrr.ProvinceNameMCustReceive, adrr.AdrCd1MCustReceive, adrr.AdrCd2MCustReceive)");
				selectData.Append("				ELSE '' ");
				selectData.Append("			END");
				selectData.Append($",CASE WHEN ord.ReceiveWay = '{ ParamConst.ReceiveWay.SHIPPING_CD }' AND adrr.AdrCd3MCustReceive IS NOT NULL AND adrr.AdrCd3MCustReceive != ''");
				selectData.Append("				THEN adrr.AdrCd3MCustReceive");
				selectData.Append("			ELSE ''");
				selectData.Append("		END,'\",'");
				selectData.Append(",'\"', CASE ord.Status ");
				selectData.Append($"			WHEN '{ ParamConst.ReserveStatus.ORDER }' ");
				selectData.Append($"				THEN '{ ParamConst.ReserveStatus.ORDER_TEXT }'");
				selectData.Append($"			WHEN '{ ParamConst.ReserveStatus.PAID }' ");
				selectData.Append($"				THEN '{ ParamConst.ReserveStatus.PAID_TEXT }' ");
				selectData.Append($"			WHEN '{ParamConst.ReserveStatus.COMPLETED_DELIVERY}'");
				selectData.Append($"				THEN '{ParamConst.ReserveStatus.COMPLETED_DELIVERY_TEXT}'");
				selectData.Append($"			WHEN '{ ParamConst.ReserveStatus.CANCEL }' ");
				selectData.Append($"				THEN '{ ParamConst.ReserveStatus.CANCEL_TEXT }'");
				selectData.Append($"			ELSE ''");
				selectData.Append("			END,'\"'");
				selectData.Append(")");
				selectData.Append("ORDER BY ord.OrderId, mpd.ProductCd SEPARATOR '\\r\\n'");
				selectData.Append(") AS CSV");
			}
			else
			{
				selectData.Append($@"SELECT
									ord.OrderId
									, ord.BussinessCd
									, (SELECT BussinessName FROM mbussiness 
										WHERE BussinessCd = ord.BussinessCd ) AS BussinessName
									, ord.BarCd
									, ord.TotalQuantity
									, mc.CustId
									, mc.KanjiFamilyName
									, mc.KanjiFirstName
									, mc.KanaFamilyName
									, mc.KanaFirstName
									, mc.PhoneNumber
									, mc.MailAddress
									, group_concat(distinct mb.BrandCd) AS BrandCd
									, group_concat(distinct mb.BrandName) AS BrandName
									, adrMc.ZipCd
									, adrMc.ZipCodeDsp
									, adrMc.Province
									, kbnProvinceMc.KbnValueName AS ProvinceName
									, adrMc.AdrCd1
									, adrMc.AdrCd2
									, adrMc.AdrCd3
									, ord.ShopCd
									, ms.ShopName
									, ms.Area2Cd
									, ord.Total
									, adrr.KanjiFamilyNameMCustReceive
									, adrr.KanjiFirstNameMCustReceive
									, adrr.KanaFamilyNameMCustReceive
									, adrr.KanaFirstNameMCustReceive
									, adrr.PhoneNumberMCustReceive
									, adrr.ZipCdMCustReceive
									, adrr.ZipCodeDspMCustReceive
									, adrr.ProvinceMCustReceive
									, adrr.ProvinceNameMCustReceive
									, adrr.AdrCd1MCustReceive
									, adrr.AdrCd2MCustReceive
									, adrr.AdrCd3MCustReceive
									, adrr.CreateUserCdCustRecieve
									, adrr.UpdateDtTmCustRecieve
									, adrr.UpdateDtTmAddress
									, ord.ReceiveWay
									, CASE
										WHEN ord.ReceiveWay = '{ ParamConst.ReceiveWay.IN_SHOP_CD }' OR ord.ReceiveWay IS NULL THEN '{ ParamConst.ReceiveWay.IN_SHOP }'
										WHEN ord.ReceiveWay ='{ ParamConst.ReceiveWay.SHIPPING_CD }' THEN '{ ParamConst.ReceiveWay.SHIPPING }'
										ELSE ''
									END AS ReceiveWayName
									, ord.PaymentWay
									, CASE
										WHEN ord.PaymentWay = '{ ParamConst.PaymentWay.PAY_IN_SHOP_CD }' THEN '{ ParamConst.PaymentWay.PAY_IN_SHOP }'
										WHEN ord.PaymentWay ='{ ParamConst.PaymentWay.TRANSFER_CD }' THEN '{ ParamConst.PaymentWay.TRANSFER }'
										ELSE ''
									END AS PaymentWayName
									, ord.IsDiscount
									, CASE ord.IsDiscount
										WHEN { ParamConst.Discount.NOT_DISCOUNT_CD } THEN '{ ParamConst.Discount.NOT_DISCOUNT }'
										WHEN { ParamConst.Discount.HAVE_DISCOUNT_CD } THEN '{ ParamConst.Discount.HAVE_DISCOUNT }'
										ELSE ''
									END AS DiscountName
									, ord.Memo
									, ord.Status
									, CASE ord.Status
										WHEN '{ ParamConst.ReserveStatus.ORDER }' THEN '{ ParamConst.ReserveStatus.ORDER_TEXT }'
										WHEN '{ ParamConst.ReserveStatus.PAID }' THEN '{ ParamConst.ReserveStatus.PAID_TEXT }'
										WHEN '{ParamConst.ReserveStatus.COMPLETED_DELIVERY}' THEN '{ParamConst.ReserveStatus.COMPLETED_DELIVERY_TEXT}'
										WHEN '{ ParamConst.ReserveStatus.CANCEL }' THEN '{ ParamConst.ReserveStatus.CANCEL_TEXT }'
									END AS StatusName
									, ord.CreateDtTm
									, ord.UpdateDtTm
									, CASE WHEN sm_order.MailType = '{ ParamConst.MailType.MAIL_ORDER }'
													AND sm_order.MailStatus is not null
												THEN sm_order.MailStatus
											ELSE '{ ParamConst.MailStatus.STATUS_NOT_SEND }'
										END AS OrderMailStatus
									, CASE WHEN sm_order.MailType = '{ ParamConst.MailType.MAIL_ORDER }'
													AND sm_order.MailStatus = '{ ParamConst.MailStatus.STATUS_SUCCESS }' 
												THEN '{ ParamConst.MailStatus.STATUS_SUCCESS_TEXT }'
											WHEN sm_order.MailType = '{ ParamConst.MailType.MAIL_ORDER }'
													AND sm_order.MailStatus = '{ ParamConst.MailStatus.STATUS_ERROR }' 
												THEN '{ ParamConst.MailStatus.STATUS_ERROR_TEXT }'
											WHEN sm_order.MailType = '{ ParamConst.MailType.MAIL_ORDER }'
													AND sm_order.MailStatus = '{ ParamConst.MailStatus.STATUS_RESEND }'
												THEN '{ ParamConst.MailStatus.STATUS_RESEND_TEXT }'
												ELSE '{ ParamConst.MailStatus.STATUS_NOT_SEND_TEXT }'
											END AS OrderMailStatusName
									, CASE WHEN sm_remind.MailType = '{ ParamConst.MailType.MAIL_REMIND }'
													AND sm_remind.MailStatus is not null
												THEN sm_remind.MailStatus
											ELSE '{ ParamConst.MailStatus.STATUS_NOT_SEND }'
										END AS RemindMailStatus
									, CASE WHEN sm_remind.MailType = '{ ParamConst.MailType.MAIL_REMIND }'
													AND sm_remind.MailStatus = '{ ParamConst.MailStatus.STATUS_SUCCESS }'
												THEN '{ ParamConst.MailStatus.STATUS_SUCCESS_TEXT }'
											WHEN sm_remind.MailType = '{ ParamConst.MailType.MAIL_REMIND }'
													AND sm_remind.MailStatus = '{ ParamConst.MailStatus.STATUS_ERROR }'
												THEN '{ ParamConst.MailStatus.STATUS_ERROR_TEXT }'
											WHEN sm_remind.MailType = '{ ParamConst.MailType.MAIL_REMIND }'
													AND sm_remind.MailStatus = '{ ParamConst.MailStatus.STATUS_RESEND }'
												THEN '{ ParamConst.MailStatus.STATUS_RESEND_TEXT }'
												ELSE '{ ParamConst.MailStatus.STATUS_NOT_SEND_TEXT }'
											END AS RemindMailStatusName
									, CASE WHEN sm_finished.MailType = '{ ParamConst.MailType.MAIL_FINISH }'
													AND sm_finished.MailStatus is not null
												THEN sm_finished.MailStatus
											ELSE '{ ParamConst.MailStatus.STATUS_NOT_SEND }'
										END AS FinishedMailStatus
									, CASE WHEN sm_finished.MailType = '{ ParamConst.MailType.MAIL_FINISH }'
													AND sm_finished.MailStatus = '{ ParamConst.MailStatus.STATUS_SUCCESS }'
												THEN '{ ParamConst.MailStatus.STATUS_SUCCESS_TEXT }'
											WHEN sm_finished.MailType = '{ ParamConst.MailType.MAIL_FINISH }'
													AND sm_finished.MailStatus = '{ ParamConst.MailStatus.STATUS_ERROR }'
												THEN '{ ParamConst.MailStatus.STATUS_ERROR_TEXT }'
											WHEN sm_finished.MailType = '{ ParamConst.MailType.MAIL_FINISH }'
													AND sm_finished.MailStatus = '{ ParamConst.MailStatus.STATUS_RESEND }'
												THEN '{ ParamConst.MailStatus.STATUS_RESEND_TEXT }'
												ELSE '{ ParamConst.MailStatus.STATUS_NOT_SEND_TEXT }'
											END AS FinishedMailStatusName
									, sm_mailbounce.ErrorCd AS MailBounce
									, CASE WHEN sm_mailbounce.MailStatus = '{ ParamConst.MailStatus.STATUS_ERROR }'
										THEN CASE WHEN sm_mailbounce.ErrorCd = '{ ParamConst.MailBounce.MAIL_EXIST }'
													THEN '{ ParamConst.MailBounce.MAIL_EXIST_TEXT }'
												WHEN sm_mailbounce.ErrorCd = '{ ParamConst.MailBounce.MAIL_FULL }'
													THEN '{ ParamConst.MailBounce.MAIL_FULL_TEXT }'
												WHEN sm_mailbounce.ErrorCd = '{ ParamConst.MailBounce.MAIL_REJECT }'
													THEN '{ ParamConst.MailBounce.MAIL_REJECT_TEXT }'
												WHEN sm_mailbounce.ErrorCd IS NOT NULL
													THEN '{ ParamConst.MailBounce.OTHER }'
												END
									END AS MailBounceName
									, sm_mailbounce.ErrorDescription AS MailBounceDescription
									, ord.CreateUserCd
									, ord.UpdateDtTm");
			}

			return selectData.ToString();
		}
		#endregion
	}
}