using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services
{
	public class MkbnValueService : BaseService, IMkbnValueService
	{
		public MkbnValueService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<long> AddAsync(MkbnValue mkbnValue)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<MkbnValue> mkbnValues)
		{
			throw new NotImplementedException();
		}

		#region 条件による区分値定義一覧を取得する
		/// <summary>
		/// 条件による区分値定義一覧を取得する
		/// </summary>
		/// <param name="mkbnvalue"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<MkbnValue>> GetAllAsync(MkbnValue mkbnvalue, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.MkbnValue.GetAllAsync(mkbnvalue, sortColumnName, sortType);
		}
		#endregion

		public Tuple<List<MkbnValue>, int> GetDataPagination(int currentpage, int pageSize, MkbnValue mkbnValue, string sortColumnName, string sortType)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MkbnValue> entity)
		{
			throw new NotImplementedException();
		}
	}
}
