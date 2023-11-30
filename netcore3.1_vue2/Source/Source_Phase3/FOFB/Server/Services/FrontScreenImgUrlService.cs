using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services
{
	public class FrontScreenImgUrlService : BaseService, IFrontScreenImgUrlService
	{
		public FrontScreenImgUrlService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<long> AddAsync(FrontScreenImgUrl entity)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<FrontScreenImgUrl> entity)
		{
			throw new NotImplementedException();
		}

		public Task<List<FrontScreenImgUrl>> GetAllAsync(FrontScreenImgUrl entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Tuple<List<FrontScreenImgUrl>, int> GetDataPagination(int currentpage, int pageSize, FrontScreenImgUrl entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<FrontScreenImgUrl> entity)
		{
			throw new NotImplementedException();
		}
	}
}
