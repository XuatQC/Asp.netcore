using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility;

namespace FOFB.Server.Controllers
{
	[ApiController]
	public class FrontScreenController : BaseController
	{
		public FrontScreenController(IService service) : base(service)
		{
		}

		/// <summary>
		/// 条件によるお客様サイトレイアウト一覧を取得する
		/// </summary>
		/// <param name="screenType"></param>
		/// <param name="bussinessCd"></param>
		/// <returns></returns>
		[HttpGet("GetListDetailScreen")]
		[AllowAnonymous]
		public async Task<IActionResult> GetListDetailScreen(string screenType, string bussinessCd)
		{
			FrontScreenSub frontScreenSub = new FrontScreenSub();
			frontScreenSub.ScrType = screenType;
			frontScreenSub.BussinessCd = bussinessCd;

			List<FrontScreenSub> lstFrontScreen = await this.Service.FrontScreen.GetAllSubAsync(frontScreenSub, "DspIndex", "ASC");
			return Json(lstFrontScreen);
		}


		/// <summary>
		/// 商品コードによるお客様サイトレイアウトのテキストデータを取得する
		/// </summary>
		/// <param name="screenType"></param>
		/// <param name="ProductCd"></param>
		/// <returns></returns>
		[HttpGet("GetTextInfoByProduct")]
		[AllowAnonymous]
		public async Task<IActionResult> GetTextInfoByProduct(string screenType, string ProductCd)
		{
			FrontScreen frontScreen = new FrontScreen();
			frontScreen.ScrType = screenType;
			frontScreen.ProductCd = ProductCd;

			List<FrontScreen> lstFrontScreen = await this.Service.FrontScreen.GetAllAsync(frontScreen);
			
			return Json(lstFrontScreen);
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

		/// 在庫マークを取得する
		/// </summary>
		/// <param name="bussinessCd"></param>
		/// <returns></returns>
		[HttpGet("GetInventorySymbol")]
		[AllowAnonymous]
		public async Task<IActionResult> GetInventorySymbol(string bussinessCd)
		{
			InventorySymbol inventorysymbol = new InventorySymbol();
			inventorysymbol.BussinessCd = bussinessCd;
			List<InventorySymbol> lstInventorySymbol = await this.Service.InventorySymbol.GetAllAsync(inventorysymbol);
			return Json(lstInventorySymbol);
		}
		/// <summary>
		/// FrontScreenを更新する
		/// </summary>
		/// <returns></returns>
		[HttpPost("UpdateAsyncSub")]
		public JsonResult UpdateAsyncSub()
		{
			List<FrontScreenImgUrl> lstImage = JsonConvert.DeserializeObject<List<FrontScreenImgUrl>>(Request.Form["lstImage"]);
			FrontScreen frontScreen = JsonConvert.DeserializeObject<FrontScreen>(Request.Form["dataContent"]);
			List<FrontScreenImgUrl> lstImgDelete = JsonConvert.DeserializeObject<List<FrontScreenImgUrl>>(Request.Form["dataImageDelete"]);
			bool isProductListScreen = JsonConvert.DeserializeObject<bool>(Request.Form["isProductListScreen"]);
			IFormFileCollection files = Request.Form.Files;
			bool resUpd = this.Service.FrontScreen.UpdateAsyncSub(lstImage, files, frontScreen, lstImgDelete,isProductListScreen);
			return Json(resUpd);
		}
		/// <summary>
		/// 品番による商品一覧
		/// </summary>
		/// <param name="screenType"></param>
		/// <param name="bussinessCd"></param>
		/// <param name="productCd"></param>
		/// <returns></returns>
		[HttpGet("GetListDetailScreenWithProductCd")]
		[AllowAnonymous]
		public async Task<IActionResult> GetListDetailScreenWithProductCd(string screenType, string bussinessCd, string productCd)
		{
			FrontScreenSub frontScreenSub = new FrontScreenSub();
			frontScreenSub.ScrType = screenType;
			frontScreenSub.BussinessCd = bussinessCd;
			frontScreenSub.ProductCd = productCd;

			List<FrontScreenSub> lstFrontScreen = await this.Service.FrontScreen.GetAllSubAsync(frontScreenSub, "DspIndex", "ASC");
			return Json(lstFrontScreen);
		}

		/// <summary>
		/// FrontScreenを追加する
		/// </summary>
		/// <param name="mBussiness"></param>
		/// <param name="bussinessCdDuplicate"></param>
		/// <param name="bussinessNameDuplicate"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		[HttpPost("CreateFrontScreenByBussinessCd")]
		[AllowAnonymous]
		public JsonResult CreateFrontScreenByBussinessCd(MBussiness mBussiness, string bussinessCdDuplicate, string bussinessNameDuplicate, string updateUserCd)
		{
			long result = Service.FrontScreen.CreateFrontScreenByBussinessCd(mBussiness, bussinessCdDuplicate, bussinessNameDuplicate, updateUserCd);
			return Json(result);
		}
	}
}