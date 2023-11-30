using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface ISendMailRepository : IGenericRepository<SendMail>
	{
		List<SendMail> SendMail(List<SendMail> sendMails, List<string> lstEmailTo, MailSetting mailSetting, List<string> lstUrlImgBarCd);
		Task<SendMail> GetMailBounceError(string mailAddress, string mailTitle, string bussinessCd);
	}
}
