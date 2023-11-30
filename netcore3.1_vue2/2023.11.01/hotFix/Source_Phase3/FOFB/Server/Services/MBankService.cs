using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services
{
	public class MBankService : BaseService, IMBankService
	{
		public MBankService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

		public Task<long> AddAsync(MBank entity)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<MBank> entity)
		{
			throw new NotImplementedException();
		}

		#region 条件によるMbank一覧を取得する
		/// <summary>
		/// 条件によるMbank一覧を取得する
		/// </summary>
		/// <param name="mBank"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<MBank>> GetAllAsync(MBank mBank, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.MBank.GetAllAsync(mBank, sortColumnName, sortType);
		}
		#endregion

		public Tuple<List<MBank>, int> GetDataPagination(int currentpage, int pageSize, MBank entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MBank> entity)
		{
			throw new NotImplementedException();
		}
	}
}
