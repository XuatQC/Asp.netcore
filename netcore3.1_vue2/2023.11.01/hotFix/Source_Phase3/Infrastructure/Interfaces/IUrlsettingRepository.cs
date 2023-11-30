using FOFB.Shared.Entities;

namespace Infrastructure.Interfaces
{
	public interface IUrlsettingRepository : IGenericRepository<UrlSetting>
	{
		bool AddOrUpdate(UrlSetting urlsettings);
		bool IsDuplicateUrl(UrlSetting urlsettings);
	}
}
