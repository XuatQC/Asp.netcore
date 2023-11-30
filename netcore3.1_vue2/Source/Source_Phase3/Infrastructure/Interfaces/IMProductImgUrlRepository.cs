using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IMProductImgUrlRepository : IGenericRepository<MProductImgUrl>
	{
		Task<long> AddListAsync(List<MProductImgUrl> mProductImgUrl);
	}
}
