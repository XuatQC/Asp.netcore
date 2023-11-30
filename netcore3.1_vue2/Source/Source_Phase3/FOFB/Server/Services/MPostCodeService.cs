using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services
{
	public class MPostCodeService : BaseService, IMPostCodeService
	{
		public MPostCodeService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#region ğŒ‚É‚æ‚é—X•Ö”Ô†ˆê——‚ğæ“¾‚·‚é
		/// <summary>
		/// ğŒ‚É‚æ‚é—X•Ö”Ô†ˆê——‚ğæ“¾‚·‚é
		/// </summary>
		/// <param name="mPostCode"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<MPostCode>> GetAllAsync(MPostCode mPostCode, string sortColumnName, string sortType)
		{
			return UnitOfWork.MPostCode.GetAllAsync(mPostCode, sortColumnName, sortType);
		}
		#endregion

		public Tuple<List<MPostCode>, int> GetDataPagination(int currentpage, int pageSize, MPostCode mPostCode, string sortColumnName, string sortType)
		{
			throw new NotImplementedException();
		}
		public Task<long> AddAsync(MPostCode mPostCode)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MPostCode> mPostCodes)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<MPostCode> mPostCodes)
		{
			throw new NotImplementedException();
		}
	}
}
