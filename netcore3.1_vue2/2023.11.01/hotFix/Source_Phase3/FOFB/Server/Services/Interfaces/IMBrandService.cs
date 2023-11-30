using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services.Interfaces
{
	public interface IMBrandService : IGenericService<MBrand>
	{
		long AddAsyncSub(MBrand mBrand, string bussinessCd);
		Task<List<MBrandSub>> GetAllSubAsync(MBrandSub mBrandSub, string sortColumnName = null, string sortType = null);
	}
}