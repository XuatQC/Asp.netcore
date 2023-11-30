using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IFrontScreenRepository : IGenericRepository<FrontScreen>
	{
		Task<List<FrontScreenSub>> GetAllSubAsync(FrontScreenSub entity, string sortColumnName, string sortType);
		Task<long> InsertFrontScreenByCreateBussiness(string sqlQuery);
	}
}
