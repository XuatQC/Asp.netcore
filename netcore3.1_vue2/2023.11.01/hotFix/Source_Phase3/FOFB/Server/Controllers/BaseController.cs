using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Linq;
using System.Security.Claims;
using Utility;

namespace FOFB.Server.Controllers
{
	[Route("api/[controller]")]
	[Route("admin/api/[controller]")]
	[Route("shop/api/[controller]")]
	[Authorize]
	public class BaseController : Controller
	{
		protected IService Service;

		protected BaseController(IService service)
		{
			this.Service = service ?? throw new ArgumentNullException(nameof(service));
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			string userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();

			string remoteIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
			string localIpAddress = HttpContext.Connection.LocalIpAddress?.ToString();

			Log.Logger.Information("▼------" + context.ActionDescriptor.DisplayName + " Start");
			Log.Logger.Information("User agent: " + userAgent);
			Log.Logger.Information("Remote IpAddress: " + remoteIpAddress);
			Log.Logger.Information("Local IpAddress: " + localIpAddress);

			if (context.ActionArguments.Count > 0)
			{
				Log.Logger.Information("------------------Parameter------------------");
				foreach (var arg in context.ActionArguments)
				{
					Log.Logger.Information(arg.Key + ": " + JsonConvert.SerializeObject(arg.Value));
				}
			}

			// Check user infor before request
			HttpContext httpcontext = context.HttpContext;

			var user = httpcontext.User.FindFirst(ClaimTypes.NameIdentifier);
			var userId = (user == null ? string.Empty : user.Value);

			if (!string.IsNullOrEmpty(userId))
			{
				try
				{
					var getsession = HttpContext.Session.GetString("UserLogedin");
					if (string.IsNullOrEmpty(getsession))
					{
						// return respone
						context.Result = new JsonResult(new { norticeMsgId = "SESSION_EXPIRED" });
						Log.Logger.Error("Session timout");
					}
					else
					{
						//get user infor
						var mUserCokie = JsonConvert.DeserializeObject<MuserLoginSub>(httpcontext.Request.Cookies["userData"]);
						if (mUserCokie != null)
						{
							MuserLoginSub muserLoginSubParam = new MuserLoginSub();
							muserLoginSubParam.UserCd = mUserCokie.UserCd;

							// Check if User has been deleted
							var mUserLoaded = this.Service.MUserLogin.GetMuserLoginSub(muserLoginSubParam);
							if (mUserLoaded == null)
							{
								context.Result = new JsonResult(new { msgUserErr = Message.Login.MSG_USER_DELETED });
								Log.Logger.Error("User Deleted: " + Message.Login.MSG_USER_DELETED);
								httpcontext.Session.Remove("userlogedin");
							}
							else
							{
								// Check if is user infor changed
								if (string.IsNullOrEmpty(mUserLoaded.BussinessCd))
								{
									mUserCokie.BussinessCd = string.Empty;
									mUserLoaded.BussinessCd = string.Empty;
								}

								mUserLoaded.AccessToken = string.Empty;
								mUserCokie.AccessToken = string.Empty;

								mUserLoaded.RefreshToken = string.Empty;
								mUserCokie.RefreshToken = string.Empty;

								mUserLoaded.RefreshToken = string.Empty;
								mUserCokie.RefreshToken = string.Empty;

								mUserLoaded.Password = string.Empty;
								mUserCokie.Password = string.Empty;
							}

							if (!Common.ObjDataCompare(mUserCokie, mUserLoaded))
							{
								// return respone to required relogin
								context.Result = new JsonResult(new { norticeMsgId = "USER_CHANGED", updatedUser = mUserLoaded });
								Log.Logger.Error("cookie User:" + JsonConvert.SerializeObject(mUserCokie));
								Log.Logger.Error("loaded User:" + JsonConvert.SerializeObject(mUserLoaded));
								Log.Logger.Error("User changed: USER_CHANGED");
								httpcontext.Session.Remove("userlogedin");
							}

							if (mUserCokie.Department == ParamConst.Department.ADMIN)
							{
								// Check user permission
								if (!ParamConst.LST_COMMON_PATH.Contains(httpcontext.Request.Path.ToString()))
								{
									string[] lstCheckAdminPath = httpcontext.Request.Path.ToString().Split("/");

									if (lstCheckAdminPath.Contains("admin"))
									{
										// Check role can access funtion
										bool isHasPermission =
												this.Service.RolesActionDetail.IsUserCanAccessPathByRole(mUserCokie.Permissions,
															httpcontext.Request.Path);

										if (!isHasPermission)
										{
											// return respone to required relogin
											context.Result = new JsonResult(new { norticeMsgId = "ACCESS_DENIED", msgAccessErr = Message.Login.MSG_IP_ACCESS_DENIE });
											Log.Logger.Error("Path reject access: " + httpcontext.Request.Path.ToString());
											Log.Logger.Error("User Permissions: " + mUserCokie.Permissions);
											httpcontext.Session.Remove("userlogedin");
										}
									}
								}
							}
							else
							{
								// check if shop cannot receive money when expirce date but admin can
								bool isPaymentAble = this.Service.BaseService.IsPaymentAble(mUserLoaded.BussinessCd);
								//bool isDeliveryAble = this.Service.BaseService.IsDeliveryAble(mUserLoaded.BussinessCd);

								if (!isPaymentAble)
								{
									// check if shop login
									if (!this.Service.BaseService.Common.IsValidIp(remoteIpAddress))
									{
										string[] arrreplacemsg = { "ログイン" };
										string errormsg = Message.GetMessageInfo(Message.Common.MSG_OUT_OF_PERIOD, arrreplacemsg);
										context.Result = new JsonResult(new { msgshopaaccesserr = errormsg });
										Log.Logger.Error("Not valid ip: " + errormsg);
										httpcontext.Session.Remove("userlogedin");
									}
								}
							}
						}
					}
				}
				catch (System.Exception ex)
				{
					Log.Logger.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
				}
			}

			Log.Logger.Information("------------------Respone------------------");
			Log.Logger.Information("Status: " + context.HttpContext.Response.StatusCode);
			base.OnActionExecuting(context);
		}

		public override void OnActionExecuted(ActionExecutedContext context)
		{
			base.OnActionExecuted(context);
			if (context.Result != null)
			{
				Log.Logger.Information("Result: " + JsonConvert.SerializeObject(context.Result));
			}
			Log.Logger.Information("▲------" + context.ActionDescriptor.DisplayName + " End");
		}
	}
}
