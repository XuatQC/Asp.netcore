using FOFB.Shared;
using FOFB.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IMailTemplateRepository : IGenericRepository<MailTemplate>
	{
		Tuple<List<MailTemplateSub>, int> GetDataMailTemplatesPagination(int currentpage, int pageSize, MailTemplateSub mailTemplateSub, string sortColumnName = null, string sortType = null);
		Task<long> InsertMailTemplateByCreateBussiness(string sqlQuery);
	}
}
