using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IMBrandRepository : IGenericRepository<MBrand>
	{
		long AddAsyncSub(MBrand mBrand, string bussinessCd);
		Task<List<MBrandSub>> GetAllSubAsync(MBrandSub mBrandSub, string sortColumnName = null, string sortType = null);
	}
}