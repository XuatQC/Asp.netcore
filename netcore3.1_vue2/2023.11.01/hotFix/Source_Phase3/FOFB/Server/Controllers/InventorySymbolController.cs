using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Controllers
{
	[ApiController]
	public class InventorySymbolController : BaseController
	{
		public InventorySymbolController(IService service) : base(service)
		{
		}

		/// <summary>
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
		[HttpPost("UpdateInventorySymbol")]
		public async Task<IActionResult> UpdateInventorySymbol(string bussinessCd,string inventoryNumber,string updateUserCd)
		{
			List<InventorySymbol> lstInventorysymbol = new List<InventorySymbol>();
			InventorySymbol inventorysymbol = new InventorySymbol();
			inventorysymbol.BussinessCd = bussinessCd;
			inventorysymbol.UpdateUserCd = updateUserCd;
			inventorysymbol.InventoryNumCheck = inventoryNumber;
			lstInventorysymbol.Add(inventorysymbol);
			bool resultUpdate = await this.Service.InventorySymbol.UpdateAsync(lstInventorysymbol);
			return Json(resultUpdate);
		}
	}
}