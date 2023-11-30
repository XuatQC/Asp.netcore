using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IOrdHistoryRepository : IGenericRepository<OrdHistory>
	{
		Task<long> AddListAsync(List<OrdHistory> ordHistory);

		Task<List<OrdHistorySub>> GetAllSubAsync(OrdHistory ordhistory, string sortColumnName = null, string sortType = null);
	}
}

