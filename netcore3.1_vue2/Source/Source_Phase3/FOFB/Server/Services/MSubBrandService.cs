using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Utility;

namespace FOFB.Server.Services
{
	public class MSubBrandService : BaseService, IMSubBrandService
	{
		public MSubBrandService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<long> AddAsync(MSubBrand entity)
		{
			return UnitOfWork.MSubBrand.AddAsync(entity);
		}

		public Task<bool> DeleteAsync(List<MSubBrand> entity)
		{
			throw new NotImplementedException();
		}

		public Task<List<MSubBrand>> GetAllAsync(MSubBrand mSubBrand, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.MSubBrand.GetAllAsync(mSubBrand);
		}

		public Tuple<List<MSubBrand>, int> GetDataPagination(int currentpage, int pageSize, MSubBrand entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MSubBrand> entity)
		{
			throw new NotImplementedException();
		}
	}
}