using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services.Interfaces
{
	public interface IOrdHistoryService : IGenericService<OrdHistory> 
	{
		Task<List<OrdHistorySub>> GetAllSubsync(OrdHistory ordHistory, string sortColumnName = null, string sortType = null);
		Task<long> AddListAsync(List<OrdHistory> ordhistory);
	}
}
