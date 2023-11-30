using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IMenuRoleRelationRepository : IGenericRepository<MenuRoleRelation>
	{
		Task<long> AddListAsync(List<MenuRoleRelation> menuRoleRelations);
	}
}
