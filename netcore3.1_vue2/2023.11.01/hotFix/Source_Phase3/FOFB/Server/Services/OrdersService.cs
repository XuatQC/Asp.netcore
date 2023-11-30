using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Utility;
using ZXing;

namespace FOFB.Server.Services
{
	public class OrdersService : BaseService, IOrdersService
	{
		private static Mutex _sessionMutex = new Mutex();
		public OrdersService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<long> AddAsync(Orders orders)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<Orders> orders)
		{
			throw new NotImplementedException();
		}

		public Task<List<Orders>> GetAllAsync(Orders orders, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		#region 条件によるOrdersSub一覧を取得する
		/// <summary>
		/// 条件によるOrdersSub一覧を取得する
		/// </summary>
		/// <param name="ordersSub"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public List<OrdersSub> GetAllSub(OrdersSub ordersSub, string sortColumnName = null, string sortType = null)
		{
			if (ordersSub.OrderUnSelected.Count > 0)
			{
				// 注文番号一覧
				ordersSub.Order.LstOrderIdUnCheck = Common.GetListOrderId(ordersSub.OrderUnSelected, "'");
			}

			List<OrdersSub> ordersSubs = UnitOfWork.Orders.GetAllSubAsync(ordersSub.Order, sortColumnName, sortType).Result;

			return ordersSubs;
		}
		#endregion

		public Tuple<List<Orders>, int> GetDataPagination(int currentpage, int pageSize, Orders orders, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		#region 予約をInsertする
		/// <summary>
		/// 予約をInsertする
		/// </summary>
		/// <param name="dataOrderSub"></param>
		/// <returns></returns>
		public object InsertOrders(OrderDetailSub dataOrderSub)
		{
			long resultIns = ParamConst.ResultInsert.INSERT_FAIL;
			string barCode = "";
			bool cLock = false;
			dynamic resOrders = null;
			try
			{
				cLock = _sessionMutex.WaitOne();
				using (var transactionScope = new TransactionScope())
				{
					long resultAdrCus = AddMAddressForCus(dataOrderSub);
					if (resultAdrCus != ParamConst.ResultInsert.INSERT_FAIL)
					{
						dataOrderSub.custInfo.AddrId = (int)resultAdrCus;
						Task<long> resultAddCus = UnitOfWork.MCust.AddAsync((MCust)dataOrderSub.custInfo);

						if (resultAddCus.Result != ParamConst.ResultInsert.INSERT_FAIL)
						{
							dataOrderSub.dataOrder.CustId = (int)resultAddCus.Result;
							resOrders = InsertOrders(dataOrderSub.dataOrder);
							if (resOrders != null)
							{
								if (dataOrderSub.custRecieveInfo.ZipCd != null)
								{
									long resultAdrCusRecieve = AddMAddressForCusRecieve(dataOrderSub);
									if (resultAdrCusRecieve != ParamConst.ResultInsert.INSERT_FAIL)
									{
										dataOrderSub.custRecieveInfo.AddrId = (int)resultAdrCusRecieve;
										dataOrderSub.custRecieveInfo.OrderId = resOrders.ordersId;
										Task<long> resultInsCusRecieve = UnitOfWork.MCustRecieve.AddAsync((MCustRecieve)dataOrderSub.custRecieveInfo);
									}
								}
								// 予約データに対して予約番号を追加する
								for (int i = 0; i < dataOrderSub.dataProduct.Count; i++)
								{
									dataOrderSub.dataProduct[i].OrderId = resOrders.ordersId;
								}
								barCode = resOrders.barCode;

								resultIns = InsertListOrdersDetail(dataOrderSub.dataProduct, dataOrderSub);
							}
						}
					}
					if (resultIns == ParamConst.ResultOrders.PRODUCT_OUT_QUANTITY
						|| resultIns == ParamConst.ResultOrders.ADD_PRODUCT_FAIL)
					{
						transactionScope.Dispose();
						return new { result = resultIns, barCode = string.Empty };
					}
					transactionScope.Complete();

				}
			}
			catch (AbandonedMutexException)
			{
				cLock = true;
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
			}
			finally
			{
				_sessionMutex.ReleaseMutex();
			}
			return new { result = resultIns, barCode = barCode };
		}
		#endregion

		#region お客様にMAddressを追加する
		/// <summary>
		/// お客様にMAddressを追加する
		/// </summary>
		/// <param name="dataOrderSub"></param>
		/// <returns></returns>
		private long AddMAddressForCus(OrderDetailSub dataOrderSub)
		{
			long resultAdrCus = ParamConst.ResultInsert.INSERT_FAIL;
			MAddress mAddressForCus = new MAddress();
			mAddressForCus.ZipCd = dataOrderSub.custInfo.ZipCd;
			mAddressForCus.ZipCodeDsp = dataOrderSub.custInfo.ZipCodeDsp;
			mAddressForCus.Province = dataOrderSub.custInfo.Province;
			mAddressForCus.AdrCd1 = dataOrderSub.custInfo.AdrCd1;
			mAddressForCus.AdrCd2 = dataOrderSub.custInfo.AdrCd2;
			mAddressForCus.AdrCd3 = dataOrderSub.custInfo.AdrCd3;
			mAddressForCus.AdrType = ParamConst.AdrType.ADR_CUST;

			resultAdrCus = UnitOfWork.MAddress.AddAsync(mAddressForCus).Result;
			return resultAdrCus;
		}
		#endregion

		#region 商品受取人にMAddressを追加する
		/// <summary>
		/// 商品受取人にMAddressを追加する
		/// </summary>
		/// <param name="dataOrderSub"></param>
		/// <returns></returns>
		private long AddMAddressForCusRecieve(OrderDetailSub dataOrderSub)
		{
			long resultAdrCusRecieve = ParamConst.ResultInsert.INSERT_FAIL;
			MAddress mAddressForCusRecieve = new MAddress();
			mAddressForCusRecieve.ZipCd = dataOrderSub.custRecieveInfo.ZipCd;
			mAddressForCusRecieve.ZipCodeDsp = dataOrderSub.custRecieveInfo.ZipCodeDsp;
			mAddressForCusRecieve.Province = dataOrderSub.custRecieveInfo.Province;
			mAddressForCusRecieve.AdrCd1 = dataOrderSub.custRecieveInfo.AdrCd1;
			mAddressForCusRecieve.AdrCd2 = dataOrderSub.custRecieveInfo.AdrCd2;
			mAddressForCusRecieve.AdrCd3 = dataOrderSub.custRecieveInfo.AdrCd3;
			mAddressForCusRecieve.AdrType = ParamConst.AdrType.ADR_RECIEVE;
			resultAdrCusRecieve = UnitOfWork.MAddress.AddAsync(mAddressForCusRecieve).Result;
			return resultAdrCusRecieve;
		}
		#endregion

		#region 予約一覧を追加する
		/// <summary>
		/// 予約一覧を追加する
		/// </summary>
		/// <param name="reservationDetail"></param>
		/// <param name="dataOrderSub"></param>
		/// <returns></returns>
		public long InsertListOrdersDetail(List<OrderDetail> reservationDetail, OrderDetailSub dataOrderSub)
		{
			long resultInsOrdersDtl = ParamConst.ResultInsert.INSERT_FAIL;
			try
			{
				// 在庫数量をチェックする
				dynamic chkProduct = CheckInventoryProduct(reservationDetail, resultInsOrdersDtl);
				if (chkProduct.resultInsOrdersDtl != ParamConst.ResultOrders.PRODUCT_OUT_QUANTITY
					&& chkProduct.resultUpd)
				{
					// 予約メール送信
					SendMail resultSendMail = SendMailOrder(dataOrderSub.dataProduct, dataOrderSub.custInfo, dataOrderSub.custRecieveInfo, dataOrderSub.dataOrder);
					if (resultSendMail != null)
					{
						// 予約履歴をInsertする
						long resultInsResHis = InsertReserveHistorys(reservationDetail, resultSendMail);
						if (resultInsResHis != ParamConst.ResultInsert.INSERT_FAIL)
						{
							resultInsOrdersDtl = UnitOfWork.OrderDetail.AddListAsync(reservationDetail).Result;
						}
					}
				}
				else
				{
					resultInsOrdersDtl = chkProduct.resultInsOrdersDtl;
				}
				return resultInsOrdersDtl;
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return resultInsOrdersDtl;
			}
		}
		#endregion

		#region 予約履歴をInsertする
		/// <summary>
		/// 予約履歴をInsertする
		/// </summary>
		/// <param name="reservationDetail"></param>
		/// <param name="resultSendMail"></param>
		/// <returns></returns>
		private long InsertReserveHistorys(List<OrderDetail> reservationDetail, SendMail resultSendMail)
		{
			#region Delete insert muilti order history
			//OrdHistory reservehistory = null;
			//List<OrdHistory> reservehistorys = new List<OrdHistory>();
			//for (int i = 0; i < reservationDetail.Count; i++)
			//{
			//	reservehistory = new OrdHistory();
			//	reservehistory.OrderId = reservationDetail[i].OrderId;
			//	reservehistory.HistType = ParamConst.Histype.UPDATE_STATUS;
			//	reservehistory.UpdatedStatus = ParamConst.ReserveStatus.ORDER;
			//	reservehistory.ProductCd = reservationDetail[i].ProductCd;
			//	reservehistory.Size = reservationDetail[i].SizeCd;
			//	reservehistory.Color = reservationDetail[i].ColorCd;
			//	reservehistory.MailId = resultSendMail.MailId;
			//	reservehistory.MailType = resultSendMail.MailType;
			//	reservehistory.MailStatus = resultSendMail.MailStatus;
			//	reservehistory.UpdateUserCd = reservationDetail[i].UpdateUserCd;

			//	reservehistorys.Add(reservehistory);
			//}

			//return UnitOfWork.OrdHistory.AddListAsync(reservehistorys).Result;
			#endregion

			if (reservationDetail.Count == 0)
			{
				return ParamConst.ResultInsert.INSERT_FAIL;
			}
			List<OrdHistory> lstOrdHistory = new List<OrdHistory>();

			lstOrdHistory.Add(addOrdHistory(ParamConst.Histype.UPDATE_STATUS, resultSendMail, reservationDetail[0]));
			lstOrdHistory.Add(addOrdHistory(ParamConst.Histype.SEND_MAIL, resultSendMail, reservationDetail[0]));

			return UnitOfWork.OrdHistory.AddListAsync(lstOrdHistory).Result;
		}
		private OrdHistory addOrdHistory(string histType, SendMail resultSendMail, OrderDetail reservationDetail)
		{
			OrdHistory reservehistory = new OrdHistory();

			reservehistory.OrderId = reservationDetail.OrderId;
			reservehistory.HistType = histType;
			reservehistory.UpdatedStatus = ParamConst.ReserveStatus.ORDER;
			reservehistory.MailId = resultSendMail.MailId;
			reservehistory.MailType = resultSendMail.MailType;
			reservehistory.MailStatus = resultSendMail.MailStatus;
			reservehistory.UpdateUserCd = reservationDetail.UpdateUserCd;
			return reservehistory;
		}
		#endregion

		#region 在庫数量をチェックする
		/// <summary>
		/// 在庫数量をチェックする
		/// </summary>
		/// <param name="reservationDetail"></param>
		/// <param name="resultInsOrdersDtl"></param>
		/// <returns></returns>
		private object CheckInventoryProduct(List<OrderDetail> reservationDetail, long resultInsOrdersDtl)
		{
			bool resultUpd = false;
			MProductDetail mproductdetail = new MProductDetail();
			List<MProductDetail> listProduct = UnitOfWork.MProductDetail.GetAllAsync(mproductdetail).Result;
			List<MProductDetail> listProductUpdate = new List<MProductDetail>();
			for (int i = 0; i < listProduct.Count; i++)
			{
				for (int j = 0; j < reservationDetail.Count; j++)
				{
					if (listProduct[i].Sku == reservationDetail[j].Sku)
					{
						listProduct[i].InventoryNumber = listProduct[i].InventoryNumber - reservationDetail[j].Quantity;
						listProductUpdate.Add(listProduct[i]);
						if (listProduct[i].InventoryNumber < 0)
						{
							resultInsOrdersDtl = ParamConst.ResultOrders.PRODUCT_OUT_QUANTITY;
							return new { resultInsOrdersDtl = resultInsOrdersDtl, resultUpd = resultUpd };
						}
						break;
					}
				}
			}
			resultUpd = UnitOfWork.MProductDetail.UpdateAsync(listProductUpdate).Result;
			return new { resultInsOrdersDtl = resultInsOrdersDtl, resultUpd = resultUpd };

		}
		#endregion

		#region 予約追加
		/// <summary>
		/// 予約追加
		/// </summary>
		/// <param name="dataOrder"></param>
		/// <returns></returns>
		public object InsertOrders(OrdersSub dataOrder)
		{
			object resultWithBarCode = null;
			// 予約番号生成
			dataOrder = GenBarCode(dataOrder);

			dataOrder.CreateDtTm = Common.GetCurrJPDateTime();
			long resultIns = UnitOfWork.Orders.AddAsync(dataOrder).Result;
			if (resultIns != ParamConst.ResultInsert.INSERT_FAIL)
			{
				resultWithBarCode = new { barCode = dataOrder.BarCd, ordersId = dataOrder.OrderId };
			}
			return resultWithBarCode;
		}
		#endregion

		#region バーコードを生成する
		/// <summary>
		/// バーコードを生成する
		/// </summary>
		/// <param name="dataOrder"></param>
		/// <returns></returns>
		private OrdersSub GenBarCode(OrdersSub dataOrder)
		{
			dataOrder.OrderId = GetOrdersIdTmp(dataOrder);

			// バーコード生成
			string barCode = dataOrder.OrderId.Substring(1);

			dataOrder.BarCd = string.Concat("*", dataOrder.OrderId, GenDigit(barCode, char.Parse(dataOrder.BussinessCd)), "*");

			ExportBarCode(dataOrder);

			return dataOrder;
		}
		#endregion

		#region バーコード画像を保存する
		/// <summary>
		/// バーコード画像を保存する
		/// </summary>
		/// <param name="dataOrder"></param>
		private void ExportBarCode(OrdersSub dataOrder)
		{
			BarcodeWriter writer = new BarcodeWriter() { Format = BarcodeFormat.CODE_39 };
			string path_ImgBarCd = Common.GetPathImageBarCd(dataOrder.BussinessName, ParamConst.PATH_BAR_CODE_IMAGE);
			Directory.CreateDirectory(path_ImgBarCd);
			writer.Write(dataOrder.BarCd.Replace("*", "")).Save(path_ImgBarCd + "\\" + dataOrder.OrderId + ".jpg");
		}
		#endregion

		#region 予約データに対して予約番号を作成する
		/// <summary>
		/// 予約データに対して予約番号を作成する
		/// </summary>
		/// <param name="dataOrder"></param>
		/// <returns></returns>
		private string GetOrdersIdTmp(OrdersSub dataOrder)
		{
			DateTime dtNow = Common.GetCurrJPDateTime();
			string maxOrderId = UnitOfWork.Orders.GetRsvIdMax(dataOrder.BussinessCd);
			string currDt = dtNow.ToString("yyMMdd");
			int nextRsvId = 0;

			if (!string.IsNullOrEmpty(maxOrderId))
			{
				string maxDt = maxOrderId.Substring(0, 6);
				if (maxDt == currDt)
				{
					nextRsvId = int.Parse(maxOrderId.Substring(6, 4)) + 1;
				}
			}

			return string.Concat(dataOrder.BussinessCd, currDt, nextRsvId.ToString().PadLeft(4, '0'));
		}
		#endregion

		#region バーコードデジット生成 
		/// <summary>
		/// バーコードデジット生成 
		/// </summary>
		/// <param name="barCode"></param>
		/// <returns></returns>
		public string GenDigit(string barCode, char bussinessCd)
		{
			int sumCode = char.ToUpper(bussinessCd) - 55;
			if (sumCode < 10)
			{
				sumCode = int.Parse(bussinessCd.ToString());
			}

			for (int i = 0; i <= 9; i++)
			{
				sumCode += int.Parse(barCode.Substring(i, 1));
			}

			int checkDigit = sumCode % 43;
			string digit = "";
			if (checkDigit <= 9)
			{
				digit += (char)(48 + checkDigit);
			}
			else if (checkDigit <= 35)
			{
				digit += (char)(55 + checkDigit);

			}
			else
			{
				switch (checkDigit)
				{
					case 36:
						digit += (char)(45);
						break;
					case 37:
						digit += (char)(46);
						break;
					case 38:
						digit += (char)(48);
						break;
					case 39:
						digit += (char)(36);
						break;
					case 40:
						digit += (char)(47);
						break;
					case 41:
						digit += (char)(43);
						break;
					case 42:
						digit += (char)(37);
						break;
					default:
						break;
				}
			}
			return digit;
		}
		#endregion

		#region Ordersを更新する
		/// <summary>
		/// Ordersを更新する
		/// </summary>
		/// <param name="orders"></param>
		/// <returns></returns>
		public Task<bool> UpdateAsync(List<Orders> orders)
		{
			return UnitOfWork.Orders.UpdateAsync(orders);
		}
		#endregion

		#region 予約メール送信
		/// <summary>
		/// 予約メール送信
		/// </summary>
		/// <param name="orderDetail"></param>
		/// <param name="mCustSub"></param>
		/// <param name="mCustrecieveSub"></param>
		/// <param name="ordersSub"></param>
		/// <returns></returns>
		private SendMail SendMailOrder(List<OrderDetail> orderDetail, MCustSub mCustSub, MCustrecieveSub mCustrecieveSub, OrdersSub ordersSub)
		{
			string path_ImgBarCd = string.Empty;
			SendMail sendMail = null;
			List<SendMail> lstSendMail = new List<SendMail>();
			List<string> lstEmailTo = new List<string>();
			List<string> lstUrlImgBarCd = new List<string>();
			SendMail sendMailResult = new SendMail();
			List<MBank> lstMBank = null;

			// メールテンプレート一覧取得
			MailTemplate mailTemplate = new MailTemplate();
			mailTemplate.Type = ParamConst.MailType.MAIL_ORDER;
			mailTemplate.BussinessCd = ordersSub.BussinessCd;
			List<MailTemplate> lstMailTemplate = UnitOfWork.MailTemplate.GetAllAsync(mailTemplate).Result;

			if (ordersSub.PaymentWay == ParamConst.PaymentWay.TRANSFER_CD)
			{
				MBank mBank = new MBank();
				mBank.BussinessCd = ordersSub.BussinessCd;
				lstMBank = UnitOfWork.MBank.GetAllAsync(mBank).Result;
			}

			if (lstMailTemplate != null)
			{
				path_ImgBarCd = Common.GetPathImageBarCd(ordersSub.BussinessName, ParamConst.PATH_BAR_CODE_IMAGE);
				for (int i = 0; i < lstMailTemplate.Count; i++)
				{
					sendMail = Common.CreateMail(ordersSub,
												orderDetail,
												mCustSub,
												mCustrecieveSub,
												lstMailTemplate[i],
												null,
												lstMBank != null && lstMBank.Count > 0 ? lstMBank[0] : null);

					lstSendMail.Add(sendMail);
					lstEmailTo.Add(lstMailTemplate[i].SendTo == ParamConst.SendTo.SENDTO_CUSTOMER ? mCustSub.MailAddress : null);
					lstUrlImgBarCd.Add(path_ImgBarCd + "/" + ordersSub.OrderId + ".jpg");
				}

				MailSetting mailSetting = new MailSetting();
				mailSetting.BussinessCd = ordersSub.BussinessCd;
				List<MailSetting> lstMailSetting = UnitOfWork.MailSetting.GetAllAsync(mailSetting).Result;

				if (lstMailSetting != null)
				{
					// メール送信
					List<SendMail> sendMailsResult = UnitOfWork.SendMail.SendMail(lstSendMail, lstEmailTo, lstMailSetting[0], lstUrlImgBarCd);
					if (sendMailsResult != null)
					{
						long mailId = 0;
						// 履歴新規追加
						for (int i = 0; i < sendMailsResult.Count; i++)
						{
							mailId = UnitOfWork.SendMail.AddAsync(sendMailsResult[i]).Result;
							if (mailId != 0 && sendMailsResult[i].SendTo == ParamConst.SendTo.SENDTO_CUSTOMER)
							{
								sendMailsResult[i].MailId = (int)mailId;
								sendMailResult = sendMailsResult[i];
							}
						}
					}
				}
			}
			return sendMailResult;
		}
		#endregion

		#region 条件による注文一覧
		/// <summary>
		/// 条件による注文一覧
		/// </summary>
		/// <param name="currentpage"></param>
		/// <param name="pageSize"></param>
		/// <param name="ordersSub"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Tuple<List<OrdersSub>, int> GetDataOrdersPagination(int currentpage, int pageSize, OrdersSub ordersSub, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.Orders.GetDataOrdersPagination(currentpage, pageSize, ordersSub, sortColumnName, sortType);
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
			return UnitOfWork.Orders.GetDataOrdersHitoryPagination(currentpage, pageSize, ordersSub, sortColumnName, sortType);
		}
		#endregion

		#region 注文メール再送
		/// <summary>
		/// 注文メール再送
		/// </summary>
		/// <param name="ordersSubs"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		public List<SendMailError> ResendOrderMail(List<OrdersSub> ordersSubs, string updateUserCd)
		{
			string path_ImgBarCd = string.Empty;
			List<SendMail> lstSendMail = new List<SendMail>();
			List<string> lstEmailTo = new List<string>();
			List<string> lstUrlImgBarCd = new List<string>();
			List<SendMailError> sendMailErrors = new List<SendMailError>();
			SendMailError sendMailError = null;
			MailTemplate mailTemplateBySendTo = null;

			// 注文番号一覧
			string lstOrderId = Common.GetListOrderId(ordersSubs, "'");

			MBank mBank = new MBank();
			mBank.BussinessCd = ordersSubs[0].BussinessCd;
			List<MBank> lstMBank = UnitOfWork.MBank.GetAllAsync(mBank).Result;

			// メールテンプレート一覧取得
			MailTemplate mailTemplate = new MailTemplate();
			mailTemplate.Type = ParamConst.MailType.MAIL_ORDER;
			mailTemplate.BussinessCd = ordersSubs[0].BussinessCd;
			List<MailTemplate> lstMailTemplate = UnitOfWork.MailTemplate.GetAllAsync(mailTemplate).Result;

			// 予約番号による商品詳細一覧
			OrderDetail orderDetail = new OrderDetail();
			orderDetail.LstOrderId = lstOrderId;
			List<OrderDetail> lstOrderDetail = UnitOfWork.OrderDetail.GetAllAsync(orderDetail).Result;

			// 送信した注文メールの一覧
			SendMail mailSended = new SendMail();
			mailSended.RsvStatus = ParamConst.ReserveStatus.ORDER;
			mailSended.MailType = ParamConst.MailType.MAIL_ORDER;
			mailSended.LstOrderId = lstOrderId;
			List<SendMail> lstMailSended = UnitOfWork.SendMail.GetAllAsync(mailSended).Result;

			if (lstMailTemplate != null && lstOrderDetail != null && lstMailSended != null)
			{
				List<SendMail> resendMails = null;
				List<OrderDetail> orderDetails = null;
				MCustSub mCustSub = null;
				MCustrecieveSub mCustrecieveSub = null;
				string contentMail = string.Empty;
				for (int i = 0; i < ordersSubs.Count; i++)
				{
					path_ImgBarCd = Common.GetPathImageBarCd(ordersSubs[i].BussinessName, ParamConst.PATH_BAR_CODE_IMAGE);

					resendMails = new List<SendMail>();
					resendMails = GetMailSendedByOrdId(lstMailSended, ordersSubs[i].OrderId);

					if (resendMails.Count > 0)
					{
						orderDetails = new List<OrderDetail>();
						orderDetails = GetOrderDetailByOrdId(lstOrderDetail, ordersSubs[i].OrderId);

						for (int j = 0; j < resendMails.Count; j++)
						{
							mCustSub = ConvertObjectMCust(ordersSubs[i]);
							mCustrecieveSub = ConvertObjectMCustrecieve(ordersSubs[i]);

							contentMail = string.Empty;
							mailTemplateBySendTo = new MailTemplate();
							mailTemplateBySendTo = lstMailTemplate.FirstOrDefault(x => x.SendTo == resendMails[j].SendTo);
							contentMail = mailTemplateBySendTo.Content;
							resendMails[j].MailTitle = mailTemplateBySendTo.Title.Replace(ParamConst.ItemMailTemplate.MAIL_RSV_ID, ordersSubs[i].OrderId);
							resendMails[j].MailContent = Common.CreateContentMail(ordersSubs[i],
																				mCustSub,
																				mCustrecieveSub,
																				orderDetails,
																				contentMail,
																				!string.IsNullOrEmpty(ordersSubs[i].PaymentWay) && ordersSubs[i].PaymentWay == ParamConst.PaymentWay.TRANSFER_CD ?
																																						lstMBank[0] :
																																						null);
							resendMails[j].UpdateUserCd = updateUserCd;

							lstSendMail.Add(resendMails[j]);
							lstEmailTo.Add(resendMails[j].SendTo == ParamConst.SendTo.SENDTO_CUSTOMER ? ordersSubs[i].MailAddress : null);
							lstUrlImgBarCd.Add(path_ImgBarCd + "/" + ordersSubs[i].OrderId + ".jpg");
						}
					}
				}

				MailSetting mailSetting = new MailSetting();
				mailSetting.BussinessCd = ordersSubs[0].BussinessCd;
				List<MailSetting> lstMailSetting = UnitOfWork.MailSetting.GetAllAsync(mailSetting).Result;

				if (lstMailSetting != null)
				{
					// メール送信
					List<SendMail> sendMailsResult = UnitOfWork.SendMail.SendMail(lstSendMail, lstEmailTo, lstMailSetting[0], lstUrlImgBarCd);
					if (sendMailsResult != null)
					{
						for (int i = 0; i < sendMailsResult.Count; i++)
						{
							if (sendMailsResult[i].MailStatus == ParamConst.MailStatus.STATUS_ERROR
								&& sendMailsResult[i].SendTo == ParamConst.SendTo.SENDTO_CUSTOMER)
							{
								sendMailError = new SendMailError();
								sendMailError.OrderId = sendMailsResult[i].OrderId;

								sendMailErrors.Add(sendMailError);
							}
						}

						// メール更新
						bool resultMailUpd = UnitOfWork.SendMail.UpdateAsync(sendMailsResult).Result;

						if (resultMailUpd)
						{
							// 予約履歴をInsertする
							InsertSendMailHistorys(sendMailsResult);
						}
					}
				}
			}

			return sendMailErrors;
		}
		#endregion

		#region 入金リマインドメール送信
		/// <summary>
		/// 入金リマインドメール送信
		/// </summary>
		/// <param name="ordersSubs"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		public List<SendMailError> SendRemindMail(List<OrdersSub> ordersSubs, string updateUserCd)
		{
			string path_ImgBarCd = string.Empty;
			List<SendMail> lstSendMail = new List<SendMail>();
			SendMail sendMail = null;
			List<string> lstEmailTo = new List<string>();
			List<string> lstUrlImgBarCd = new List<string>();
			List<SendMailError> sendMailErrors = new List<SendMailError>();
			SendMailError sendMailError = null;

			// 注文番号一覧
			string lstOrderId = Common.GetListOrderId(ordersSubs, "'");

			// メールテンプレート一覧取得
			MailTemplate mailTemplate = new MailTemplate();
			mailTemplate.Type = ParamConst.MailType.MAIL_REMIND;
			mailTemplate.BussinessCd = ordersSubs[0].BussinessCd;
			List<MailTemplate> lstMailTemplate = UnitOfWork.MailTemplate.GetAllAsync(mailTemplate).Result;

			// 予約番号による商品詳細一覧
			OrderDetail orderDetail = new OrderDetail();
			orderDetail.LstOrderId = lstOrderId;
			List<OrderDetail> lstOrderDetail = UnitOfWork.OrderDetail.GetAllAsync(orderDetail).Result;

			// 送信したリマインドメールの一覧
			SendMail mailSended = new SendMail();
			mailSended.RsvStatus = ParamConst.ReserveStatus.ORDER;
			mailSended.MailType = ParamConst.MailType.MAIL_REMIND;
			mailSended.LstOrderId = lstOrderId;
			List<SendMail> lstMailSended = UnitOfWork.SendMail.GetAllAsync(mailSended).Result;

			if (lstMailTemplate != null && lstOrderDetail != null)
			{
				List<SendMail> resendMails = null;
				List<OrderDetail> orderDetails = null;
				MCustSub mCustSub = null;
				MCustrecieveSub mCustrecieveSub = null;
				string contentMail = string.Empty;

				for (int i = 0; i < ordersSubs.Count; i++)
				{
					path_ImgBarCd = Common.GetPathImageBarCd(ordersSubs[i].BussinessName, ParamConst.PATH_BAR_CODE_IMAGE);
					orderDetails = new List<OrderDetail>();
					orderDetails = GetOrderDetailByOrdId(lstOrderDetail, ordersSubs[i].OrderId);

					resendMails = new List<SendMail>();
					resendMails = GetMailSendedByOrdId(lstMailSended, ordersSubs[i].OrderId);

					mCustSub = ConvertObjectMCust(ordersSubs[i]);
					mCustrecieveSub = ConvertObjectMCustrecieve(ordersSubs[i]);

					// ない場合、メールを作成
					if (resendMails.Count == 0)
					{
						sendMail = Common.CreateMail(ordersSubs[i], orderDetails, mCustSub, mCustrecieveSub, lstMailTemplate[0], updateUserCd);

						lstSendMail.Add(sendMail);
						lstEmailTo.Add(ordersSubs[i].MailAddress);
						lstUrlImgBarCd.Add(path_ImgBarCd + "/" + ordersSubs[i].OrderId + ".jpg");
					}
					// 既に送信したメールがある場合
					else
					{
						contentMail = string.Empty;
						contentMail = lstMailTemplate[0].Content;
						resendMails[0].MailTitle = lstMailTemplate[0].Title.Replace(ParamConst.ItemMailTemplate.MAIL_RSV_ID, ordersSubs[i].OrderId);
						resendMails[0].MailContent = Common.CreateContentMail(ordersSubs[i], mCustSub, mCustrecieveSub, orderDetails, contentMail);
						resendMails[0].UpdateUserCd = updateUserCd;

						lstSendMail.Add(resendMails[0]);
						lstEmailTo.Add(resendMails[0].SendTo == ParamConst.SendTo.SENDTO_CUSTOMER ? ordersSubs[i].MailAddress : null);
						lstUrlImgBarCd.Add(path_ImgBarCd + "/" + ordersSubs[i].OrderId + ".jpg");
					}
				}

				MailSetting mailSetting = new MailSetting();
				mailSetting.BussinessCd = ordersSubs[0].BussinessCd;
				List<MailSetting> lstMailSetting = UnitOfWork.MailSetting.GetAllAsync(mailSetting).Result;

				if (lstMailSetting != null)
				{
					// メール送信
					List<SendMail> sendMailsResult = UnitOfWork.SendMail.SendMail(lstSendMail, lstEmailTo, lstMailSetting[0], lstUrlImgBarCd);
					if (sendMailsResult != null)
					{
						long mailId = 0;
						List<SendMail> sendMailsUpd = new List<SendMail>();
						for (int i = 0; i < sendMailsResult.Count; i++)
						{
							if (sendMailsResult[i].MailStatus == ParamConst.MailStatus.STATUS_ERROR)
							{
								sendMailError = new SendMailError();
								sendMailError.OrderId = sendMailsResult[i].OrderId;

								sendMailErrors.Add(sendMailError);
							}

							if (sendMailsResult[i].MailId == 0)
							{
								mailId = UnitOfWork.SendMail.AddAsync(sendMailsResult[i]).Result;
								if (mailId != 0)
								{
									sendMailsResult[i].MailId = (int)mailId;
								}
							}
							else
							{
								sendMailsUpd.Add(sendMailsResult[i]);
							}
						}

						if (sendMailsUpd.Count > 0)
						{
							// メール更新
							UnitOfWork.SendMail.UpdateAsync(sendMailsUpd);
						}

						// 予約履歴をInsertする
						InsertSendMailHistorys(sendMailsResult);
					}
				}
			}

			return sendMailErrors;
		}
		#endregion

		#region 注文キャンセル
		/// <summary>
		/// 注文キャンセル
		/// </summary>
		/// <param name="ordersSubs"></param>
		/// <param name="isSendMail"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		public object CancelOrder(List<OrdersSub> ordersSubs, bool isSendMail, string updateUserCd)
		{
			bool isCancelSuccess = false;
			List<SendMailError> sendMailErrors = null;

			List<Orders> ordersUpd = new List<Orders>();
			Orders orderUpd = null;
			for (int i = 0; i < ordersSubs.Count; i++)
			{
				orderUpd = new Orders();
				orderUpd.OrderId = ordersSubs[i].OrderId;
				orderUpd.CustId = ordersSubs[i].CustId;
				orderUpd.Status = ordersSubs[i].Status;
				orderUpd.ShopCd = ordersSubs[i].ShopCd;
				orderUpd.BarCd = ordersSubs[i].BarCd;
				orderUpd.ReceiveWay = ordersSubs[i].ReceiveWay;
				orderUpd.IsDiscount = ordersSubs[i].IsDiscount;
				orderUpd.Total = ordersSubs[i].Total;
				orderUpd.TotalQuantity = ordersSubs[i].TotalQuantity;
				orderUpd.Memo = ordersSubs[i].Memo;
				orderUpd.BussinessCd = ordersSubs[i].BussinessCd;
				orderUpd.CreateUserCd = ordersSubs[i].CreateUserCd;
				orderUpd.CreateDtTm = ordersSubs[i].CreateDtTm;
				orderUpd.UpdateUserCd = updateUserCd;

				ordersUpd.Add(orderUpd);
			}

			isCancelSuccess = UnitOfWork.Orders.UpdateAsync(ordersUpd).Result;

			if (isCancelSuccess)
			{
				// 注文番号一覧
				string lstOrderId = Common.GetListOrderId(ordersSubs, "'");

				// 予約番号による商品詳細一覧
				OrderDetail orderDetail = new OrderDetail();
				orderDetail.LstOrderId = lstOrderId;
				List<OrderDetail> orderDetails = UnitOfWork.OrderDetail.GetAllAsync(orderDetail).Result;

				isCancelSuccess = UpdateInventoryCancelOrder(orderDetails, updateUserCd);

				if (isCancelSuccess)
				{
					// 注文キャンセル履歴を追加する
					InsertCancelOrdersHistorys(ordersSubs, updateUserCd);

					if (isSendMail)
					{
						sendMailErrors = SendCancelOrderMail(ordersSubs, orderDetails, updateUserCd);
					}
				}
			}

			return new { isCancelSuccess = isCancelSuccess, sendMailErrors = sendMailErrors };
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
			string lstOrderId = string.Empty;

			if (!isDownloadAll)
			{
				// 注文番号一覧
				lstOrderId = Common.GetListOrderId(ordersSub.OrdersSubs, "'");
				ordersSub.Order = new OrdersSub();
				ordersSub.Order.LstOrderId = lstOrderId;
			}
			else
			{
				if (ordersSub.OrderUnSelected.Count > 0)
				{
					// 注文番号一覧
					lstOrderId = Common.GetListOrderId(ordersSub.OrderUnSelected, "'");
					ordersSub.Order.LstOrderIdUnCheck = lstOrderId;
				}
			}

			string csvExport = UnitOfWork.Orders.DownloadCSV(ordersSub.Order, isDownloadAll);

			return csvExport;
		}
		#endregion

		#region 注文更新
		/// <summary>
		/// 注文更新
		/// </summary>
		/// <param name="ordersSubs"></param>
		/// <param name="isSendMail"></param>
		/// <param name="shopCdBefore"></param>
		/// <param name="isChangeCustReceive"></param>
		/// <param name="lstProductCd"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		public object OrderUdpate(OrdersSub ordersSubs, bool isSendMail, string shopCdBefore, bool isChangeCustReceive, string lstProductCd, string updateUserCd)
		{
			bool isOrderUpdate = false;
			List<OrderDetail> orderDetailsUpd = new List<OrderDetail>();
			List<OrdHistory> ordHistories = new List<OrdHistory>();
			OrdHistory ordHistory = null;
			bool isSendMailUpdSucces = false;

			// 商品詳細一覧
			MProductDetail mProductDetail = new MProductDetail();
			mProductDetail.LstProductCd = lstProductCd;
			List<MProductDetail> mProductDetails = UnitOfWork.MProductDetail.GetAllAsync(mProductDetail).Result;

			// 商品詳細削除一覧
			MProductDetail mProductDtlSkuDelParam = null;
			List<MProductDetail> lstmProductDetailsSkuDelete = null;

			// DBからの注文詳細一覧
			OrderDetail orderDetail = new OrderDetail();
			orderDetail.OrderId = ordersSubs.Order.OrderId;
			List<OrderDetail> lstOrderDetailOld = UnitOfWork.OrderDetail.GetAllAsync(orderDetail).Result;

			// その前にもう更新された場合
			if (ordersSubs.OrderDetails.Count > lstOrderDetailOld.Count)
			{
				return new { error = Message.Common.MSG_ALREADY_UPDATE };
			}

			try
			{
				if (!string.IsNullOrEmpty(shopCdBefore) && ordersSubs.Order.ShopCd != shopCdBefore)
				{
					ordHistory = CreateHistoryUpdateInfoOrder(ordersSubs.Order.OrderId, ordersSubs.Order, shopCdBefore, null, 0, false, updateUserCd);
					ordHistories.Add(ordHistory);
				}

				int quantityChange = 0;
				OrderDetail orderDtlByOrderDtlId = null;
				MProductDetail mProductDetailBySku = null;
				List<string> lstSkuErr = new List<string>();

				for (int i = 0; i < ordersSubs.OrderDetails.Count; i++)
				{
					orderDtlByOrderDtlId = new OrderDetail();
					orderDtlByOrderDtlId = lstOrderDetailOld.FirstOrDefault(x => x.OrderDtlId == ordersSubs.OrderDetails[i].OrderDtlId);

					// その前にもう更新された場合
					if (ordersSubs.OrderDetails[i].UpdateDtTm != orderDtlByOrderDtlId.UpdateDtTm)
					{
						return new { error = Message.Common.MSG_ALREADY_UPDATE };
					}

					// サイズ変更 と 色変更
					if (ordersSubs.OrderDetails[i].Sku != orderDtlByOrderDtlId.Sku)
					{
						mProductDetailBySku = mProductDetails.FirstOrDefault(x => x.Sku == ordersSubs.OrderDetails[i].Sku);

						if (mProductDetailBySku == null)
						{
							lstSkuErr.Add(ordersSubs.OrderDetails[i].Sku);
							return new { error = Message.OrderDetail.MSG_PRODUCT_NOT_EXIST, lstSkuErr = lstSkuErr };
						}

						// 旧サイズの在庫数を増える
						mProductDetails.FirstOrDefault(x => x.Sku == orderDtlByOrderDtlId.Sku).InventoryNumber += orderDtlByOrderDtlId.Quantity;

						// 新サイズの在庫数を減る
						mProductDetails.FirstOrDefault(x => x.Sku == ordersSubs.OrderDetails[i].Sku).InventoryNumber -= ordersSubs.OrderDetails[i].Quantity;

						// 変更サイズの商品数が足りない場合
						if (mProductDetails.FirstOrDefault(x => x.Sku == ordersSubs.OrderDetails[i].Sku).InventoryNumber < 0)
						{
							lstSkuErr.Add(ordersSubs.OrderDetails[i].Sku);
						}

						ordHistory = CreateHistoryOrderUpdate(ordersSubs.OrderDetails[i], orderDtlByOrderDtlId, updateUserCd);
						ordHistories.Add(ordHistory);
					}
					else
					{
						// 数量変更
						if (ordersSubs.OrderDetails[i].Quantity != orderDtlByOrderDtlId.Quantity)
						{
							quantityChange = orderDtlByOrderDtlId.Quantity - ordersSubs.OrderDetails[i].Quantity;

							mProductDetails.FirstOrDefault(x => x.Sku == ordersSubs.OrderDetails[i].Sku).InventoryNumber += quantityChange;
							ordersSubs.OrderDetails[i].UpdateUserCd = updateUserCd;
							if (mProductDetails.FirstOrDefault(x => x.Sku == ordersSubs.OrderDetails[i].Sku).InventoryNumber < 0)
							{
								lstSkuErr.Add(ordersSubs.OrderDetails[i].Sku);
							}
							else
							{
								ordHistory = CreateHistoryUpdateInfoOrder(orderDtlByOrderDtlId.OrderId,
																			null,
																			null,
																			orderDtlByOrderDtlId,
																			ordersSubs.OrderDetails[i].Quantity,
																			false,
																			updateUserCd);
								ordHistories.Add(ordHistory);
							}
						}
					}

					// 更新注文詳細リストに追加する
					orderDetailsUpd.Add(ordersSubs.OrderDetails[i]);
				}

				// 商品の数量が足りない場合
				if (lstSkuErr.Count > 0)
				{
					return new { error = Message.OrderDetail.MSG_WRONG_INFO.Replace("*の", ""), lstSkuErr = lstSkuErr };
				}

				// 商品が削除された場合
				if (ordersSubs.OrderDetailRemoves.Count > 0)
				{
					// mProductDtlSkuDelParam.LstProductCd = lstProductCd;
					//List<MProductDetail> mProductDetails = UnitOfWork.MProductDetail.GetAllAsync(mProductDetail).Result;
					MProductDetail mProductDtlSkuDel = new MProductDetail();

					for (int i = 0; i < ordersSubs.OrderDetailRemoves.Count; i++)
					{
						mProductDtlSkuDelParam = new MProductDetail();
						mProductDtlSkuDelParam.Sku = ordersSubs.OrderDetailRemoves[i].Sku;
						mProductDtlSkuDel = UnitOfWork.MProductDetail.GetAllAsync(mProductDtlSkuDelParam).Result.FirstOrDefault();

						if (mProductDtlSkuDel != null)
						{
							// 旧サイズの在庫数を増える
							mProductDtlSkuDel.InventoryNumber += ordersSubs.OrderDetailRemoves[i].Quantity;
							lstmProductDetailsSkuDelete = new List<MProductDetail>();
							lstmProductDetailsSkuDelete.Add(mProductDtlSkuDel);

							ordersSubs.OrderDetailRemoves[i].DelFlg = true; // DelFlg = 1
							ordersSubs.OrderDetailRemoves[i].UpdateUserCd = updateUserCd;

							// 更新注文詳細リストに追加する
							orderDetailsUpd.Add(ordersSubs.OrderDetailRemoves[i]);

							ordHistory = CreateHistoryUpdateInfoOrder(ordersSubs.OrderDetailRemoves[i].OrderId, null, null, ordersSubs.OrderDetailRemoves[i], 0, false, updateUserCd);
							ordHistories.Add(ordHistory);
						}

						// 旧サイズの在庫数を増える
						//mProductDetails.FirstOrDefault(x => x.Sku == ordersSubs.OrderDetailRemoves[i].Sku).InventoryNumber += ordersSubs.OrderDetailRemoves[i].Quantity;
						
					}
				}

				MAddress mAddressUpd = new MAddress();
				MCustRecieve mCustRecieveUpd = new MCustRecieve();
				// 商品受取人の情報を更新する
				if (ordersSubs.Order.ReceiveWay == ParamConst.ReceiveWay.SHIPPING_CD && isChangeCustReceive)
				{
					// DBから商品受取人の情報を取得する
					MCustrecieveSub mCustrecieveSub = new MCustrecieveSub();
					mCustrecieveSub.OrderId = ordersSubs.Order.OrderId;
					MCustrecieveSub mCustRecieveOld = UnitOfWork.MCustRecieve.GetAllSubAsync(mCustrecieveSub).Result;

					if (ordersSubs.Order.UpdateDtTmCustRecieve != mCustRecieveOld.UpdateDtTm
						|| ordersSubs.Order.UpdateDtTmAddress != mCustRecieveOld.UpdateDtTmAddress)
					{
						return new { error = Message.Common.MSG_ALREADY_UPDATE };
					}

					mAddressUpd = AddressMCustReceiveUpdate(ordersSubs.Order, mCustRecieveOld, updateUserCd);
					mCustRecieveUpd = MCustReceiveUpdate(ordersSubs.Order, mCustRecieveOld, updateUserCd);

					// 履歴
					ordHistory = CreateHistoryUpdateInfoOrder(ordersSubs.Order.OrderId, null, null, null, 0, isChangeCustReceive, updateUserCd);
					ordHistories.Add(ordHistory);
				}

				using (var transactionScope = new TransactionScope())
				{
					if (lstmProductDetailsSkuDelete != null)
					{
						mProductDetails.AddRange(lstmProductDetailsSkuDelete);
					}

					// 商品詳細を更新する
					bool isProductDetailUpd = UnitOfWork.MProductDetail.UpdateAsync(mProductDetails).Result;

					if (!isProductDetailUpd)
					{
						return new { isOrderUpdate = isOrderUpdate };
					}

					// 注文詳細を更新する
					bool isOrderDetailsUpd = UnitOfWork.OrderDetail.UpdateAsync(orderDetailsUpd).Result;

					if (!isOrderDetailsUpd)
					{
						return new { isOrderUpdate = isOrderUpdate };
					}

					// 注文更新
					List<Orders> orders = new List<Orders>();
					ordersSubs.Order.UpdateUserCd = updateUserCd;
					orders.Add(ordersSubs.Order);
					bool isOrdersUpd = UnitOfWork.Orders.UpdateAsync(orders).Result;

					if (!isOrdersUpd)
					{
						return new { isOrderUpdate = isOrderUpdate };
					}

					if (ordersSubs.Order.ReceiveWay == ParamConst.ReceiveWay.SHIPPING_CD && isChangeCustReceive)
					{
						// 商品受取人の住所を更新する
						List<MAddress> mAddresses = new List<MAddress>();
						mAddresses.Add(mAddressUpd);

						bool isAddressUpd = UnitOfWork.MAddress.UpdateAsync(mAddresses).Result;

						if (!isAddressUpd)
						{
							return new { isOrderUpdate = isOrderUpdate };
						}

						// 商品受取人の情報を更新する
						List<MCustRecieve> mCustRecieves = new List<MCustRecieve>();
						mCustRecieves.Add(mCustRecieveUpd);

						bool isCustRecieveUpd = UnitOfWork.MCustRecieve.UpdateAsync(mCustRecieves).Result;

						if (!isCustRecieveUpd)
						{
							return new { isOrderUpdate = isOrderUpdate };
						}
					}

					isOrderUpdate = true;
					transactionScope.Complete();
				}

				if (isSendMail)
				{
					isSendMailUpdSucces = SendUpdateOrderMail(ordersSubs.Order, ordersSubs.OrderDetails, updateUserCd);
				}

				// 注文更新履歴を追加する
				UnitOfWork.OrdHistory.AddListAsync(ordHistories);

				return new { isOrderUpdate = isOrderUpdate, isSendMailUpdSucces = isSendMailUpdSucces };
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return new { isOrderUpdate = isOrderUpdate };
			}
		}
		#endregion

		#region 予約状態更新
		/// <summary>
		/// 予約状態更新
		/// </summary>
		/// <param name="ordersSub"></param>
		/// <param name="lastStatus"></param>
		/// <returns></returns>
		public object UpdateStatusTransferOrder(OrdersSub ordersSub, string lastStatus)
		{
			string errorMsg = string.Empty;
			bool isStatusUpdate = false;
			bool isSendMailSuccess = false;

			Orders orders = new Orders();
			orders.OrderId = ordersSub.OrderId;
			List<Orders> ordersGetDb = UnitOfWork.Orders.GetAllAsync(orders).Result;

			if (ordersGetDb != null && ordersGetDb.Count > 0)
			{
				if (ordersSub.UpdateDtTm != ordersGetDb[0].UpdateDtTm)
				{
					return new { error = Message.Common.MSG_ALREADY_UPDATE };
				}

				try
				{
					List<Orders> ordersUpdStatus = new List<Orders>();
					ordersUpdStatus.Add((Orders)ordersSub);

					isStatusUpdate = UnitOfWork.Orders.UpdateAsync(ordersUpdStatus).Result;

					if (isStatusUpdate)
					{
						// 注文更新履歴を追加する
						OrdHistory ordHistory = new OrdHistory();
						ordHistory.OrderId = ordersSub.OrderId;
						ordHistory.HistType = ParamConst.Histype.UPDATE_STATUS;
						ordHistory.LastStatus = lastStatus;
						ordHistory.UpdatedStatus = ordersSub.Status;
						ordHistory.Department = ParamConst.Department.ADMIN;
						ordHistory.CreateUserCd = ordersSub.UpdateUserCd;
						ordHistory.UpdateUserCd = ordersSub.UpdateUserCd;
						UnitOfWork.OrdHistory.AddAsync(ordHistory);

						if (ordersSub.Status == ParamConst.ReserveStatus.PAID)
						{
							// 入金完了メール送信
							isSendMailSuccess = SendMailFinish(ordersSub);
						}
					}
				}
				catch (Exception e)
				{
					Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				}
			}
			return new { isStatusUpdate = isStatusUpdate, isSendMailSuccess = isSendMailSuccess };
		}
		#endregion

		#region 入金完了メール送信
		/// <summary>
		/// 入金完了メール送信
		/// </summary>
		/// <param name="ordersSub"></param>
		/// <returns></returns>
		public bool SendMailFinish(OrdersSub ordersSub)
		{
			string path_ImgBarCd = string.Empty;
			List<SendMail> lstSendMail = new List<SendMail>();
			SendMail sendMail = null;
			List<string> lstEmailTo = new List<string>();
			List<string> lstUrlImgBarCd = new List<string>();
			bool isSendMailFinishedSucces = false;

			List<MBank> lstMBank = null;
			if (ordersSub.ReceiveWay == ParamConst.ReceiveWay.SHIPPING_CD && ordersSub.PaymentWay == ParamConst.PaymentWay.TRANSFER_CD)
			{
				MBank mBank = new MBank();
				mBank.BussinessCd = ordersSub.BussinessCd;
				lstMBank = UnitOfWork.MBank.GetAllAsync(mBank).Result;
			}

			// メールテンプレート一覧取得
			MailTemplate mailTemplate = new MailTemplate();
			mailTemplate.Type = ParamConst.MailType.MAIL_FINISH;
			mailTemplate.BussinessCd = ordersSub.BussinessCd;
			List<MailTemplate> lstMailTemplate = UnitOfWork.MailTemplate.GetAllAsync(mailTemplate).Result;

			if (lstMailTemplate != null)
			{
				path_ImgBarCd = Common.GetPathImageBarCd(ordersSub.BussinessName, ParamConst.PATH_BAR_CODE_IMAGE);

				OrderDetail orderDetail = new OrderDetail();
				orderDetail.OrderId = ordersSub.OrderId;
				List<OrderDetail> lstOrderDetail = UnitOfWork.OrderDetail.GetAllAsync(orderDetail).Result;

				MCustSub mCustSub = ConvertObjectMCust(ordersSub);

				MCustrecieveSub mCustrecieveSub = null;
				if (ordersSub.ReceiveWay == ParamConst.ReceiveWay.SHIPPING_CD && ordersSub.PaymentWay == ParamConst.PaymentWay.PAY_IN_SHOP_CD)
				{
					MCustrecieveSub mCustrecieve = new MCustrecieveSub();
					mCustrecieve.OrderId = ordersSub.OrderId;
					mCustrecieveSub = UnitOfWork.MCustRecieve.GetAllSubAsync(mCustrecieve).Result;
				}
				else
				{
					mCustrecieveSub = ConvertObjectMCustrecieve(ordersSub);
				}

				for (int i = 0; i < lstMailTemplate.Count; i++)
				{
					sendMail = Common.CreateMail(ordersSub, lstOrderDetail, mCustSub, mCustrecieveSub, lstMailTemplate[i], ordersSub.UpdateUserCd, lstMBank != null ? lstMBank[0] : null);

					lstSendMail.Add(sendMail);
					lstEmailTo.Add(lstMailTemplate[i].SendTo == ParamConst.SendTo.SENDTO_CUSTOMER ? ordersSub.MailAddress : null);
					lstUrlImgBarCd.Add(path_ImgBarCd + "/" + ordersSub.OrderId + ".jpg");
				}

				MailSetting mailSetting = new MailSetting();
				mailSetting.BussinessCd = ordersSub.BussinessCd;
				List<MailSetting> lstMailSetting = UnitOfWork.MailSetting.GetAllAsync(mailSetting).Result;

				if (lstMailSetting != null)
				{
					// メール送信
					List<SendMail> sendMailsResult = UnitOfWork.SendMail.SendMail(lstSendMail, lstEmailTo, lstMailSetting[0], lstUrlImgBarCd);
					if (sendMailsResult != null)
					{
						long mailId = 0;
						for (int i = 0; i < sendMailsResult.Count; i++)
						{
							if ((sendMailsResult[i].MailStatus == ParamConst.MailStatus.STATUS_SUCCESS
								|| sendMailsResult[i].MailStatus == ParamConst.MailStatus.STATUS_RESEND)
								&& sendMailsResult[i].SendTo == ParamConst.SendTo.SENDTO_CUSTOMER)
							{
								isSendMailFinishedSucces = true;
							}

							if (sendMailsResult[i].MailId == 0)
							{
								mailId = UnitOfWork.SendMail.AddAsync(sendMailsResult[i]).Result;
								if (mailId != 0)
								{
									sendMailsResult[i].MailId = (int)mailId;
								}
							}
						}

						// 予約履歴をInsertする
						InsertSendMailHistorys(sendMailsResult);
					}
				}
			}
			return isSendMailFinishedSucces;
		}
		#endregion

		#region 注文更新メールを送信する
		/// <summary>
		/// 注文更新メールを送信する
		/// </summary>
		/// <param name="ordersSub"></param>
		/// <param name="orderDetails"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		private bool SendUpdateOrderMail(OrdersSub ordersSub, List<OrderDetail> orderDetails, string updateUserCd)
		{
			string path_ImgBarCd = string.Empty;
			List<SendMail> lstSendMail = new List<SendMail>();
			SendMail sendMail = null;
			List<string> lstEmailTo = new List<string>();
			List<string> lstUrlImgBarCd = new List<string>();
			bool isSendMailUpdSucces = false;

			// メールテンプレート一覧取得
			MailTemplate mailTemplate = new MailTemplate();
			mailTemplate.Type = ParamConst.MailType.MAIL_CHANGE;
			mailTemplate.BussinessCd = ordersSub.BussinessCd;
			List<MailTemplate> lstMailTemplate = UnitOfWork.MailTemplate.GetAllAsync(mailTemplate).Result;

			if (lstMailTemplate != null)
			{
				path_ImgBarCd = Common.GetPathImageBarCd(ordersSub.BussinessName, ParamConst.PATH_BAR_CODE_IMAGE);

				MCustSub mCustSub = ConvertObjectMCust(ordersSub);
				MCustrecieveSub mCustrecieveSub = ConvertObjectMCustrecieve(ordersSub);

				for (int i = 0; i < lstMailTemplate.Count; i++)
				{
					sendMail = Common.CreateMail(ordersSub, orderDetails, mCustSub, mCustrecieveSub, lstMailTemplate[i], updateUserCd);

					lstSendMail.Add(sendMail);
					lstEmailTo.Add(lstMailTemplate[i].SendTo == ParamConst.SendTo.SENDTO_CUSTOMER ? ordersSub.MailAddress : null);
					lstUrlImgBarCd.Add(path_ImgBarCd + "/" + ordersSub.OrderId + ".jpg");
				}

				MailSetting mailSetting = new MailSetting();
				mailSetting.BussinessCd = ordersSub.BussinessCd;
				List<MailSetting> lstMailSetting = UnitOfWork.MailSetting.GetAllAsync(mailSetting).Result;

				if (lstMailSetting != null)
				{
					// メール送信
					List<SendMail> sendMailsResult = UnitOfWork.SendMail.SendMail(lstSendMail, lstEmailTo, lstMailSetting[0], lstUrlImgBarCd);
					if (sendMailsResult != null)
					{
						long mailId = 0;
						for (int i = 0; i < sendMailsResult.Count; i++)
						{
							if ((sendMailsResult[i].MailStatus == ParamConst.MailStatus.STATUS_SUCCESS
								|| sendMailsResult[i].MailStatus == ParamConst.MailStatus.STATUS_RESEND)
								&& sendMailsResult[i].SendTo == ParamConst.SendTo.SENDTO_CUSTOMER)
							{
								isSendMailUpdSucces = true;
							}

							if (sendMailsResult[i].MailId == 0)
							{
								mailId = UnitOfWork.SendMail.AddAsync(sendMailsResult[i]).Result;
								if (mailId != 0)
								{
									sendMailsResult[i].MailId = (int)mailId;
								}
							}
						}

						// 予約履歴をInsertする
						InsertSendMailHistorys(sendMailsResult);
					}
				}
			}

			return isSendMailUpdSucces;
		}
		#endregion

		#region 注文キャンセル時に在庫数を更新する
		/// <summary>
		/// 注文キャンセル時に在庫数を更新する
		/// </summary>
		/// <param name="orderDetails"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		private bool UpdateInventoryCancelOrder(List<OrderDetail> orderDetails, string updateUserCd)
		{
			bool result = false;

			// 商品一覧
			MProductDetail mProductDetail = new MProductDetail();
			List<MProductDetail> mProductDetails = UnitOfWork.MProductDetail.GetAllAsync(mProductDetail).Result;

			if (mProductDetails != null)
			{
				List<OrderDetail> orderDetailsBySku = null;
				for (int i = 0; i < mProductDetails.Count; i++)
				{
					orderDetailsBySku = new List<OrderDetail>();
					orderDetailsBySku = orderDetails.Where(x => x.Sku == mProductDetails[i].Sku).ToList();

					if (orderDetailsBySku.Count > 0)
					{
						for (int j = 0; j < orderDetailsBySku.Count; j++)
						{
							mProductDetails[i].InventoryNumber += orderDetailsBySku[j].Quantity;
						}
						mProductDetails[i].UpdateUserCd = updateUserCd;
					}
				}

				result = UnitOfWork.MProductDetail.UpdateAsync(mProductDetails).Result;
			}

			return result;
		}
		#endregion

		#region 注文キャンセル履歴を追加する
		/// <summary>
		/// 注文キャンセル履歴を追加する
		/// </summary>
		/// <param name="ordersSubs"></param>
		/// <param name="updateUserCd"></param>
		private void InsertCancelOrdersHistorys(List<OrdersSub> ordersSubs, string updateUserCd)
		{
			List<OrdHistory> ordHistories = new List<OrdHistory>();
			OrdHistory ordHistory = null;

			for (int i = 0; i < ordersSubs.Count; i++)
			{
				ordHistory = new OrdHistory();
				ordHistory.OrderId = ordersSubs[i].OrderId;
				ordHistory.HistType = ParamConst.Histype.UPDATE_STATUS;
				ordHistory.Department = ParamConst.Department.ADMIN;
				ordHistory.UpdatedStatus = ParamConst.ReserveStatus.CANCEL;
				ordHistory.UpdateUserCd = updateUserCd;
				ordHistories.Add(ordHistory);
			}

			UnitOfWork.OrdHistory.AddListAsync(ordHistories);
		}
		#endregion

		#region 注文キャンセルメールを送信する
		/// <summary>
		/// 注文キャンセルメールを送信する
		/// </summary>
		/// <param name="ordersSubs"></param>
		/// <param name="orderDetails"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		private List<SendMailError> SendCancelOrderMail(List<OrdersSub> ordersSubs, List<OrderDetail> orderDetails, string updateUserCd)
		{
			string path_ImgBarCd = string.Empty;
			List<SendMail> lstSendMail = new List<SendMail>();
			SendMail sendMail = null;
			List<string> lstEmailTo = new List<string>();
			List<string> lstUrlImgBarCd = new List<string>();
			List<SendMailError> sendMailErrors = new List<SendMailError>();
			SendMailError sendMailError = null;

			// メールテンプレート一覧取得
			MailTemplate mailTemplate = new MailTemplate();
			mailTemplate.Type = ParamConst.MailType.MAIL_CANCEL;
			mailTemplate.BussinessCd = ordersSubs[0].BussinessCd;
			List<MailTemplate> lstMailTemplate = UnitOfWork.MailTemplate.GetAllAsync(mailTemplate).Result;

			if (lstMailTemplate != null)
			{

				List<OrderDetail> orderDetailsByOrdId = null;
				MCustSub mCustSub = null;
				MCustrecieveSub mCustrecieveSub = null;

				for (int i = 0; i < ordersSubs.Count; i++)
				{
					path_ImgBarCd = Common.GetPathImageBarCd(ordersSubs[i].BussinessName, ParamConst.PATH_BAR_CODE_IMAGE);
					orderDetailsByOrdId = new List<OrderDetail>();
					orderDetailsByOrdId = GetOrderDetailByOrdId(orderDetails, ordersSubs[i].OrderId);

					mCustSub = ConvertObjectMCust(ordersSubs[i]);
					mCustrecieveSub = ConvertObjectMCustrecieve(ordersSubs[i]);

					for (int j = 0; j < lstMailTemplate.Count; j++)
					{
						sendMail = Common.CreateMail(ordersSubs[i], orderDetailsByOrdId, mCustSub, mCustrecieveSub, lstMailTemplate[j], updateUserCd);

						lstSendMail.Add(sendMail);
						lstEmailTo.Add(lstMailTemplate[j].SendTo == ParamConst.SendTo.SENDTO_CUSTOMER ? ordersSubs[i].MailAddress : null);
						lstUrlImgBarCd.Add(path_ImgBarCd + "/" + ordersSubs[i].OrderId + ".jpg");
					}
				}

				MailSetting mailSetting = new MailSetting();
				mailSetting.BussinessCd = ordersSubs[0].BussinessCd;
				List<MailSetting> lstMailSetting = UnitOfWork.MailSetting.GetAllAsync(mailSetting).Result;

				if (lstMailSetting != null)
				{
					// メール送信
					List<SendMail> sendMailsResult = UnitOfWork.SendMail.SendMail(lstSendMail, lstEmailTo, lstMailSetting[0], lstUrlImgBarCd);
					if (sendMailsResult != null)
					{
						long mailId = 0;
						List<SendMail> sendMailsUpd = new List<SendMail>();
						for (int i = 0; i < sendMailsResult.Count; i++)
						{
							if (sendMailsResult[i].MailStatus == ParamConst.MailStatus.STATUS_ERROR
								&& sendMailsResult[i].SendTo == ParamConst.SendTo.SENDTO_CUSTOMER)
							{
								sendMailError = new SendMailError();
								sendMailError.OrderId = sendMailsResult[i].OrderId;

								sendMailErrors.Add(sendMailError);
							}

							if (sendMailsResult[i].MailId == 0)
							{
								mailId = UnitOfWork.SendMail.AddAsync(sendMailsResult[i]).Result;
								if (mailId != 0)
								{
									sendMailsResult[i].MailId = (int)mailId;
								}
							}
						}

						// 予約履歴をInsertする
						InsertSendMailHistorys(sendMailsResult);
					}
				}
			}

			return sendMailErrors;
		}
		#endregion

		#region メール送信履歴を追加する
		/// <summary>
		/// メール送信履歴を追加する
		/// </summary>
		/// <param name="sendMailsResult"></param>
		/// <returns></returns>
		private void InsertSendMailHistorys(List<SendMail> sendMailsResult)
		{
			OrdHistory mailHistory = null;
			List<OrdHistory> mailHistorys = new List<OrdHistory>();
			sendMailsResult = sendMailsResult.Where(x => x.SendTo == ParamConst.SendTo.SENDTO_CUSTOMER).ToList();
			for (int i = 0; i < sendMailsResult.Count; i++)
			{
				mailHistory = new OrdHistory();
				mailHistory.OrderId = sendMailsResult[i].OrderId;
				mailHistory.HistType = ParamConst.Histype.SEND_MAIL;
				mailHistory.MailId = sendMailsResult[i].MailId;
				mailHistory.MailType = sendMailsResult[i].MailType;
				mailHistory.MailStatus = sendMailsResult[i].MailStatus;
				mailHistory.ErrorCd = sendMailsResult[i].ErrorCd;
				mailHistory.ErrorDescription = sendMailsResult[i].ErrorDescription;
				mailHistory.Department = ParamConst.Department.ADMIN;
				mailHistory.CreateUserCd = sendMailsResult[i].UpdateUserCd;
				mailHistory.UpdateUserCd = sendMailsResult[i].UpdateUserCd;
				mailHistorys.Add(mailHistory);
			}

			UnitOfWork.OrdHistory.AddListAsync(mailHistorys);
		}
		#endregion

		#region 受取人情報マッピング
		/// <summary>
		/// 受取人情報マッピング
		/// </summary>
		/// <param name="ordersSub"></param>
		/// <returns></returns>
		private MCustrecieveSub ConvertObjectMCustrecieve(OrdersSub ordersSub)
		{
			MCustrecieveSub mCustrecieveSub = new MCustrecieveSub();
			mCustrecieveSub.KanjiFamilyName = ordersSub.KanjiFamilyNameMCustReceive;
			mCustrecieveSub.KanjiFirstName = ordersSub.KanjiFirstNameMCustReceive;
			mCustrecieveSub.KanaFamilyName = ordersSub.KanaFamilyNameMCustReceive;
			mCustrecieveSub.KanaFirstName = ordersSub.KanaFirstNameMCustReceive;
			mCustrecieveSub.ZipCodeDsp = ordersSub.ZipCodeDspMCustReceive;
			mCustrecieveSub.ProvinceName = ordersSub.ProvinceNameMCustReceive;
			mCustrecieveSub.AdrCd1 = ordersSub.AdrCd1MCustReceive;
			mCustrecieveSub.AdrCd2 = ordersSub.AdrCd2MCustReceive;
			mCustrecieveSub.AdrCd3 = ordersSub.AdrCd3MCustReceive;
			mCustrecieveSub.PhoneNumber = ordersSub.PhoneNumberMCustReceive;

			return mCustrecieveSub;
		}
		#endregion

		#region お客様情報マッピング
		/// <summary>
		/// お客様情報マッピング
		/// </summary>
		/// <param name="ordersSub"></param>
		/// <returns></returns>
		private MCustSub ConvertObjectMCust(OrdersSub ordersSub)
		{
			MCustSub mCustSub = new MCustSub();
			mCustSub.KanjiFamilyName = ordersSub.KanjiFamilyName;
			mCustSub.KanjiFirstName = ordersSub.KanjiFirstName;
			mCustSub.KanaFamilyName = ordersSub.KanaFamilyName;
			mCustSub.KanaFirstName = ordersSub.KanaFirstName;
			mCustSub.ZipCodeDsp = ordersSub.ZipCodeDsp;
			mCustSub.ProvinceName = ordersSub.ProvinceName;
			mCustSub.AdrCd1 = ordersSub.AdrCd1;
			mCustSub.AdrCd2 = ordersSub.AdrCd2;
			mCustSub.AdrCd3 = ordersSub.AdrCd3;
			mCustSub.PhoneNumber = ordersSub.PhoneNumber;
			mCustSub.MailAddress = ordersSub.MailAddress;

			return mCustSub;
		}
		#endregion

		#region 注文更新履歴を作成する
		/// <summary>
		/// 注文更新履歴を作成する
		/// </summary>
		/// <param name="orderDetailUpdate"></param>
		/// <param name="orderDetailOld"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		private OrdHistory CreateHistoryOrderUpdate(OrderDetail orderDetailUpdate, OrderDetail orderDetailOld, string updateUserCd)
		{
			OrdHistory ordHistory = new OrdHistory();
			ordHistory.OrderId = orderDetailUpdate.OrderId;
			if (orderDetailUpdate.SizeCd != orderDetailOld.SizeCd
				&& orderDetailUpdate.ColorCd != orderDetailOld.ColorCd
				&& orderDetailUpdate.Quantity != orderDetailOld.Quantity)
			{
				ordHistory.HistType = ParamConst.Histype.CHANGE_COLOR_AND_SIZE_AND_QUANTITY;
				ordHistory.LastQuantity = orderDetailOld.Quantity;
				ordHistory.UpdatedlQuantity = orderDetailUpdate.Quantity;
				ordHistory.LastSize = orderDetailOld.SizeCd;
				ordHistory.UpdatedSize = orderDetailUpdate.SizeCd;
				ordHistory.LastColor = orderDetailOld.ColorCd;
				ordHistory.UpdatedColor = orderDetailUpdate.ColorCd;
			}
			else if (orderDetailUpdate.SizeCd != orderDetailOld.SizeCd
				&& orderDetailUpdate.ColorCd != orderDetailOld.ColorCd)
			{
				ordHistory.HistType = ParamConst.Histype.CHANGE_SIZE_AND_COLOR;
				ordHistory.LastSize = orderDetailOld.SizeCd;
				ordHistory.UpdatedSize = orderDetailUpdate.SizeCd;
				ordHistory.LastColor = orderDetailOld.ColorCd;
				ordHistory.UpdatedColor = orderDetailUpdate.ColorCd;
			}
			else if (orderDetailUpdate.ColorCd != orderDetailOld.ColorCd
				&& orderDetailUpdate.Quantity != orderDetailOld.Quantity)
			{
				ordHistory.HistType = ParamConst.Histype.CHANGE_COLOR_AND_QUANTITY;
				ordHistory.LastQuantity = orderDetailOld.Quantity;
				ordHistory.UpdatedlQuantity = orderDetailUpdate.Quantity;
				ordHistory.LastColor = orderDetailOld.ColorCd;
				ordHistory.UpdatedColor = orderDetailUpdate.ColorCd;
			}
			else if (orderDetailUpdate.SizeCd != orderDetailOld.SizeCd
				&& orderDetailUpdate.Quantity != orderDetailOld.Quantity)
			{
				ordHistory.HistType = ParamConst.Histype.CHANGE_SIZE_AND_QUANTITY;
				ordHistory.LastQuantity = orderDetailOld.Quantity;
				ordHistory.UpdatedlQuantity = orderDetailUpdate.Quantity;
				ordHistory.LastSize = orderDetailOld.SizeCd;
				ordHistory.UpdatedSize = orderDetailUpdate.SizeCd;
			}
			else if (orderDetailUpdate.ColorCd != orderDetailOld.ColorCd)
			{
				ordHistory.HistType = ParamConst.Histype.CHANGE_COLOR;
				ordHistory.LastColor = orderDetailOld.ColorCd;
				ordHistory.UpdatedColor = orderDetailUpdate.ColorCd;
			}
			else
			{
				ordHistory.HistType = ParamConst.Histype.CHANGE_SIZE;
				ordHistory.LastSize = orderDetailOld.SizeCd;
				ordHistory.UpdatedSize = orderDetailUpdate.SizeCd;
			}

			ordHistory.ProductCd = orderDetailUpdate.ProductCd;
			ordHistory.Size = orderDetailUpdate.SizeCd;
			ordHistory.Color = orderDetailUpdate.ColorCd;
			ordHistory.Department = ParamConst.Department.ADMIN;
			ordHistory.CreateUserCd = updateUserCd;
			ordHistory.UpdateUserCd = updateUserCd;

			return ordHistory;
		}
		#endregion

		#region 商品受取人の住所を更新する
		/// <summary>
		/// 商品受取人の住所を更新する
		/// </summary>
		/// <param name="ordersSub"></param>
		/// <param name="mCustrecieveSub"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		private MAddress AddressMCustReceiveUpdate(OrdersSub ordersSub, MCustrecieveSub mCustrecieveSub, string updateUserCd)
		{
			MAddress mAddressUpd = new MAddress();
			mAddressUpd.AddrId = mCustrecieveSub.AddrId;
			mAddressUpd.ZipCd = ordersSub.ZipCdMCustReceive;
			mAddressUpd.ZipCodeDsp = ordersSub.ZipCodeDspMCustReceive;
			mAddressUpd.Province = ordersSub.ProvinceMCustReceive;
			mAddressUpd.AdrCd1 = ordersSub.AdrCd1MCustReceive;
			mAddressUpd.AdrCd2 = ordersSub.AdrCd2MCustReceive;
			mAddressUpd.AdrCd3 = ordersSub.AdrCd3MCustReceive;
			mAddressUpd.AdrType = mCustrecieveSub.AdrType;
			mAddressUpd.CreateUserCd = ordersSub.CreateUserCdCustRecieve;
			mAddressUpd.UpdateUserCd = updateUserCd;

			return mAddressUpd;
		}
		#endregion

		#region 商品受取人の情報を更新する
		/// <summary>
		/// 商品受取人の情報を更新する
		/// </summary>
		/// <param name="ordersSub"></param>
		/// <param name="mCustrecieveSub"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		private MCustRecieve MCustReceiveUpdate(OrdersSub ordersSub, MCustrecieveSub mCustrecieveSub, string updateUserCd)
		{
			MCustRecieve mCustRecieveUp = new MCustRecieve();
			mCustRecieveUp.RecieveId = mCustrecieveSub.RecieveId;
			mCustRecieveUp.OrderId = ordersSub.OrderId;
			mCustRecieveUp.KanaFamilyName = ordersSub.KanaFamilyNameMCustReceive;
			mCustRecieveUp.KanaFirstName = ordersSub.KanaFirstNameMCustReceive;
			mCustRecieveUp.KanjiFamilyName = ordersSub.KanjiFamilyNameMCustReceive;
			mCustRecieveUp.KanjiFirstName = ordersSub.KanjiFirstNameMCustReceive;
			mCustRecieveUp.PhoneNumber = ordersSub.PhoneNumberMCustReceive;
			mCustRecieveUp.AddrId = mCustrecieveSub.AddrId;
			mCustRecieveUp.CreateUserCd = ordersSub.CreateUserCdCustRecieve;
			mCustRecieveUp.UpdateUserCd = updateUserCd;

			return mCustRecieveUp;
		}
		#endregion

		#region GetListCustReservation
		/// <summary>
		/// GetListCustReservation
		/// </summary>
		/// <param name="orders"></param>
		/// <param name="isGetOneMailSts"></param>
		/// <returns></returns>
		public List<OrdersSub> GetListCustReservation(Orders orders)
		{
			return UnitOfWork.Orders.GetListCustReservation(orders);
		}
		#endregion

		#region 予約更新
		/// <summary>
		/// 予約更新
		/// </summary>
		/// <param name="reservation">予約</param>
		/// <returns></returns>
		public async Task<int> UpdateReservationAsync(Orders reservation)
		{
			int result = ParamConst.ResultType.ERROR;
			try
			{
				Orders ordersPrm = new Orders();
				ordersPrm.OrderId = reservation.OrderId;

				Orders reservationLoad = UnitOfWork.Orders.GetAllAsync(ordersPrm).Result.FirstOrDefault();

				if (reservationLoad.UpdateDtTm != reservation.UpdateDtTm)
				{
					result = ParamConst.ResultType.UPDATED;
					return result;
				}
				List<Orders> lstReservation = new List<Orders>();
				lstReservation.Add(reservation);

				bool isUpdateSuccess = await this.UnitOfWork.Orders.UpdateAsync(lstReservation);
				if (isUpdateSuccess)
				{
					result = ParamConst.ResultType.SUCCESS;

				}
				return result;
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return ParamConst.ResultType.ERROR;
			}
		}
		#endregion

		#region orderIdでorderdetailを取得します
		/// <summary>
		/// orderIdでorderdetailを取得します
		/// </summary>
		/// <param name="orderDetails"></param>
		/// <param name="orderId"></param>
		/// <returns></returns>
		private List<OrderDetail> GetOrderDetailByOrdId(List<OrderDetail> orderDetails, string orderId)
		{
			return orderDetails.Where(x => x.OrderId == orderId).ToList();
		}
		#endregion

		#region orderIdによって送信されたメールを取得します
		/// <summary>
		/// orderIdによって送信されたメールを取得します
		/// </summary>
		/// <param name="sendMails"></param>
		/// <param name="orderId"></param>
		/// <returns></returns>
		private List<SendMail> GetMailSendedByOrdId(List<SendMail> sendMails, string orderId)
		{
			return sendMails.Where(x => x.OrderId == orderId).ToList();
		}
		#endregion

		#region 更新順序の履歴を作成する
		/// <summary>
		/// 更新順序の履歴を作成する
		/// </summary>
		/// <param name="orderId"></param>
		/// <param name="ordersSub"></param>
		/// <param name="shopCdBefore"></param>
		/// <param name="orderDetail"></param>
		/// <param name="quantity"></param>
		/// <param name="isChangeCustReceive"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		private OrdHistory CreateHistoryUpdateInfoOrder(string orderId,
											OrdersSub ordersSub,
											string shopCdBefore,
											OrderDetail orderDetail,
											int quantity,
											bool isChangeCustReceive,
											string updateUserCd)
		{
			OrdHistory ordHistory = new OrdHistory();
			ordHistory.OrderId = orderId;
			if (!string.IsNullOrEmpty(shopCdBefore))
			{
				ordHistory.HistType = ParamConst.Histype.CHANGE_SHOP;
				ordHistory.LastShopCd = shopCdBefore;
				ordHistory.UpdatedShopCd = ordersSub.ShopCd;
			}
			else if (isChangeCustReceive)
			{
				ordHistory.HistType = ParamConst.Histype.CHANGE_CUST_RECEIVE;
			}
			else if (orderDetail != null)
			{
				if (quantity != 0)
				{
					ordHistory.HistType = ParamConst.Histype.CHANGE_QUANTITY;
					ordHistory.LastQuantity = orderDetail.Quantity;
					ordHistory.UpdatedlQuantity = quantity;
				}
				else if (orderDetail.DelFlg)
				{
					ordHistory.HistType = ParamConst.Histype.DELETE_ORDER_DETAIL;
				}
				ordHistory.ProductCd = orderDetail.ProductCd;
				ordHistory.Size = orderDetail.SizeCd;
				ordHistory.Color = orderDetail.ColorCd;
			}

			ordHistory.Department = ParamConst.Department.ADMIN;
			ordHistory.CreateUserCd = updateUserCd;
			ordHistory.UpdateUserCd = updateUserCd;

			return ordHistory;
		}
		#endregion
	}
}