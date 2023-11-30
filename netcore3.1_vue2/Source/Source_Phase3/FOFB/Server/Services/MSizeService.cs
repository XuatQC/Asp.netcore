using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace  FOFB.Server.Services
{
	public class MSizeService : BaseService, IMSizeService
	{
		public MSizeService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<long> AddAsync(MSize entity)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<MSize> entity)
		{
			throw new NotImplementedException();
		}

		#region �����ɂ��MSize�ꗗ���擾����
		/// <summary>
		/// �����ɂ��MSize�ꗗ���擾����
		/// </summary>
		/// <param name="mSize"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<MSize>> GetAllAsync(MSize mSize, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.MSize.GetAllAsync(mSize);
		}
		#endregion

		public Tuple<List<MSize>, int> GetDataPagination(int currentpage, int pageSize, MSize entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MSize> entity)
		{
			throw new NotImplementedException();
		}
	}
}