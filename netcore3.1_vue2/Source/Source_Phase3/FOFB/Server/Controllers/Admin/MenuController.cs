using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace FOFB.Server.Controllers
{
	[ApiController]
	public class MenuController : BaseController
	{
		public MenuController(IService service) : base(service)
		{
		}

		/// <summary>
		/// Get list Menu with param
		/// </summary>
		/// <returns></returns>
		[HttpPost("GetMenusByRole")]
		public async Task<IActionResult> GetMenusByRole(int roleId, MUserLogin muserLogin)
		{
			List<Menu> data = await this.Service.Menu.GetMenusByRoleAsync(roleId);
			return Json(data);
		}

		/// <summary>
		/// Get list Menu with param
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetMenusByUser")]
		public async Task<IActionResult> GetMenusByUser(string userCd)
		{
			if (string.IsNullOrEmpty(userCd))
			{
				return null;
			}
			var userPermissisions = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Permissions");
			MuserLoginSub muserLoginSubParam = new MuserLoginSub();
			muserLoginSubParam.UserCd = userCd;
			muserLoginSubParam.Permissions = userPermissisions == null ? "" : userPermissisions.ToString();
			Menu menu = new Menu();
			List<Menu> lstMenu = await this.Service.Menu.GetMenusByUserAsync(muserLoginSubParam, menu, null, null);
			return Json(lstMenu);
		}

		/// <summary>
		/// Get list shop Menu
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetShopMenu")]
		public async Task<IActionResult> GetShopMenu()
		{
			Menu menu = new Menu();
			menu.Department = ParamConst.Department.SHOP;

			List<Menu> lstMenu = await this.Service.Menu.GetAllAsync(menu, null, null);
			return Json(lstMenu);
		}

		/// <summary>
		/// Get list Menu with param
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAll()
		{
			List<Menu> data = await this.Service.Menu.GetAllAsync(null, null, null);
			return Json(data);
		}
	}
}