using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility;

namespace FOFB.Server.Controllers
{
	[ApiController]
	public class MuserLoginController : BaseController
	{
		public MuserLoginController(IService service) : base(service)
		{
		}

		/// <summary>
		/// ログイン
		/// </summary>
		/// <returns></returns>
		[HttpPost("Login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login(MUserLogin muserLogin, bool isAutoLogin)
		{
			string currNetworkIp = HttpContext.Connection.RemoteIpAddress?.ToString();

			Tuple<MuserLoginSub, string, List<MkbnValue>> muserLoginRespone = await this.Service.MUserLogin.Login(muserLogin, isAutoLogin, currNetworkIp);

			if (!string.IsNullOrEmpty(muserLoginRespone.Item2))
			{
				return Json(new { error = muserLoginRespone.Item2 });
			}

			// Check if user used old password
			bool isDefaultPw = this.Service.BaseService.Common.MD5Hash(muserLogin.UserCd) == muserLoginRespone.Item1.Password;
			muserLoginRespone.Item1.Password = null;
			HttpContext.Session.SetString("UserLogedin", muserLoginRespone.Item1.UserCd);

			int isShowPopup = muserLoginRespone.Item3[0].NumberValue;
			int isChangePass = muserLoginRespone.Item3[1].NumberValue;

			return Json(new { userData = muserLoginRespone.Item1, isDefaultPw  = isDefaultPw, isShowPopup = isShowPopup, isChangePass = isChangePass });
		}

		#region パスワード変更
		[HttpPost("ChangePassword")]
		/// <summary>
		/// パスワード変更
		/// </summary>
		/// <param name="MUser"></param>
		/// <returns>Result</returns>
		public async Task<JsonResult> ChangePasswordAsync(MUserLogin muser, string oldPassword)
		{
			Tuple<bool, string> changePassRespone = await this.Service.MUserLogin.ChangePassword(muser, oldPassword);

			// if change password Error
			if (!string.IsNullOrEmpty(changePassRespone.Item2))
			{
				return Json(new { error = changePassRespone.Item2 });
			}

			return Json(changePassRespone.Item1);
		}
		#endregion

		/// <summary>
		/// パスワードリセット
		/// </summary>
		/// <param name="password"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		[HttpPost("ResetPassword")]
		public JsonResult ResetPassword(string password, string updateUserCd)
		{
			int resultResetPass = Service.MUserLogin.ResetPassword(password, updateUserCd);

			return Json(resultResetPass);
		}
	}
}
