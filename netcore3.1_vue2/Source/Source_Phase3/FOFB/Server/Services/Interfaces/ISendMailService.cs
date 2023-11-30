using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services.Interfaces
{
	public interface ISendMailService : IGenericService<SendMail>
	{
		Task<SendMail> GetMailBounceError(string mailAddress, string mailTitle, string bussinessCd);
	}
}