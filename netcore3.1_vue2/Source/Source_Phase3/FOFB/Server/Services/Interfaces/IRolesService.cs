using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services.Interfaces
{
	public interface IRolesService : IGenericService<Roles>
	{
		Task<List<RolesSub>> GetAllSubAsync(RolesSub rolesSub, string sortColumnName = null, string sortType = null);
		bool InsertRoles(RolesSub rolesSub);
		int UpdateRoles(RolesSub rolesSub);
	}
}
