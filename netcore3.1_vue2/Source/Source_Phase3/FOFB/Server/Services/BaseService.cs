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
	public class BaseService : IBaseService
	{
		public readonly IUnitOfWork UnitOfWork;

		private Common _common;
		public Common Common => _common ??= new Common(UnitOfWork.Config);
		public BaseService(IUnitOfWork unitOfWork)
		{
			this.UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		}

		#region 入金可能期間チェック
		/// <summary>
		/// 入金可能期間チェック
		/// </summary>
		/// <returns></returns>
		public bool IsPaymentAble(string bussinessCd)
		{
			Log.Logger.Information("▼------Commom.IsPaymentAble Start");
			DatetimeSetting datetimeSetting = new DatetimeSetting();
			datetimeSetting.BussinessCd = bussinessCd;

			// get payment period
			List<DatetimeSetting> lstPaymentPeriod = this.UnitOfWork.DatetimeSetting.GetAll(datetimeSetting);
			Log.Logger.Information("bussinessCd:" + bussinessCd);

			// compare now with period
			string strOpenDt = lstPaymentPeriod.Where(x => x.BussinessCd == bussinessCd).FirstOrDefault().RcvMoneyStartDtTm.ToString();
			string strCloseDt = lstPaymentPeriod.Where(x => x.BussinessCd == bussinessCd).FirstOrDefault().RcvMoneyEndDtTm.ToString();

			DateTime today = Common.GetCurrJPDateTime();
			DateTime openDate = Convert.ToDateTime(strOpenDt);
			DateTime closeDate = Convert.ToDateTime(strCloseDt);

			Log.Logger.Information("open: " + openDate + " current datetm: " + today + " close: " + closeDate);
			bool result = (today >= openDate && today <= closeDate);

			Log.Logger.Information("IsPaymentAble: " + result);
			Log.Logger.Information("▲------Commom.IsPaymentAble End");
			return result;
		}
		#endregion

		#region 引渡し可能期間チェック
		/// <summary>
		/// 引渡し可能期間チェック
		/// </summary>
		/// <returns></returns>
		//public bool IsDeliveryAble(string bussinessCd)
		//{
		//	Log.Logger.Information("▼------Commom.IsDeliveryAble Start");
		//	DatetimeSetting datetimeSetting = new DatetimeSetting();
		//	datetimeSetting.BussinessCd = bussinessCd;

		//	// get delivery period
		//	List<DatetimeSetting> lstPaymentPeriod = this.UnitOfWork.DatetimeSetting.GetAll(datetimeSetting);
		//	Log.Logger.Information("bussinessCd:" + bussinessCd);

		//	// compare now with period
		//	string strOpenDt = lstPaymentPeriod.Where(x => x.BussinessCd == bussinessCd).FirstOrDefault().DeliveryStartDtTm.ToString();

		//	DateTime today = Common.GetCurrJPDateTime();
		//	DateTime openDate = Convert.ToDateTime(strOpenDt);
		//	Log.Logger.Information("open: " + openDate + " current datetm: " + today);

		//	bool result = (today >= openDate);

		//	Log.Logger.Information("IsDeliveryAble: " + result);
		//	Log.Logger.Information("▲------Commom.IsDeliveryAble End");
		//	return result;
		//}
		#endregion

		/// <summary>
		/// 入金可能期間チェック(本部側)
		/// </summary>
		/// <returns></returns>
		public bool IsAdminPaymentAble(string bussinessCd)
		{
			Log.Logger.Information("▼------Commom.IsAdminPaymentAble Start");
			DatetimeSetting datetimeSetting = new DatetimeSetting();
			datetimeSetting.BussinessCd = bussinessCd;

			// get payment period
			List<DatetimeSetting> lstPaymentPeriod = this.UnitOfWork.DatetimeSetting.GetAll(datetimeSetting);
			Log.Logger.Information("bussinessCd:" + bussinessCd);

			// compare now with period
			string strOpenDt = lstPaymentPeriod.Where(x => x.BussinessCd == bussinessCd).FirstOrDefault().RcvMoneyStartDtTm.ToString();

			DateTime today = Common.GetCurrJPDateTime();
			DateTime openDate = Convert.ToDateTime(strOpenDt);

			Log.Logger.Information("open: " + openDate + " current datetm: " + today);
			bool result = (today >= openDate);

			Log.Logger.Information("IsAdminPaymentAble: " + result);
			Log.Logger.Information("▲------Commom.IsAdminPaymentAble End");
			return result;
		}
	}
}
