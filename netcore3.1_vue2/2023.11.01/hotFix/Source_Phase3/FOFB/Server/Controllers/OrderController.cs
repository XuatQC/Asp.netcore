using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Controllers
{
	[ApiController]
	public class OrderController : BaseController
	{
		public OrderController(IService service) : base(service)
		{
		}

		/// <summary>
		/// 条件によるOrdersSub一覧を取得する
		/// </summary>
		/// <param name="ordersSub"></param>
		/// <returns></returns>
		[HttpPost("GetAllSub")]
		[AllowAnonymous]
		public JsonResult GetAllSub(OrdersSub ordersSub)
		{
			List<OrdersSub> ordersSubs = Service.Orders.GetAllSub(ordersSub, "ord.UpdateDtTm", "DESC");
			return Json(ordersSubs);
		}

		/// <summary>
		/// 条件による注文一覧
		/// </summary>
		/// <param name="ordersSub"></param>
		/// <param name="currentpage"></param>
		/// <param name="pageSize"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="isDesc"></param>
		/// <returns></returns>
		[HttpPost("GetDataOrdersPagination")]
		[AllowAnonymous]
		public JsonResult GetDataOrdersPagination(OrdersSub ordersSub, int currentpage, int pageSize, string sortColumnName, bool isDesc)
		{
			Tuple<List<OrdersSub>, int> data = Service.Orders.GetDataOrdersPagination(currentpage, pageSize, ordersSub, sortColumnName, isDesc ? "DESC" : "ASC");
			return Json(new { data = data.Item1, totalData = data.Item2 });
		}

		/// <summary>
		/// 予約詳細を追加する
		/// </summary>
		/// <param name="dataOrderSub"></param>
		/// <returns></returns>
		[HttpPost("InsertOrders")]
		[AllowAnonymous]
		public JsonResult InsertOrders(OrderDetailSub dataOrderSub)
		{
			return Json(Service.Orders.InsertOrders(dataOrderSub));
		}

		/// <summary>
		/// 店舗表示一覧
		/// </summary>
		/// <param name="area2Cd"></param>
		/// <param name="bussinessCd"></param>
		/// <returns></returns>
		[HttpGet("GetListShop")]
		[AllowAnonymous]
		public async Task<IActionResult> GetListShop(string area2Cd, string bussinessCd)
		{
			MShop mShop = new MShop();
			mShop.Area2Cd = int.Parse(area2Cd);
			mShop.BussinessCd = bussinessCd;
			mShop.Status = false;					// 出店
			mShop.DisplayFlg = false;				// 表示
			List<MShop> lstMShop = await Service.MShop.GetAllAsync(mShop);
			return Json(lstMShop);
		}

		/// <summary>
		/// 郵便番号一覧
		/// </summary>
		/// <param name="zipCode"></param>
		/// <returns></returns>
		[HttpGet("GetListMPostCode")]
		[AllowAnonymous]
		public async Task<IActionResult> GetListMPostCode(string zipCode)
		{
			MPostCode mPostCode = new MPostCode();
			mPostCode.ZipCd = zipCode;
			List<MPostCode> lstMPostCode = await Service.MPostCode.GetAllAsync(mPostCode);
			return Json(lstMPostCode);
		}

		/// <summary>
		/// 注文番号による予約詳細
		/// </summary>
		/// <param name="orderId"></param>
		/// <returns></returns>
		[HttpGet("GetOrderDetail")]
		[AllowAnonymous]
		public async Task<IActionResult> GetOrderDetail(string orderId)
		{
			OrderDetail orderDetail = new OrderDetail();
			orderDetail.OrderId = orderId;
			List<OrderDetail> lstOrderDetail = await Service.OrderDetail.GetAllAsync(orderDetail, "mp.ProductCd, ms.SortIndex, mc.SortIndex");
			return Json(lstOrderDetail);
		}

		/// <summary>
		/// 注文メール再送
		/// </summary>
		/// <param name="ordersSubs"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		[HttpPost("ResendOrderMail")]
		[AllowAnonymous]
		public JsonResult ResendOrderMail(List<OrdersSub> ordersSubs, string updateUserCd)
		{
			List<SendMailError> lstOrderIdErr = Service.Orders.ResendOrderMail(ordersSubs, updateUserCd);
			return Json(lstOrderIdErr);
		}

		/// <summary>
		/// 入金リマインドメール送信
		/// </summary>
		/// <param name="ordersSubs"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		[HttpPost("SendRemindMail")]
		[AllowAnonymous]
		public JsonResult SendRemindMail(List<OrdersSub> ordersSubs, string updateUserCd)
		{
			List<SendMailError> lstOrderIdErr = Service.Orders.SendRemindMail(ordersSubs, updateUserCd);
			return Json(lstOrderIdErr);
		}

		/// <summary>
		/// 注文キャンセル
		/// </summary>
		/// <param name="ordersSubs"></param>
		/// <param name="isSendMail"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		[HttpPost("CancelOrder")]
		[AllowAnonymous]
		public JsonResult CancelOrder(List<OrdersSub> ordersSubs, bool isSendMail, string updateUserCd)
		{
			object lstOrderIdErr = Service.Orders.CancelOrder(ordersSubs, isSendMail, updateUserCd);
			return Json(lstOrderIdErr);
		}

		/// <summary>
		/// CSVダウンロード
		/// </summary>
		/// <param name="ordersSub"></param>
		/// <param name="isDownloadAll"></param>
		/// <returns></returns>
		[HttpPost("DownloadCSV")]
		[AllowAnonymous]
		public JsonResult DownloadCSV(OrdersSub ordersSub, bool isDownloadAll)
		{
			string csvExport = Service.Orders.DownloadCSV(ordersSub, isDownloadAll);
			return Json(csvExport);
		}

		/// <summary>
		/// メモ登録・更新
		/// </summary>
		/// <param name="orders"></param>
		/// <returns></returns>
		[HttpPost("SubmitMemo")]
		[AllowAnonymous]
		public JsonResult SubmitMemo(List<Orders> orders)
		{
			bool result = Service.Orders.UpdateAsync(orders).Result;
			return Json(result);
		}

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
		[HttpPost("OrderUdpate")]
		[AllowAnonymous]
		public JsonResult OrderUdpate(OrdersSub ordersSubs, bool isSendMail, string shopCdBefore, bool isChangeCustReceive, string lstProductCd, string updateUserCd)
		{
			object result = Service.Orders.OrderUdpate(ordersSubs, isSendMail, shopCdBefore, isChangeCustReceive, lstProductCd, updateUserCd);
			return Json(result);
		}

		/// <summary>
		/// 予約状態更新
		/// </summary>
		/// <param name="ordersSub"></param>
		/// <param name="lastStatus"></param>
		/// <returns></returns>
		[HttpPost("UpdateStatusTransferOrder")]
		[AllowAnonymous]
		public JsonResult UpdateStatusTransferOrder(OrdersSub ordersSub, string lastStatus)
		{

			object result = Service.Orders.UpdateStatusTransferOrder(ordersSub, lastStatus);
			return Json(result);
		}

		/// <summary>
		/// GetLstColorByPrdSize
		/// </summary>
		/// <param name="productCd"></param>
		/// <param name="sizeCd"></param>
		/// <returns></returns>
		[HttpGet("GetLstColorByPrdSize")]
		[AllowAnonymous]
		public  IActionResult GetLstColorByPrdSize(string productCd, string sizeCd)
		{
			OrderDetail orderDetail = new OrderDetail();
			orderDetail.ProductCd = productCd;
			orderDetail.SizeCd = sizeCd;

			List<OrderDetail> colorLstInDetail = this.Service.OrderDetail.GetLstColor(orderDetail);
			return Json(colorLstInDetail);
		}
	}
}
