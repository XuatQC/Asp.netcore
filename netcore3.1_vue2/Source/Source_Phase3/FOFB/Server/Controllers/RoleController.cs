using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FOFB.Server.Controllers
{
	[ApiController]
	public class RoleController : BaseController
	{
		public RoleController(IService service) : base(service)
		{
		}
		/// <summary>
		/// 条件によるRole一覧を取得する
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetAll")]
		public JsonResult GetAll()
		{
			Roles roles = new Roles();
			List<Roles> lstRole =  this.Service.Roles.GetAllAsync(roles).Result;
			return Json(lstRole);
		}

		/// <summary>
		/// 条件によるRole一覧を取得する
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetAllSub")]
		public JsonResult GetAllSub()
		{
			RolesSub rolesSub = new RolesSub();
			List<RolesSub> lstRoleSub = Service.Roles.GetAllSubAsync(rolesSub, "r.RoleId", "ASC").Result;
			return Json(lstRoleSub);
		}

		/// <summary>
		/// 権限を登録する
		/// </summary>
		/// <param name="rolesSub"></param>
		/// <returns></returns>
		[HttpPost("InsertRoles")]
		public bool InsertRoles(RolesSub rolesSub)
		{
			bool result = Service.Roles.InsertRoles(rolesSub);
			return result;
		}

		/// <summary>
		/// 権限を更新する
		/// </summary>
		/// <param name="rolesSub"></param>
		/// <returns></returns>
		[HttpPost("UpdateRoles")]
		public int UpdateRoles(RolesSub rolesSub)
		{
			int result = Service.Roles.UpdateRoles(rolesSub);
			return result;
		}
	}
}