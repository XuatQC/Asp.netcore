using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Utility;

namespace FOFB.Server.Services
{
	public class MCustService : BaseService, IMCustService
	{
		public MCustService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#region MCustを追加する
		/// <summary>
		/// MCustを追加する
		/// </summary>
		/// <param name="mCust"></param>
		/// <returns></returns>
		public Task<long> AddAsync(MCust mCust)
		{
			return UnitOfWork.MCust.AddAsync(mCust);
		}
		#endregion

		public Task<bool> DeleteAsync(List<MCust> mCusts)
		{
			throw new NotImplementedException();
		}

		public Task<List<MCust>> GetAllAsync(MCust mCust, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Tuple<List<MCust>, int> GetDataPagination(int currentpage, int pageSize, MCust mCust, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MCust> mCusts)
		{
			throw new NotImplementedException();
		}

		#region お客様詳細情報
		/// <summary>
		/// お客様詳細情報
		/// </summary>
		/// <param name="mCustSub"></param>
		/// <returns></returns>
		public Task<MCustSub> GetAllSubAsync(MCustSub mCustSub)
		{
			return UnitOfWork.MCust.GetAllSubAsync(mCustSub);
		}
		#endregion

		#region お客様情報更新
		/// <summary>
		/// お客様情報更新
		/// </summary>
		/// <param name="mCustSub"></param>
		/// <returns></returns>
		public int UpdateCustInfo(MCustSub mCustSub)
		{
			int result = ParamConst.ResultType.ERROR;

			MCustSub mCustByCustId = new MCustSub();
			mCustByCustId.CustId = mCustSub.CustId;
			MCustSub mCustSubByCustId = UnitOfWork.MCust.GetAllSubAsync(mCustByCustId).Result;

			try
			{
				if (mCustSubByCustId != null)
				{
					if (mCustSubByCustId.UpdateDtTm == mCustSub.UpdateDtTm)
					{
						using (var transactionScope = new TransactionScope())
						{
							bool resultUpdAdrCust = UpdateMAddress(mCustSub);

							if (!resultUpdAdrCust)
							{
								return result;
							}

							List<MCust> mCusts = new List<MCust>();
							mCusts.Add((MCust)mCustSub);
							bool resultUpdCust = UnitOfWork.MCust.UpdateAsync(mCusts).Result;

							if (resultUpdCust)
							{
								result = ParamConst.ResultType.SUCCESS;
							}
							else
							{
								return result;
							}

							transactionScope.Complete();
						}
					}
					else
					{
						result = ParamConst.ResultType.UPDATED;
					}
				}

				return result;
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return ParamConst.ResultType.ERROR;
			}
		}
		#endregion

		#region お客様住所更新
		/// <summary>
		/// お客様住所更新
		/// </summary>
		/// <param name="mCustSub"></param>
		/// <returns></returns>
		private bool UpdateMAddress(MCustSub mCustSub)
		{
			bool resultUpdAdrCust = false;
			MAddress mAddress = new MAddress();
			mAddress.AddrId = mCustSub.AddrId;
			mAddress.ZipCd = mCustSub.ZipCd;
			mAddress.ZipCodeDsp = mCustSub.ZipCodeDsp;
			mAddress.Province = mCustSub.Province;
			mAddress.AdrCd1 = mCustSub.AdrCd1;
			mAddress.AdrCd2 = mCustSub.AdrCd2;
			mAddress.AdrCd3 = mCustSub.AdrCd3;
			mAddress.AdrType = mCustSub.AdrType;
			mAddress.CreateUserCd = mCustSub.CreateUserCd;
			mAddress.UpdateUserCd = mCustSub.UpdateUserCd;

			List<MAddress> mAddresses = new List<MAddress>();
			mAddresses.Add(mAddress);

			resultUpdAdrCust = UnitOfWork.MAddress.UpdateAsync(mAddresses).Result;
			return resultUpdAdrCust;
		}
		#endregion
	}
}
