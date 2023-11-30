using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FOFB.Server.Controllers
{
    [ApiController]
	public class MSizeController : BaseController
	{
		public MSizeController(IService service) : base(service)
		{
		}
		/// <summary>
		/// 条件によるMSize一覧を取得する
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetAll")]
		public JsonResult GetAll()
		{
			MSize mSize = new MSize();
			List<MSize> lstSize = Service.MSize.GetAllAsync(mSize, "SortIndex", "ASC").Result;
			return Json(lstSize);
		}
	}
}
