using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace FOFB.Server.Services.Interfaces
{
	public interface IMShopService : IGenericService<MShop>
	{
		Tuple<List<MShopSub>, int> GetDataMShopSubPagination(int currentpage, int pageSize, MShopSub mShopSub, string sortColumnName = null, string sortType = null);
		object InsertShopInfo(MShopSub mShopSub);
		int UpdateShopInfo(MShopSub mShopSub);
		object CheckDataCsv(IFormFileCollection files, string bussinessCd);
		object InsertDataCsv(List<MShopSub> mShopSubs, string lstShopCdUpd);
	}
}
