using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IMAdminUserRepository : IGenericRepository<MAdminUser>
	{
		Task<List<MAdminUserSub>> GetAllSubAsync(MAdminUser mAdminUser, string sortColumnName = null, string sortType = null);
		Task<long> AddListAsync(List<MAdminUser> mAdminUsers);
		Task<long> InsertOrUpdateDataCSV(string sqlQuery);
	}

}