using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services
{
	public class OrdHistoryService : BaseService, IOrdHistoryService
	{
		public OrdHistoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}
		public Task<long> AddAsync(OrdHistory ordHistory)
		{
			return UnitOfWork.OrdHistory.AddAsync(ordHistory);
		}

		public Task<bool> DeleteAsync(List<OrdHistory> ordHistories)
		{
			throw new NotImplementedException();
		}

		public Task<List<OrdHistorySub>> GetAllSubsync(OrdHistory ordHistory, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.OrdHistory.GetAllSubAsync(ordHistory, sortColumnName, sortType);
		}

		#region 条件によるOrdHistory一覧を取得する
		/// <summary>
		/// 条件によるOrdHistory一覧を取得する
		/// </summary>
		/// <param name="ordHistory"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<OrdHistory>> GetAllAsync(OrdHistory ordHistory, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.OrdHistory.GetAllAsync(ordHistory, sortColumnName, sortType);
		}
		#endregion

		public Tuple<List<OrdHistory>, int> GetDataPagination(int currentpage, int pageSize, OrdHistory ordHistory, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		#region OrdHistoryを更新する
		/// <summary>
		/// OrdHistoryを更新する
		/// </summary>
		/// <param name="ordHistories"></param>
		/// <returns></returns>
		public Task<bool> UpdateAsync(List<OrdHistory> ordHistories)
		{
			return UnitOfWork.OrdHistory.UpdateAsync(ordHistories);
		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ordhistory"></param>
		/// <returns></returns>
		public Task<long> AddListAsync(List<OrdHistory> ordhistory)
		{
			return UnitOfWork.OrdHistory.AddListAsync(ordhistory);
		}
	}
}
