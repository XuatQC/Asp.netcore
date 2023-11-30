using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services
{
	public class MProductDetailService : BaseService, IMProductDetailService
	{
		public MProductDetailService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<long> AddAsync(MProductDetail entity)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<MProductDetail> entity)
		{
			throw new NotImplementedException();
		}

		public Task<List<MProductDetail>> GetAllAsync(MProductDetail entity, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.MProductDetail.GetAllAsync(entity, sortColumnName, sortType);
		}

		public Tuple<List<MProductDetail>, int> GetDataPagination(int currentpage, int pageSize, MProductDetail entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MProductDetail> entity)
		{
			throw new NotImplementedException();
		}
	}
}