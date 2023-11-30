using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services
{
	public class MBussinessService : BaseService, IMBussinessService
	{
		public MBussinessService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<long> AddAsync(MBussiness mBussiness)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<MBussiness> mBussiness)
		{
			throw new NotImplementedException();
		}
		#region ğŒ‚É‚æ‚é‹Æ‘Ôˆê——‚ğæ“¾‚·‚é
		/// <summary>
		/// ğŒ‚É‚æ‚é‹Æ‘Ôˆê——‚ğæ“¾‚·‚é
		/// </summary>
		/// <param name="mBussiness"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<MBussiness>> GetAllAsync(MBussiness mBussiness, string sortColumnName = null, string sortType = null)
		{
			mBussiness = mBussiness ?? new MBussiness();
			return UnitOfWork.MBussiness.GetAllAsync(mBussiness, null, null);
		}
		#endregion
		public Tuple<List<MBussiness>, int> GetDataPagination(int currentpage, int pageSize, MBussiness mBussiness, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MBussiness> mBussiness)
		{
			throw new NotImplementedException();
		}
	}
}