using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IRolesDetailRepository : IGenericRepository<RolesDetail>
	{
		Task<long> AddListAsync(List<RolesDetail> rolesDetails);
	}
}