using FOFB.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services.Interfaces
{
	public interface IOrdersService : IGenericService<Orders>
	{
		object InsertOrders(OrderDetailSub dataOrderSub);
		List<OrdersSub> GetAllSub(OrdersSub ordersSub, string sortColumnName = null, string sortType = null);
		Tuple<List<OrdersSub>, int> GetDataOrdersPagination(int currentpage, int pageSize, OrdersSub ordersSub, string sortColumnName = null, string sortType = null);
		Tuple<List<OrdersSub>, int> GetDataOrdersHitoryPagination(int currentpage, int pageSize, OrdersSub ordersSub, string sortColumnName = null, string sortType = null);
		List<SendMailError> ResendOrderMail(List<OrdersSub> ordersSubs, string updateUserCd);
		List<SendMailError> SendRemindMail(List<OrdersSub> ordersSubs, string updateUserCd);
		object CancelOrder(List<OrdersSub> ordersSubs, bool isSendMail, string updateUserCd);
		string DownloadCSV(OrdersSub ordersSub, bool isDownloadAll);
		object OrderUdpate(OrdersSub ordersSubs, bool isSendMail, string shopCdBefore, bool isChangeCustReceive, string lstProductCd, string updateUserCd);
		List<OrdersSub> GetListCustReservation(Orders orders);
		Task<int> UpdateReservationAsync(Orders reservation);
		object UpdateStatusTransferOrder(OrdersSub ordersSub, string lastStatus);
		bool SendMailFinish(OrdersSub reservation);
	}
}
