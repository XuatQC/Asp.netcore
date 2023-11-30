using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FOFB.Server.Controllers.Admin
{
	[ApiController]
	public class MCustController : BaseController
	{
		public MCustController(IService service) : base(service)
		{
		}

		/// <summary>
		/// お客様詳細情報
		/// </summary>
		/// <param name="custId"></param>
		/// <returns></returns>
		[HttpGet("GetMCustInfo")]
		[AllowAnonymous]
		public async Task<IActionResult> GetMCustInfo(int custId)
		{
			MCustSub mCustSub = new MCustSub();
			mCustSub.CustId = custId;
			MCustSub mCustInfo = await Service.MCust.GetAllSubAsync(mCustSub);
			return Json(mCustInfo);
		}

		/// <summary>
		/// お客様情報更新
		/// </summary>
		/// <param name="custInfo"></param>
		/// <returns></returns>
		[HttpPost("UpdateCustInfo")]
		[AllowAnonymous]
		public int UpdateCustInfo(MCustSub custInfo)
		{
			int result = Service.MCust.UpdateCustInfo(custInfo);
			return result;
		}
	}
}
