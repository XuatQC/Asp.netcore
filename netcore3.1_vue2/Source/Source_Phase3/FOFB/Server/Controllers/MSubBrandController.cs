using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Controllers
{
	[ApiController]
	public class MSubBrandController : BaseController
	{
		public MSubBrandController(IService service) : base(service)
		{
		}

		/// <summary>
		/// 条件によるMSubBrand一覧を取得する
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetAllByBrandCd")]
		public JsonResult GetAllByBrandCd(string brandCd)
		{
			MSubBrand mSubBrand = new MSubBrand();
			mSubBrand.BrandCd = brandCd;
			List<MSubBrand> lstBrand =  this.Service.SubBrand.GetAllAsync(mSubBrand).Result;
			return Json(lstBrand);
		}
	}
}