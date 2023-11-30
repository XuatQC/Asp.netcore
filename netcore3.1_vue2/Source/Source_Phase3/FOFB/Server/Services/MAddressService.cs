using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services
{
	public class MAddressService : BaseService, IMAddressService
	{
		public MAddressService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#region MAddressを追加する
		/// <summary>
		/// MAddressを追加する
		/// </summary>
		/// <param name="mAddress"></param>
		/// <returns></returns>
		public Task<long> AddAsync(MAddress mAddress)
		{
			return UnitOfWork.MAddress.AddAsync(mAddress);
		}
		#endregion

		public Task<bool> DeleteAsync(List<MAddress> mAddresses)
		{
			throw new NotImplementedException();
		}

		public Task<List<MAddress>> GetAllAsync(MAddress mAddress, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Tuple<List<MAddress>, int> GetDataPagination(int currentpage, int pageSize, MAddress mAddress, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MAddress> mAddresses)
		{
			throw new NotImplementedException();
		}
	}
}
