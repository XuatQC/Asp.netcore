using FOFB.Shared.Entities;

namespace FOFB.Server.Services.Interfaces
{
	public interface IUrlSettingService : IGenericService<UrlSetting> 
	{
		bool AddOrUpdate(UrlSetting urlsettings);

		bool IsDupplicateUrl(UrlSetting urlsetting);
	}
}
