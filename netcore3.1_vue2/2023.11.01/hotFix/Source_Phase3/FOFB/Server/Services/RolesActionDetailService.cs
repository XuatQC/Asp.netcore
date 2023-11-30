using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FOFB.Server.Services
{
	public class RolesActionDetailService : BaseService, IRolesActionDetailService
	{
		public RolesActionDetailService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<long> AddAsync(RolesActionDetail rolesActionDetail)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<RolesActionDetail> rolesActionDetail)
		{
			throw new NotImplementedException();
		}

		public Task<List<RolesActionDetail>> GetAllAsync(RolesActionDetail rolesActionDetail, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.RolesActionDetail.GetAllAsync(rolesActionDetail);
		}

		#region IsUserCanAccessPathByRole
		/// <summary>
		/// IsUserCanAccessPathByRole
		/// </summary>
		/// <param name="actionType"></param>
		/// <param name="actionPath"></param>
		/// <returns></returns>
		public bool IsUserCanAccessPathByRole(string actionType, string actionPath)
		{
			RolesActionDetail rolesActionDetail = new RolesActionDetail();
			rolesActionDetail.ActionUrl = actionPath;

			var roleActionRespone = this.UnitOfWork.RolesActionDetail.GetAllAsync(rolesActionDetail).Result.FirstOrDefault();

			if (roleActionRespone != null && 
				!string.IsNullOrEmpty(roleActionRespone.ActionType) &&
				!string.IsNullOrEmpty(actionType))
			{
				var actPerArray = actionType.Split(",");
				if (actPerArray.Contains(roleActionRespone.ActionType))
				{
					return true;
				}
			}
			return false;
		}
        #endregion

        public Tuple<List<RolesActionDetail>, int> GetDataPagination(int currentpage, int pageSize, RolesActionDetail entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<RolesActionDetail> entity)
		{
			throw new NotImplementedException();
		}
	}
}