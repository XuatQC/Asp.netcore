using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Utility;

namespace FOFB.Server.Services
{
	public class MBrandService : BaseService, IMBrandService
	{
		public MBrandService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}


		public Task<long> AddAsync(MBrand entity)
		{
			return UnitOfWork.MBrand.AddAsync(entity);
		}

		public Task<bool> DeleteAsync(List<MBrand> entity)
		{
			throw new NotImplementedException();
		}

		public Task<List<MBrand>> GetAllAsync(MBrand entity, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.MBrand.GetAllAsync(entity);
		}

		#region �����ɂ��MBrandSub�ꗗ���擾����
		/// <summary>
		/// �����ɂ��MBrandSub�ꗗ���擾����
		/// </summary>
		/// <param name="mBrandSub"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<MBrandSub>> GetAllSubAsync(MBrandSub mBrandSub, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.MBrand.GetAllSubAsync(mBrandSub, sortColumnName, sortType);
		}
		#endregion

		public Tuple<List<MBrand>, int> GetDataPagination(int currentpage, int pageSize, MBrand entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MBrand> entity)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		///  MBrand��ǉ�����
		/// </summary>
		/// <param name="mBrand"></param>
		/// <param name="bussinessCd"></param>
		/// <returns></returns>
		public long AddAsyncSub(MBrand mBrand, string bussinessCd)
        {
			using (var transactionScope = new TransactionScope())
			{
				long resultInsSubBrand = 0;
				long resultInsBrand = UnitOfWork.MBrand.AddAsync(mBrand).Result;
				if (resultInsBrand != ParamConst.ResultInsertBrand.INSERT_FAIL)
				{
					MBrandRelation mBrandRelation = new MBrandRelation();
					mBrandRelation.BrandCd = mBrand.BrandCd;
					mBrandRelation.BussinessCd = bussinessCd;
					mBrandRelation.CreateUserCd = mBrand.CreateUserCd;
					resultInsBrand = UnitOfWork.MBrandRelation.AddAsync(mBrandRelation).Result;
					if(resultInsBrand!= 0)
                    {
						MSubBrand mSubBrand = new MSubBrand();
						mSubBrand.BrandCd = mBrand.BrandCd;
						mSubBrand.SubBrand = mBrand.SubBrand;
						mSubBrand.CreateUserCd = mBrand.CreateUserCd;
						resultInsSubBrand = UnitOfWork.MSubBrand.AddAsync(mSubBrand).Result;
						if(resultInsSubBrand != 0)
						{
							transactionScope.Complete();
						}

					}
				}
				return resultInsSubBrand != 0 ? resultInsBrand : resultInsSubBrand;
			}


		}
    }
}