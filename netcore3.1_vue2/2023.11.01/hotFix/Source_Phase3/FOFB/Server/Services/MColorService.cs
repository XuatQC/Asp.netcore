using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services
{
	public class MColorService : BaseService, IMColorService
	{
		public MColorService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<long> AddAsync(MColor entity)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<MColor> entity)
		{
			throw new NotImplementedException();
		}

		#region 条件によるMColor一覧を取得する
		/// <summary>
		/// 条件によるMColor一覧を取得する
		/// </summary>
		/// <param name="mColor"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<MColor>> GetAllAsync(MColor mColor, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.MColor.GetAllAsync(mColor);
		}
		#endregion

		public Tuple<List<MColor>, int> GetDataPagination(int currentpage, int pageSize, MColor entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MColor> entity)
		{
			throw new NotImplementedException();
		}
	}
}