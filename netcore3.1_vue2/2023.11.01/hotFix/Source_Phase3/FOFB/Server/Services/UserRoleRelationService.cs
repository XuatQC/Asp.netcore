using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services
{
	public class UserRoleRelationService : BaseService, IUserRoleRelationService
	{
		public UserRoleRelationService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<long> AddAsync(UserRoleRelation entity)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<UserRoleRelation> entity)
		{
			throw new NotImplementedException();
		}

		public Task<List<UserRoleRelation>> GetAllAsync(UserRoleRelation entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Tuple<List<UserRoleRelation>, int> GetDataPagination(int currentpage, int pageSize, UserRoleRelation entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<UserRoleRelation> entity)
		{
			throw new NotImplementedException();
		}
	}
}