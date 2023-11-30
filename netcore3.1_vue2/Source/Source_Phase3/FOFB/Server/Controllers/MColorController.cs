using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FOFB.Server.Controllers
{
    [ApiController]
	public class MColorController : BaseController
	{
		public MColorController(IService service) : base(service)
		{
		}
		/// <summary>
		/// 条件によるMColor一覧を取得する
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetAll")]
		public JsonResult GetAll()
		{
			MColor mColor = new MColor();
			List<MColor> lstColor = Service.MColor.GetAllAsync(mColor, "SortIndex", "ASC").Result;
			return Json(lstColor);
		}
	}
}
