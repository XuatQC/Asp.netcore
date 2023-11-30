using FOFB.Shared.Entities;
using System.Threading.Tasks;

namespace FOFB.Server.Services.Interfaces
{
	public interface IDatetimeSettingService : IGenericService<DatetimeSetting>
	{
		bool IsOrdStartDtTm(string bussinessCd);
		bool AddOrUpdate(DatetimeSetting datetimeSetting);

	}
}
