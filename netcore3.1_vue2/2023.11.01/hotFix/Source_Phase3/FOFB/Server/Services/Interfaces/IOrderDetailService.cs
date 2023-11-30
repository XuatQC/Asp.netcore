using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services.Interfaces
{
	public interface IOrderDetailService : IGenericService<OrderDetail>
	{
		List<OrderDetail> GetLstColor(OrderDetail orderdetail, string propetyOrder = null, string typeOrder = null);
	}
}
