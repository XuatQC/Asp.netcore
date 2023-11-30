using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Controllers
{
	[ApiController]
	public class CommonController : BaseController
	{
		public CommonController(IService service) : base(service)
		{
		}

		/// <summary>
		/// 条件による区分値定義一覧を取得する
		/// </summary>
		/// <param name="kbnCd"></param>
		/// <returns></returns>
		[HttpGet("GetCbbByKbnCd")]
		[AllowAnonymous]
		public async Task<IActionResult> GetCbbByKbnCd(string kbnCd)
		{
			MkbnValue mkbnValue = new MkbnValue();
			mkbnValue.KbnCd = kbnCd;
			List<MkbnValue> lstMkbnValue = await this.Service.MkbnValue.GetAllAsync(mkbnValue);
			return Json(lstMkbnValue);
		}
	}
}
