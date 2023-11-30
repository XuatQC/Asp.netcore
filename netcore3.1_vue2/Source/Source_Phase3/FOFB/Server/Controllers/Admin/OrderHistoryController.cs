using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Controllers
{
	[ApiController]
	public class OrderHistoryController : BaseController
	{
		public OrderHistoryController(IService service) : base(service)  
		{
		}

		/// <summary>
		///  予約番号によるOrdHistory一覧を取得する
		/// </summary>
		/// <param name="orderId"></param>
		/// <returns></returns>
		[HttpGet("GetOrderHistory")]
		public async Task<IActionResult> GetOrderHistory(string orderId)
		{
			OrdHistory ordHist = new OrdHistory();
			ordHist.OrderId = orderId;

			List<OrdHistorySub> lstOrderDetail = await Service.OrdHistory.GetAllSubsync(ordHist, "DATE_FORMAT(ordHist.UpdateDtTm, '%m/%d/%Y %H:%i')", "DESC");
			return Json(lstOrderDetail);
		}

	}
}
