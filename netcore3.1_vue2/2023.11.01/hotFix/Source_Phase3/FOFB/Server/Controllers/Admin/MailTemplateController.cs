using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FOFB.Server.Controllers.Admin
{
	[ApiController]
	public class MailTemplateController : BaseController
	{
		public MailTemplateController(IService service) : base(service)
		{
		}

		/// <summary>
		/// 検索条件によるメールテンプレート一覧
		/// </summary>
		/// <param name="mailTemplateSub"></param>
		/// <param name="currentpage"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		[HttpPost("GetDataMailTemplatePagination")]
		public JsonResult GetDataMailTemplatePagination(MailTemplateSub mailTemplateSub, int currentpage, int pageSize)
		{
			Tuple<List<MailTemplateSub>, int> data = Service.Mailtemplate.GetDataMailTemplatesPagination(currentpage, pageSize, mailTemplateSub, "mt.Type", "ASC");
			return Json(new { data = data.Item1, totalData = data.Item2 });
		}

		/// <summary>
		/// メールテンプレート更新
		/// </summary>
		/// <param name="mailTemplate"></param>
		/// <returns></returns>
		[HttpPost("UpdateMailTemplate")]
		public int UpdateMailTemplate(MailTemplate mailTemplate)
		{
			int result = Service.Mailtemplate.UpdateMailTemplate(mailTemplate);
			return result;
		}

		/// <summary>
		/// MailTemplateを追加する
		/// </summary>
		/// <param name="mBussiness"></param>
		/// <param name="bussinessCdDuplicate"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		[HttpPost("CreateMailTemplateByBussinessCd")]
		public JsonResult CreateMailTemplateByBussinessCd(MBussiness mBussiness, string bussinessCdDuplicate, string updateUserCd)
		{
			long result = Service.Mailtemplate.CreateMailTemplateByBussinessCd(mBussiness, bussinessCdDuplicate, updateUserCd);
			return Json(true);
		}
	}
}
