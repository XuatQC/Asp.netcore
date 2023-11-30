using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Utility;

namespace FOFB.Server.Controllers
{
	[ApiController]
	public class MShopController : BaseController
	{
		private static LockProvider<int> lockProvider = new LockProvider<int>();
		private string currNetworkIp = string.Empty;

		public MShopController(IService service) : base(service)
		{
		}

		private void setCurrentNetWorkIp()
		{
			currNetworkIp = HttpContext.Connection.RemoteIpAddress?.ToString();
		}

		#region 予約詳細取得
		/// <summary>
		/// 予約詳細取得
		/// </summary>
		/// <returns></returns>
		[HttpPost("GetReserveDetail")]
		public async Task<JsonResult> GetReserveDetailAsync(string barCode, MuserLoginSub muserSub)
		{
			char[] barCodeStartEnd = { '*', '＊' };
			string searchBarCode = barCode.Trim(barCodeStartEnd);
			List<OrderDetail> reservationDetails = null;
			bool isPaymentAble = false;

			MShop mShop = new MShop();
			mShop.BussinessCd = muserSub.BussinessCd;
			OrdersSub custReservation = null;
			Orders orderParam = new Orders();
			orderParam.BarCd = searchBarCode;
			orderParam.BussinessCd = muserSub.BussinessCd;

			if (muserSub.Department == ParamConst.Department.SHOP)
			{
				mShop.ShopCd = muserSub.UserCd;
				orderParam.ShopCd = mShop.ShopCd;
			}

			custReservation = this.GetListCustReservation(orderParam);

			if (custReservation != null)
			{
				if (custReservation.ReceiveWay == ParamConst.ReceiveWay.SHIPPING_CD &&
					custReservation.PaymentWay == ParamConst.PaymentWay.TRANSFER_CD)
				{
					return Json(new { error = Message.Common.MSG_ERROR_NO_DATA });
				}

				OrderDetail orderDetail = new OrderDetail();
				orderDetail.OrderId = custReservation.OrderId;

				reservationDetails = await this.Service.OrderDetail.GetAllAsync(orderDetail, "mp.ProductCd, ms.SortIndex, mc.SortIndex");
				if (custReservation.Status == ParamConst.ReserveStatus.ORDER)
				{
					isPaymentAble = this.Service.BaseService.IsPaymentAble(mShop.BussinessCd);

					// Case expired receive money 
					if (!isPaymentAble)
					{
						if (string.IsNullOrEmpty(currNetworkIp))
						{
							this.setCurrentNetWorkIp();
						}
						// check if is amdin login 
						bool isValidIp = this.Service.BaseService.Common.IsValidIp(currNetworkIp);
						if (isValidIp)
						{
							bool isPaymentStart = this.Service.BaseService.IsAdminPaymentAble(mShop.BussinessCd);
							if (isPaymentStart)
							{
								// Set admin can receive money
								isPaymentAble = true;
							}
						}
					}
				}
				
			}

			return Json(new
			{
				reservation = custReservation,
				reservationDetails = reservationDetails,
				isPaymentAble = isPaymentAble
			});
		}
		#endregion

		#region GetListCustReservation
		/// <summary>
		/// GetListCustReservation
		/// </summary>
		/// <param name="orderId"></param>
		/// <param name="barCode"></param>
		/// <param name="shopCode"></param>
		/// <returns></returns>
		private OrdersSub GetListCustReservation(Orders orderParam)
		{
			if (string.IsNullOrEmpty(orderParam.OrderId) &&
				string.IsNullOrEmpty(orderParam.BarCd) &&
				string.IsNullOrEmpty(orderParam.ShopCd))
			{
				return null;
			}

			// get customer orders
			OrdersSub custReservation = this.Service.Orders.GetListCustReservation(orderParam).FirstOrDefault();
			if (custReservation == null)
			{
				orderParam.OrderId = orderParam.BarCd;
				orderParam.BarCd = null;
				custReservation = this.Service.Orders.GetListCustReservation(orderParam).FirstOrDefault();
			}
			return custReservation;
		}
		#endregion

		#region 予約情報更新
		/// <summary>
		/// 予約情報更新
		/// </summary>
		/// <returns></returns>
		[HttpPost("UpdateReserve")]
		public async Task<JsonResult> UpdateReserve(OrdersSub reservation, string lastStatus)
		{
			// Case expired receive money 
			if (!this.Service.BaseService.IsPaymentAble(reservation.BussinessCd) 
				&& reservation.Status == ParamConst.ReserveStatus.PAID)
			{
				if (string.IsNullOrEmpty(currNetworkIp))
				{
					this.setCurrentNetWorkIp();
				}
				if (!this.Service.BaseService.Common.IsValidIp(currNetworkIp))
				{
					string[] arrReplaceMsg = { "入金" };

					return Json(new { error = Message.GetMessageInfo(Message.Common.MSG_OUT_OF_PERIOD, arrReplaceMsg), isNeedGetNewData = true });
				}
			}

			bool isUpdated = true;
			bool? isSendMailFinishedSucces = null;
			//ResultSendMailError resultSendMailError = null;

			string tmpRsvId = reservation.OrderId.Substring(3, 4);

			int lockId = 0;
			try
			{
				Random _random = new Random();
				string numberRandom = _random.Next(0, 9999).ToString("D4");

				tmpRsvId += numberRandom;

				lockId = Convert.ToInt32(tmpRsvId);

				// lock for currentUserId only
				await lockProvider.WaitAsync(lockId);

				using (var transactionScope = new TransactionScope())
				{
					//reservation.UpdateUserCd = updateUserCd;

					List<Orders> orders = new List<Orders>();
					orders.Add(reservation);
					//int resulUpdateReserve = this.reservationModel.UpdateReservation(reservation);
					int resulUpdateReserve = await this.Service.Orders.UpdateReservationAsync(reservation);

					if (resulUpdateReserve == ParamConst.ResultType.UPDATED)
					{
						return Json(new { error = Message.Common.MSG_ALREADY_UPDATE });
					}
					else if (resulUpdateReserve == ParamConst.ResultType.ERROR)
					{
						isUpdated = false;
					}

					if (isUpdated)
					{
						if (reservation.Status == ParamConst.ReserveStatus.PAID)
						{
							// send mail finish
							isSendMailFinishedSucces = Service.Orders.SendMailFinish(reservation);
						}

						OrdHistory ordHistory = new OrdHistory();
						ordHistory.OrderId = reservation.OrderId;
						ordHistory.HistType = ParamConst.Histype.UPDATE_STATUS;
						ordHistory.LastStatus = lastStatus;
						ordHistory.UpdatedStatus = reservation.Status;
						var department = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "department");
						int departmentParse = department == null? ParamConst.Department.SHOP : Convert.ToInt32(department.Value);
						ordHistory.Department = departmentParse;
						ordHistory.UpdateUserCd = reservation.UpdateUserCd;

						//long histId = this.reserveHistoryModel.InsertReservehistory(reservehistory);
						long histId = await this.Service.OrdHistory.AddAsync(ordHistory);
						isUpdated = (histId > 0);

					}
					transactionScope.Complete();
				}
			}
			catch (Exception ex)
			{
				isUpdated = false;
			}
			finally
			{
				// release the lock
				lockProvider.Release(lockId);
			}
			return Json(new { isUpdated = isUpdated, isSendMailFinishedSucces = isSendMailFinishedSucces });
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
				mailHistory.UpdateUserCd = sendMailsResult[i].UpdateUserCd;
				mailHistorys.Add(mailHistory);
			}
			this.Service.OrdHistory.AddListAsync(mailHistorys);
		}
		#endregion

		/// <summary>
		/// 店舗コードによる店舗
		/// </summary>
		/// <param name="shopCd"></param>
		/// <returns></returns>
		[HttpGet("GetListShop")]
		[AllowAnonymous]
		public async Task<IActionResult> GetListShop(string shopCd)
		{
			MShop mShop = new MShop();
			mShop.ShopCd = shopCd;
			List<MShop> lstMShop = await Service.MShop.GetAllAsync(mShop);
			return Json(lstMShop);
		}

		/// <summary>
		/// 検索条件による店舗一覧
		/// </summary>
		/// <param name="mShopSub"></param>
		/// <param name="currentpage"></param>
		/// <param name="pageSize"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="isDesc"></param>
		/// <returns></returns>
		[HttpPost("GetDataMShopSubPagination")]
		[AllowAnonymous]
		public JsonResult GetDataMShopSubPagination(MShopSub mShopSub, int currentpage, int pageSize, string sortColumnName, bool isDesc)
		{
			Tuple<List<MShopSub>, int> data = Service.MShop.GetDataMShopSubPagination(currentpage, pageSize, mShopSub, sortColumnName, isDesc ? "DESC" : "ASC");
			return Json(new { data = data.Item1, totalData = data.Item2 });
		}

		/// <summary>
		/// 検索条件による予約履歴（店舗）
		/// </summary>
		/// <param name="ordersSub"></param>
		/// <param name="currentpage"></param>
		/// <param name="pageSize"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="isDesc"></param>
		/// <returns></returns>
		[HttpPost("GetDataOrdersHistoryPagination")]
		public JsonResult GetDataOrdersHistoryPagination(OrdersSub ordersSub, int currentpage, int pageSize, string sortColumnName, bool isDesc)
		{
			Tuple<List<OrdersSub>, int> data = Service.Orders.GetDataOrdersHitoryPagination(currentpage, pageSize, ordersSub, sortColumnName, isDesc ? "DESC" : "ASC");
			return Json(new { data = data.Item1, totalData = data.Item2 });
		}

		/// <summary>
		/// 店舗情報を登録する
		/// </summary>
		/// <param name="mShopSub"></param>
		/// <returns></returns>
		[HttpPost("InsertShopInfo")]
		[AllowAnonymous]
		public JsonResult InsertShopInfo(MShopSub mShopSub)
		{
			object result = Service.MShop.InsertShopInfo(mShopSub);
			return Json(result);
		}

		/// <summary>
		/// 店舗情報を更新する
		/// </summary>
		/// <param name="mShopSub"></param>
		/// <returns></returns>
		[HttpPost("UpdateShopInfo")]
		[AllowAnonymous]
		public int UpdateShopInfo(MShopSub mShopSub)
		{
			int result = Service.MShop.UpdateShopInfo(mShopSub);
			return result;
		}

		/// <summary>
		/// csvファイルデータを読み込む
		/// </summary>
		/// <returns></returns>
		[HttpPost("CheckDataCsv")]
		[AllowAnonymous]
		public JsonResult CheckDataCsv()
		{
			var files = Request.Form.Files;
			string bussinessCd = JsonConvert.DeserializeObject<string>(Request.Form["bussinessCd"]);
			object lstMShopSub = Service.MShop.CheckDataCsv(files, bussinessCd);
			return Json(lstMShopSub);
		}

		/// <summary>
		/// CSVから店舗を追加する
		/// </summary>
		/// <param name="mShopSub"></param>
		/// <returns></returns>
		[HttpPost("InsertCsv")]
		[AllowAnonymous]
		public JsonResult InsertDataCsv(MShopSub mShopSub)
		{
			object resultIns = Service.MShop.InsertDataCsv(mShopSub.MShopSubmit, mShopSub.ListShopCd);
			return Json(resultIns);
		}
	}
}
