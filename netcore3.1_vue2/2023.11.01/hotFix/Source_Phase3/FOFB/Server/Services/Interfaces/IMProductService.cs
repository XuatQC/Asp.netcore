using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services.Interfaces
{
	public interface IMProductService : IGenericService<MProduct>
	{
		Task<List<MProductSub>> GetAllSubAsync(MProductSub mProductSub, string sortColumnName = null, string sortType = null);
		Task<List<MProductSub>> GetListProductMaintain(MProductSub mProductSub, string sortColumnName = null, string sortType = null);
		List<MProductSub> GetlistProductForScreenDetail(string productCd);
		long AddAsyncSub(List<MProductImgUrl> lstImage, IFormFileCollection files, MProductSub mProductSub,string bussinessCd);
		object UpdateAsyncSub(List<MProductImgUrl> lstImage, IFormFileCollection files, MProductSub mProductSub,List<MProductImgUrl> lstMProductImgUrlDelete);
		object CheckDataCsv(IFormFileCollection files, string bussinessCd);
		long InsertDataCsv(List<MProductSub> lstProductSub, string bussinessCd);
	}
}
