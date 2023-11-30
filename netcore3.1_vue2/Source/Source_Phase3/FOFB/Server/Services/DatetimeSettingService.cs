using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace FOFB.Server.Services
{
	public class DatetimeSettingService : BaseService, IDatetimeSettingService
	{
		public DatetimeSettingService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<long> AddAsync(DatetimeSetting entity)
		{
			throw new NotImplementedException();
		}

		#region 予約開始時間をチェックする
		/// <summary>
		/// 予約開始時間をチェックする
		/// </summary>
		/// <param name="bussinessCd"></param>
		/// <returns></returns>
		public bool IsOrdStartDtTm(string bussinessCd)
		{
			// 現在時間を取得する
			DateTime dtNow = Common.GetCurrJPDateTime();

			// DBの予約時間を取得する
			DatetimeSetting datetimesetting = new DatetimeSetting();
			datetimesetting.BussinessCd = bussinessCd;
			Task<List<DatetimeSetting>> lstDatetimeSetting = UnitOfWork.DatetimeSetting.GetAllAsync(datetimesetting);

			if (lstDatetimeSetting.Result.Count == 0)
			{
				return false;
			}

			DateTime openDate = lstDatetimeSetting.Result[0].OrdStartDtTm;
			DateTime closeDate = lstDatetimeSetting.Result[0].OrdEndDtTm;

			return dtNow > openDate && dtNow < closeDate;
		}
		#endregion

		public Task<bool> DeleteAsync(List<DatetimeSetting> entity)
		{
			throw new NotImplementedException();
		}

		public Task<List<DatetimeSetting>> GetAllAsync(DatetimeSetting entity, string sortColumnName, string sortType)
		{
			return UnitOfWork.DatetimeSetting.GetAllAsync(entity);
		}

		public Tuple<List<DatetimeSetting>, int> GetDataPagination(int currentpage, int pageSize, DatetimeSetting data, string sortColumnName, string sortType)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<DatetimeSetting> entity)
		{
			return UnitOfWork.DatetimeSetting.UpdateAsync(entity);
		}

        public bool AddOrUpdate(DatetimeSetting datetimeSetting)
        {
			return UnitOfWork.DatetimeSetting.AddOrUpdate(datetimeSetting);
		}
	}
}
