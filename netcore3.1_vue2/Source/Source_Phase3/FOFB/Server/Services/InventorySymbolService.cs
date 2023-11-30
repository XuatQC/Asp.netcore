using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services
{
	public class InventorySymbolService : BaseService, IInventorySymbolService
	{
		public InventorySymbolService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<long> AddAsync(InventorySymbol entity)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<InventorySymbol> entity)
		{
			throw new NotImplementedException();
		}

		#region 条件による在庫数一覧を取得する
		/// <summary>
		/// 条件による在庫数一覧を取得する
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<InventorySymbol>> GetAllAsync(InventorySymbol entity, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.InventorySymbol.GetAllAsync(entity, sortColumnName, sortType);
		}
		#endregion

		public Tuple<List<InventorySymbol>, int> GetDataPagination(int currentpage, int pageSize, InventorySymbol entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<InventorySymbol> entity)
		{
			return UnitOfWork.InventorySymbol.UpdateAsync(entity);
		}
	}
}
