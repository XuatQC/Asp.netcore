using FOFB.Shared.Entities;

namespace FOFB.Server.Services.Interfaces
{
	public interface IRolesActionDetailService : IGenericService<RolesActionDetail> 
	{
		bool IsUserCanAccessPathByRole(string actionType, string actionPath);
	}
}