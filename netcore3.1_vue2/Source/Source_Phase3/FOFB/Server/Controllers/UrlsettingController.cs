using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility;

namespace FOFB.Server.Controllers
{
	[ApiController]
	public class UrlsettingController : BaseController
	{
		public UrlsettingController(IService service) : base(service)
		{
		}

		/// <summary>
		/// 条件によるURL一覧を取得する
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		[HttpGet("GetAll")]
		[AllowAnonymous]
		public async Task<IActionResult> GetAll(string url)
		{
			UrlSetting urlsetting = new UrlSetting();
			urlsetting.Url = url;
			List<UrlSetting> data = await this.Service.UrlSetting.GetAllAsync(urlsetting, null, null);
			return Json(data);
		}

		/// <summary>
		/// 条件によるURL一覧を取得する
		/// </summary>
		/// <param name="bussinessCd"></param>
		/// <returns></returns>
		[HttpGet("GetUrlByBussinessCd")]
		public async Task<IActionResult> GetUrlByBussinessCd(string bussinessCd)
		{
			UrlSetting urlsetting = new UrlSetting();
			urlsetting.BussinessCd = bussinessCd;
			List<UrlSetting> data = await this.Service.UrlSetting.GetAllAsync(urlsetting, null, null);
			return Json(data);
		}

		/// <summary>
		/// URLを保存する
		/// </summary>
		/// <param name="urlSetting"></param>
		/// <returns></returns>
		[HttpPost("SaveUrl")]
		public IActionResult SaveUrl(UrlSetting urlSetting)
		{
			if (this.Service.UrlSetting.IsDupplicateUrl(urlSetting))
			{
				string[] errReplaceArr = { "URL" };

				string msgErr = Message.GetMessageInfo(Message.Common.MSG_ALREADY_EXIST, errReplaceArr);
				return Json(new { error = msgErr });
			}
			bool isAddOrUpdateSuccess = this.Service.UrlSetting.AddOrUpdate(urlSetting);
			return Json(isAddOrUpdateSuccess);
		}

	}
}