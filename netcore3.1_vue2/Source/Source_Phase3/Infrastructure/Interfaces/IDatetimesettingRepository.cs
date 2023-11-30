using FOFB.Shared.Entities;
using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
	public interface IDatetimeSettingRepository : IGenericRepository<DatetimeSetting>
	{
		List<DatetimeSetting> GetAll(DatetimeSetting datetimesetting, string sortColumnName = null, string sortType = null);
		bool AddOrUpdate(DatetimeSetting datetimeSetting);

	}
}
