using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IUserRoleRelationRepository : IGenericRepository<UserRoleRelation>
	{
		Task<long> AddListAsync(List<UserRoleRelation> userRoleRelations);

	}
}