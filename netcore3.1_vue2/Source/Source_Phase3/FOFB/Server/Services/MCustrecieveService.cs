using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services
{
	public class MCustRecieveService : BaseService, IMCustRecieveService
	{
		public MCustRecieveService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#region 商品受取人の情報を追加する
		/// <summary>
		/// 商品受取人の情報を追加する
		/// </summary>
		/// <param name="mCustRecieve"></param>
		/// <returns></returns>
		public Task<long> AddAsync(MCustRecieve mCustRecieve)
		{
			return UnitOfWork.MCustRecieve.AddAsync(mCustRecieve);
		}
		#endregion

		public Task<bool> DeleteAsync(List<MCustRecieve> mCustRecieves)
		{
			throw new NotImplementedException();
		}

		public Task<List<MCustRecieve>> GetAllAsync(MCustRecieve mCustRecieve, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Tuple<List<MCustRecieve>, int> GetDataPagination(int currentpage, int pageSize, MCustRecieve mCustRecieve, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MCustRecieve> mCustRecieves)
		{
			throw new NotImplementedException();
		}
	}
}
