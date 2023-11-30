using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Controllers
{
	[ApiController]
	public class MProductController : BaseController
	{
		public MProductController(IService service) : base(service)
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
		/// 条件による商品一覧を取得する
		/// </summary>
		/// <param name="bussinessCd"></param>
		/// <param name="isMainImgInList"></param>
		/// <returns></returns>
		[HttpGet("GetListProduct")]
		[AllowAnonymous]
		public async Task<IActionResult> GetListProduct(string bussinessCd, bool isMainImgInList)
		{
			MProductSub mProductSub = new MProductSub();
			mProductSub.BussinessCd = bussinessCd;
			mProductSub.IsMainInListPage = isMainImgInList;
			List<MProductSub> productSub = await this.Service.MProduct.GetAllSubAsync(mProductSub, "P.CreateDtTm", "DESC");
			return Json(productSub);
		}
		/// <summary>
		/// 在庫商品一覧
		/// </summary>
		/// <param name="bussinessCd"></param>
		/// <returns></returns>
		[HttpGet("GetListProductMaintain")]
		public async Task<IActionResult> GetListProductMaintain(string bussinessCd)
		{
			MProductSub mProductSub = new MProductSub();
			mProductSub.BussinessCd = bussinessCd;
			List<MProductSub> productSub = await this.Service.MProduct.GetListProductMaintain(mProductSub);
			return Json(productSub);
		}

		/// <summary>
		/// 詳細画面の商品情報を取得する
		/// </summary>
		/// <param name="productCd"></param>
		/// <returns></returns>
		[HttpGet("GetlistProductForScreenDetail")]
		[AllowAnonymous]
		public List<MProductSub> GetlistProductForScreenDetail(string productCd)
		{
			return this.Service.MProduct.GetlistProductForScreenDetail(productCd);
		}

		/// <summary>
		/// 商品名を取得する
		/// </summary>
		/// <param name="bussinessCd"></param>
		/// <returns></returns>
		[HttpGet("GetProductCd")]
		[AllowAnonymous]
		public Task<List<MProductSub>> GetProductCd(string bussinessCd)
		{
			MProductSub mProductSub = new MProductSub();
			mProductSub.BussinessCd = bussinessCd;
			return this.Service.MProduct.GetAllSubAsync(mProductSub);
		}

		/// <summary>
		/// Productを追加する
		/// </summary>
		/// <returns></returns>
		[HttpPost("AddAsyncSub")]
		public JsonResult AddAsyncSub()
		{
			List<MProductImgUrl> lstImage = JsonConvert.DeserializeObject<List<MProductImgUrl>>(Request.Form["lstImage"]);
			MProductSub mProductSub = JsonConvert.DeserializeObject<MProductSub>(Request.Form["dataProductSub"]);
			string bussinessCd = JsonConvert.DeserializeObject<string>(Request.Form["bussinessCd"]);
			IFormFileCollection files = Request.Form.Files;
			long resInsert = this.Service.MProduct.AddAsyncSub(lstImage, files, mProductSub, bussinessCd);
			return Json(resInsert);
		}
		/// <summary>
		/// Productを更新する
		/// </summary>
		/// <returns></returns>
		[HttpPost("UpdateAsyncSub")]
		public JsonResult UpdateAsyncSub()
		{
			List<MProductImgUrl> lstImage = JsonConvert.DeserializeObject<List<MProductImgUrl>>(Request.Form["lstImage"]);
			MProductSub mProductSub = JsonConvert.DeserializeObject<MProductSub>(Request.Form["dataProductSub"]);
			List<MProductImgUrl> lstMProductImgUrlDelete = JsonConvert.DeserializeObject<List<MProductImgUrl>>(Request.Form["dataImageDelete"]);
			IFormFileCollection files = Request.Form.Files;
			return Json(this.Service.MProduct.UpdateAsyncSub(lstImage, files, mProductSub, lstMProductImgUrlDelete));
		}
		/// <summary>
		/// ccsvファイルデータを読み込む
		/// </summary>
		/// <returns></returns>
		[HttpPost("CheckDataCsv")]
		public JsonResult CheckDataCsv()
		{
			var files = Request.Form.Files;
			string bussinessCd = JsonConvert.DeserializeObject<string>(Request.Form["bussinessCd"]);
			return Json(this.Service.MProduct.CheckDataCsv(files, bussinessCd));
		}
		/// <summary>
		/// csvから商品を追加する
		/// </summary>
		/// <param name="lstProductSub"></param>
		/// <param name="bussinessCd"></param>
		/// <returns></returns>
		[HttpPost("InsertCsv")]
		public JsonResult InsertDataCsv(List<MProductSub> lstProductSub, string bussinessCd)
		{
			long resultIns  = this.Service.MProduct.InsertDataCsv(lstProductSub, bussinessCd);
			return Json(resultIns);
		}
		/// <summary>
		/// CheckProductSameCode
		/// </summary>
		/// <param name="productCd"></param>
		/// <returns></returns>
		[HttpGet("CheckProductSameCode")]
		public JsonResult CheckProductSameCode(string productCd)
		{
			MProduct mProduct = new MProduct();
			mProduct.ProductCd = productCd;

			long resultCount = this.Service.MProduct.GetAllAsync(mProduct).Result.Count;
			return Json(resultCount);
		}
	}
}
