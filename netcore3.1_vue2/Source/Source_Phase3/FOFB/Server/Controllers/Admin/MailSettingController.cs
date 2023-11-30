using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Controllers.Admin
{
	[ApiController]
	public class MailSettingController : BaseController
	{
		public MailSettingController(IService service) : base(service)
		{
		}

		/// <summary>
		/// 検索条件によるメール設定
		/// </summary>
		/// <param name="bussinessCd"></param>
		/// <returns></returns>
		[HttpGet("GetListMailSetting")]
		public async Task<IActionResult> GetListMailSetting(string bussinessCd)
		{
			MailSetting mailSetting = new MailSetting();
			mailSetting.BussinessCd = bussinessCd;
			List<MailSetting> lstMailSetting = await Service.MailSetting.GetAllAsync(mailSetting);
			return Json(lstMailSetting);
		}

		/// <summary>
		/// メール設定更新
		/// </summary>
		/// <param name="mailSetting"></param>
		/// <returns></returns>
		[HttpPost("UpdateMailSetting")]
		public int UpdateMailSetting(MailSetting mailSetting)
		{
			int result = Service.MailSetting.UpdateMailSetting(mailSetting);
			return result;
		}
	}
}
