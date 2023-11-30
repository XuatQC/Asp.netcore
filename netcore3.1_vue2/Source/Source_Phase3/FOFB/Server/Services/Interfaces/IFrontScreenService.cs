
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services.Interfaces
{
	public interface IFrontScreenService : IGenericService<FrontScreen>
	{
		Task<List<FrontScreenSub>> GetAllSubAsync(FrontScreenSub frontScreenSub, string sortColumnName, string sortType);
		bool UpdateAsyncSub(List<FrontScreenImgUrl> lstImage, IFormFileCollection files, FrontScreen frontScreen, List<FrontScreenImgUrl> lstImgDelete,bool isProductListScreen);
		long CreateFrontScreenByBussinessCd(MBussiness mBussiness, string bussinessCdDuplicate, string bussinessNameDuplicate, string updateUserCd);
	}
}
