using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services
{
	public class OrderDetailService : BaseService, IOrderDetailService
	{
		public OrderDetailService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<long> AddAsync(OrderDetail orderDetail)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<OrderDetail> orderDetails)
		{
			throw new NotImplementedException();
		}

		#region 条件による予約詳細
		/// <summary>
		/// 条件による予約詳細
		/// </summary>
		/// <param name="orderDetail"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<OrderDetail>> GetAllAsync(OrderDetail orderDetail, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.OrderDetail.GetAllAsync(orderDetail, sortColumnName, sortType);
		}
		#endregion

		#region GetLstColor
		/// <summary>
		/// GetLstColor
		/// </summary>
		/// <param name="orderdetail"></param>
		/// <param name="propetyOrder"></param>
		/// <param name="typeOrder"></param>
		/// <returns></returns>
		public List<OrderDetail> GetLstColor(OrderDetail orderdetail, string propetyOrder = null, string typeOrder = null)
		{
			return this.UnitOfWork.OrderDetail.GetLstColor(orderdetail, propetyOrder, typeOrder).Result;
		}
		#endregion

		public Tuple<List<OrderDetail>, int> GetDataPagination(int currentpage, int pageSize, OrderDetail orderDetail, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<OrderDetail> orderDetails)
		{
			throw new NotImplementedException();
		}
	}
}
