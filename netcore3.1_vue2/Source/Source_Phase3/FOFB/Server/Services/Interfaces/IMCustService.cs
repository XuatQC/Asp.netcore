using FOFB.Shared.Entities;
using System.Threading.Tasks;

namespace FOFB.Server.Services.Interfaces
{
	public interface IMCustService : IGenericService<MCust>
	{
		Task<MCustSub> GetAllSubAsync(MCustSub mCustSub);
		int UpdateCustInfo(MCustSub mCustSub);
	}
}
