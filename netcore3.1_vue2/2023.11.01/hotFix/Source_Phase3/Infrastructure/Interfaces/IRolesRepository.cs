using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IRolesRepository : IGenericRepository<Roles>
	{
		Task<List<RolesSub>> GetAllSubAsync(RolesSub rolesSub, string sortColumnName = null, string sortType = null);
	}
}