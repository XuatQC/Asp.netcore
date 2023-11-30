using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services.Interfaces
{
	public interface IMAdminUserService : IGenericService<MAdminUser>
	{
		Task<List<MAdminUserSub>> GetAllSubAsync(MAdminUser mAdminUser, string sortColumnName = null, string sortType = null);
		long AddAsyncSub(string bussinessCd, MAdminUserSub adminUser);
		bool UpdateAsyncSub(MAdminUserSub adminUser);
		bool DeleteAsyncSub(List<MAdminUserSub> adminUser);
		object CheckDataCsv(IFormFileCollection files,string bussinessCd);
		long InsertDataCsv(List<MAdminUserSub> lstProductSub, string lstShopCdUpd);

	}

}