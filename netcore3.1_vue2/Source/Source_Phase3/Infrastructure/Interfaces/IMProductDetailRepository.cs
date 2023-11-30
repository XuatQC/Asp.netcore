using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IMProductDetailRepository : IGenericRepository<MProductDetail>
	{
		Task<long> AddListAsync(List<MProductDetail> entity);

	}
}
