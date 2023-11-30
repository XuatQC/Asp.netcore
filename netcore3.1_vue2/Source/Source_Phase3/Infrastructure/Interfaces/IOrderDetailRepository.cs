using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IOrderDetailRepository : IGenericRepository<OrderDetail>
	{
		Task<long> AddListAsync(List<OrderDetail> entity);
		Task<List<OrderDetail>> GetLstColor(OrderDetail orderdetail, string propetyOrder, string typeOrder);
	}
}

