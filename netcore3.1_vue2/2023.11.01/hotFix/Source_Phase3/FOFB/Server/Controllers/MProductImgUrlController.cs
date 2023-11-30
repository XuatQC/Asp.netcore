using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Controllers
{
    [ApiController]
	public class MProductImgUrlController : BaseController
	{
		public MProductImgUrlController(IService service) : base(service)
		{
		}
		/// <summary>
		/// 条件によるMProductImgUrl一覧を取得する
		/// </summary>
		/// <param name="productCd"></param>
		/// <returns></returns>
		[HttpGet("GetAll")]
		[AllowAnonymous]
		public async Task<IActionResult> GetAll(string productCd)
		{
			MProductImgUrl mProductImgUrl = new MProductImgUrl();
			mProductImgUrl.ProductCd = productCd;
			List<MProductImgUrl> lstMProductImgUrl = await this.Service.MProductImgUrl.GetAllAsync(mProductImgUrl);
			return Json(lstMProductImgUrl);
		}
	}
}