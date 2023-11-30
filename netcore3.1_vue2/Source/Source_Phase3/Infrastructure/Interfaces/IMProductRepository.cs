using FOFB.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IMProductRepository : IGenericRepository<MProduct>
	{
		Task<List<MProductSub>> GetAllSubAsync(MProductSub mProductSub, string sortColumnName, string sortType);
		Task<List<MProductSub>> GetListProductMaintain(MProductSub mProductSub, string sortColumnName = null, string sortType = null);
		List<MProductSub> GetlistProductForScreenDetail(string productCd);
		Task<long> InsertOrUpdateDataCSV(string sqlQuery);
	}
}
