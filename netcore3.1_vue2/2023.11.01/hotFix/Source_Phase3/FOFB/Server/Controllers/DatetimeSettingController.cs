using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Controllers
{
	[ApiController]
	public class DatetimeSettingController : BaseController
	{
		public DatetimeSettingController(IService service) : base(service)
		{
		}
		/// <summary>
		/// 条件による予約日時設定一覧を取得する
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetAll")]
		[AllowAnonymous]
		public async Task<IActionResult> GetAll(string bussinessCd)
		{
			DatetimeSetting datetimeSetting = new DatetimeSetting();
			datetimeSetting.BussinessCd = bussinessCd;
			List<DatetimeSetting> data = await this.Service.DatetimeSetting.GetAllAsync(datetimeSetting);
			return Json(data);
		}

		/// <summary>
		/// 予約番号による予約更新日時取得
		/// </summary>
		/// <param name="bussinessCd"></param>
		/// <returns></returns>
		[HttpGet("IsValidOrderDateTime")]
		[AllowAnonymous]
		public bool IsValidOrderDateTime(string bussinessCd)
		{
			return this.Service.DatetimeSetting.IsOrdStartDtTm(bussinessCd);
		}
		/// <summary>
		/// DateTimeを更新する
		/// </summary>
		/// <param name="bussinessCd"></param>
		/// <param name="datetimeSetting"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		[HttpPost("UpdateDateTime")]
		public async Task<IActionResult> UpdateDateTime(DatetimeSetting datetimeSetting)
		{
			bool isAddOrUpdateSuccess = this.Service.DatetimeSetting.AddOrUpdate(datetimeSetting);
			return Json(isAddOrUpdateSuccess);
		}

	}
}