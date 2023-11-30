using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Controllers
{
	[ApiController]
	public class MAdminUserController : BaseController
	{
		public MAdminUserController(IService service) : base(service)
		{
		}

		/// <summary>
		/// ユーザー一覧
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetData")]
		public async Task<IActionResult> GetData()
		{
			List<MAdminUser> muserLoginRespone = await this.Service.MAdminUser.GetAllAsync(null);

			return Json(muserLoginRespone);
		}
		/// <summary>
		/// 業態コードによるユーザー一覧
		/// </summary>
		/// <param name="bussinessCd"></param>
		/// <returns></returns>
		[HttpGet("GetListUserMaintain")]
		public async Task<IActionResult> GetListUserMaintain(string bussinessCd)
		{
			MAdminUser mAdminUser = new MAdminUser();
			mAdminUser.BussinessCd = bussinessCd;
			List<MAdminUserSub> lstAdminUser = await this.Service.MAdminUser.GetAllSubAsync(mAdminUser);
			return Json(lstAdminUser);
		}
		/// <summary>
		/// MAdminUserを追加する
		/// </summary>
		/// <param name="bussinessCd"></param>
		/// <param name="adminUser"></param>
		/// <returns></returns>
		[HttpPost("AddAsync")]
		public async Task<IActionResult> AddAsync(string bussinessCd, MAdminUserSub adminUser)
		{
			long result =  this.Service.MAdminUser.AddAsyncSub(bussinessCd, adminUser);
			return Json(result);
		}
		/// <summary>
		/// ユーザー編集
		/// </summary>
		/// <param name="adminUser"></param>
		/// <returns></returns>
		[HttpPost("UpdateAsync")]
		public async Task<IActionResult> UpdateAsync(MAdminUserSub adminUser)
		{
			bool result = this.Service.MAdminUser.UpdateAsyncSub(adminUser);
			return Json(result);
		}
		/// <summary>
		/// ユーザー削除
		/// </summary>
		/// <param name="lstAdminUser"></param>
		/// <returns></returns>
		[HttpPost("DeleteAsync")]
		public async Task<IActionResult> DeleteAsync(List<MAdminUserSub> lstAdminUser)
		{
			bool result = this.Service.MAdminUser.DeleteAsyncSub(lstAdminUser);
			return Json(result);
		}
		/// <summary>
		/// csvファイルデータを読み込む
		/// </summary>
		/// <returns></returns>
		[HttpPost("CheckDataCsv")]
		public JsonResult CheckDataCsv()
		{
			var files = Request.Form.Files;
			string bussinessCd = JsonConvert.DeserializeObject<string>(Request.Form["bussinessCd"]);
			return Json(this.Service.MAdminUser.CheckDataCsv(files, bussinessCd));
		}
		/// <summary>
		/// ユーザー追加
		/// </summary>
		/// <param name="mAdminUserSub"></param>
		/// <returns></returns>
		[HttpPost("InsertCsv")]
		public JsonResult InsertDataCsv(MAdminUserSub mAdminUserSub)
		{
			long resultIns = this.Service.MAdminUser.InsertDataCsv(mAdminUserSub.MUserSubmit, mAdminUserSub.ListUserCd);
			return Json(resultIns);
		}
	}
}
