using FOFB.Shared.Entities;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IMCustRecieveRepository : IGenericRepository<MCustRecieve>
	{
		Task<MCustrecieveSub> GetAllSubAsync(MCustrecieveSub mCustRecieve);
	}
}