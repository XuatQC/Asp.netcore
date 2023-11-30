using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Controllers.Admin
{
	[ApiController]
	public class MBrandController : BaseController
	{
		public MBrandController(IService service) : base(service)
		{
		}

		/// <summary>
		/// 条件によるMBrandSub一覧を取得する
		/// </summary>
		/// <param name="bussinessCd"></param>
		/// <returns></returns>
		[HttpGet("GetListBrand")]
		public async Task<IActionResult> GetListBrand(string bussinessCd)
		{
			MBrandSub mBrandSub = new MBrandSub();
			mBrandSub.BussinessCd = bussinessCd;
			List<MBrandSub> lstMBrand = await Service.MBrand.GetAllSubAsync(mBrandSub);
			return Json(lstMBrand);
		}
	}
}
