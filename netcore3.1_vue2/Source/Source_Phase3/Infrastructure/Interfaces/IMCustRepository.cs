using FOFB.Shared.Entities;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IMCustRepository : IGenericRepository<MCust>
	{
		Task<MCustSub> GetAllSubAsync(MCustSub mCustSub);
	}
}
