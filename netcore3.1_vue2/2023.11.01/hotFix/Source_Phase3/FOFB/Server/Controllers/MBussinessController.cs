using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Controllers
{
	[ApiController]
	public class MBussinessController : BaseController
	{
		public MBussinessController(IService service) : base(service)
		{
		}

		/// <summary>
		/// ğŒ‚É‚æ‚éMBussinessˆê——‚ğæ“¾‚·‚é
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetAll")]
		[AllowAnonymous]
		public async Task<IActionResult> GetAll()
		{
			List<MBussiness> lstBussinessRespone = await this.Service.MBussiness.GetAllAsync(null, null, null);
			return Json(lstBussinessRespone);
		}
		/// <summary>
		/// ğŒ‚É‚æ‚éMBussinessˆê——‚ğæ“¾‚·‚é
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetAllByBussinessCd")]
		[AllowAnonymous]
		public async Task<IActionResult> GetAllByBussinessCd(string bussinessCd)
		{
			MBussiness mBussiness = new MBussiness();
			mBussiness.BussinessCd = bussinessCd;
			List<MBussiness> lstBussinessRespone = await this.Service.MBussiness.GetAllAsync(mBussiness, null, null);
			return Json(lstBussinessRespone);
		}
	}
}