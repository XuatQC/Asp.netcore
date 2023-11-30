using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Controllers
{
	[ApiController]
	public class MProductDetailController : BaseController
	{
		public MProductDetailController(IService service) : base(service)
		{
		}
		/// <summary>
		/// 商品画像を取得する
		/// </summary>
		/// <param name="productCd"></param>
		/// <returns></returns>
		[HttpGet("GetImageProduct")]
		[AllowAnonymous]
		public async Task<IActionResult> GetImageProduct(string productCd)
		{
			MProductImgUrl mProductImgUrl = new MProductImgUrl();
			mProductImgUrl.ProductCd = productCd;
			mProductImgUrl.IsMainInDetailPage = true;

			List<MProductImgUrl> lstMProductImgUrl = await this.Service.MProductImgUrl.GetAllAsync(mProductImgUrl, "IsMainInDetailPage");
			return Json(lstMProductImgUrl);
		}

		/// <summary>
		/// 条件によるMProductDetail一覧を取得する
		/// </summary>
		/// <param name="productCd"></param>
		/// <returns></returns>
		[HttpGet("GetAll")]
		[AllowAnonymous]
		public async Task<IActionResult> GetAll(string productCd)
		{
			MProductDetail mProductDetail = new MProductDetail();
			mProductDetail.ProductCd = productCd;
			List<MProductDetail> lstMProductDetail = await this.Service.MProductDetail.GetAllAsync(mProductDetail, "msize.SortIndex,mcolor.SortIndex");
			return Json(lstMProductDetail);
		}
	}
}
