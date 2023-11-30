using FOFB.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IMShopRepository : IGenericRepository<MShop>
	{
		Tuple<List<MShopSub>, int> GetDataMShopSubPagination(int currentpage, int pageSize, MShopSub mShopSub, string sortColumnName = null, string sortType = null);
		Task<long> AddListAsync(List<MShop> mShops);
		Task<long> InsertOrUpdateDataCSV(string sqlQuery);
	}
}