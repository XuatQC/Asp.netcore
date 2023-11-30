using FOFB.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IOrdersRepository : IGenericRepository<Orders>
	{
		string GetRsvIdMax(string bussinessCd);
		Task<List<OrdersSub>> GetAllSubAsync(OrdersSub ordersSub, string sortColumnName = null, string sortType = null);
		Tuple<List<OrdersSub>, int> GetDataOrdersPagination(int currentpage, int pageSize, OrdersSub ordersSub, string sortColumnName = null, string sortType = null);
		Tuple<List<OrdersSub>, int> GetDataOrdersHitoryPagination(int currentpage, int pageSize, OrdersSub ordersSub, string sortColumnName = null, string sortType = null);
		string DownloadCSV(OrdersSub ordersSub, bool isDownloadAll);
		List<OrdersSub> GetListCustReservation(Orders orders);
	}
}

