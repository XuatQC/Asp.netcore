using FOFB.Shared.Entities;

namespace FOFB.Server.Services.Interfaces
{
	public interface IMailSettingService : IGenericService<MailSetting>
	{
		int UpdateMailSetting(MailSetting mailSetting);
	}
}
