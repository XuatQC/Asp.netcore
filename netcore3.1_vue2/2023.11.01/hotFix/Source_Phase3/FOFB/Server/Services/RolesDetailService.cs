using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services
{
	public class RolesDetailService : BaseService, IRolesDetailService
	{
		public RolesDetailService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<long> AddAsync(RolesDetail entity)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<RolesDetail> entity)
		{
			throw new NotImplementedException();
		}

		public Task<List<RolesDetail>> GetAllAsync(RolesDetail entity, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.RolesDetail.GetAllAsync(entity);
		}

		public Tuple<List<RolesDetail>, int> GetDataPagination(int currentpage, int pageSize, RolesDetail entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<RolesDetail> entity)
		{
			throw new NotImplementedException();
		}
	}
}