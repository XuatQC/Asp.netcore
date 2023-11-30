using FOFB.Shared.Entities;
using System;
using System.Collections.Generic;

namespace FOFB.Server.Services.Interfaces
{
	public interface IMailTemplateService : IGenericService<MailTemplate>
	{
		Tuple<List<MailTemplateSub>, int> GetDataMailTemplatesPagination(int currentpage, int pageSize, MailTemplateSub mailTemplateSub, string sortColumnName = null, string sortType = null);
		int UpdateMailTemplate(MailTemplate mailTemplate);
		long CreateMailTemplateByBussinessCd(MBussiness mBussiness, string bussinessCdDuplicate, string updateUserCd);
	}
}
