using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IFrontScreenImgUrlRepository : IGenericRepository<FrontScreenImgUrl>
	{
		Task<long> AddListAsync(List<FrontScreenImgUrl> lstFrontScreenImgUrl);
	}
}
