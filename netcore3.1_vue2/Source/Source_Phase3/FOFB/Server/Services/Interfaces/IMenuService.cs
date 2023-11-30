using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services.Interfaces
{
	public interface IMenuService : IGenericService<Menu>
	{
		Task<List<Menu>> GetMenusByRoleAsync(int roleId);
		Task<List<Menu>> GetMenusByUserAsync(MuserLoginSub muserLogin, Menu menu, string sortByColumn, string sortType);
		bool IsUserCanAccessMenu(string userCd, string href);
	}
}
