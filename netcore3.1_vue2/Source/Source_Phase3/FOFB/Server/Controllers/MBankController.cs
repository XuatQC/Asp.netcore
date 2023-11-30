using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Controllers
{
	[ApiController]
	public class MBankController : BaseController
	{
		public MBankController(IService service) : base(service)
		{
		}

		/// <summary>
		/// 条件によるMbank一覧を取得する
		/// </summary>
		/// <param name="bussinessCd"></param>
		/// <returns></returns>
		[HttpGet("GetListMbank")]
		[AllowAnonymous]
		public async Task<IActionResult> GetListMbank(string bussinessCd)
		{
			MBank mbank = new MBank();
			mbank.BussinessCd = bussinessCd;
			List<MBank> lstMbank = await Service.MBank.GetAllAsync(mbank);
			return Json(lstMbank);
		}
	}
}
