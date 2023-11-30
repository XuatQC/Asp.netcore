using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IMenuRepository : IGenericRepository<Menu>
	{
		Task<List<Menu>> GetMenuByRoleAsync(int roleId);
		Task<List<Menu>> GetMenusByUserAsync(MUserLogin muserLogin, Menu menu, string sortByColumn, string sortType);
	}
}
