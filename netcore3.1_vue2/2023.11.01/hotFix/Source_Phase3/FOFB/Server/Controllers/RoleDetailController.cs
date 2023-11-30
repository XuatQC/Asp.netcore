using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FOFB.Server.Controllers
{
    [ApiController]
	public class RoleDetailController : BaseController
	{
		public RoleDetailController(IService service) : base(service)
		{
		}
		/// <summary>
		/// 条件によるRoleDetail一覧を取得する
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetAll")]
		public JsonResult GetAll(int roleId)
		{
			RolesDetail rolesDetail = new RolesDetail();
			rolesDetail.RoleId = roleId;
			List<RolesDetail> lstRoleDetail =  this.Service.RolesDetail.GetAllAsync(rolesDetail).Result;
			return Json(lstRoleDetail);
		}
	}
}