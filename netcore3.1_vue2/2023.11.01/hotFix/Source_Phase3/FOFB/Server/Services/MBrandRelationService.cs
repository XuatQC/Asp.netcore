using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services
{
	public class MBrandRelationService : BaseService, IMBrandRelationService
	{
		public MBrandRelationService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<long> AddAsync(MBrandRelation entity)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<MBrandRelation> entity)
		{
			throw new NotImplementedException();
		}

		public Task<List<MBrandRelation>> GetAllAsync(MBrandRelation entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Tuple<List<MBrandRelation>, int> GetDataPagination(int currentpage, int pageSize, MBrandRelation entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MBrandRelation> entity)
		{
			throw new NotImplementedException();
		}
	}
}