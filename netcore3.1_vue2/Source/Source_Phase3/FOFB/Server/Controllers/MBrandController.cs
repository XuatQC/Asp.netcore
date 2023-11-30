using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Controllers
{
	[ApiController]
	public class MBrandController : BaseController
	{
		public MBrandController(IService service) : base(service)
		{
		}

		/// <summary>
		/// 業態によるブランド一覧
		/// </summary>
		/// <param name="bussinessCd"></param>
		/// <returns></returns>
		[HttpGet("GetBrandWithBussinessCd")]
		public JsonResult GetBrandWithBussinessCd(string bussinessCd)
		{
			MBrandSub mBrandSub = new MBrandSub();
			mBrandSub.BussinessCd = bussinessCd;

			List<MBrandSub> lstBrand =  this.Service.MBrand.GetAllSubAsync(mBrandSub).Result;
			return Json(lstBrand);
		}
		/// <summary>
		/// 条件によるMBrand一覧を取得する
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetAll")]
		public JsonResult GetAll()
		{
			MBrand mBrand = new MBrand();
			List<MBrand> lstBrand =  this.Service.MBrand.GetAllAsync(mBrand).Result;
			return Json(lstBrand);
		}
		/// <summary>
		///  MBrandを追加する
		/// </summary>
		/// <returns></returns>
		[HttpPost("AddAsync")]
		public JsonResult AddAsync(MBrand mBrand, string bussinessCd)
		{
			long resultInsBrand = this.Service.MBrand.AddAsyncSub(mBrand, bussinessCd);
			return Json(resultInsBrand);
		}
		/// <summary>
		///  サブブランド追加する
		/// </summary>
		/// <returns></returns>
		[HttpPost("AddBrandSubAsync")]
		public JsonResult AddBrandSubAsync(MSubBrand mBrandSub)
		{
			long resultInsBrandSub = this.Service.SubBrand.AddAsync(mBrandSub).Result;
			return Json(resultInsBrandSub);
		}
	}
}