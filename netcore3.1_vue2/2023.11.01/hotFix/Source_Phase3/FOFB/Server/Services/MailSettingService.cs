using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility;

namespace FOFB.Server.Services
{
	public class MailSettingService : BaseService, IMailSettingService
	{
		public MailSettingService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

		public Task<long> AddAsync(MailSetting mailSetting)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<MailSetting> mailSettings)
		{
			throw new NotImplementedException();
		}

		#region 検索条件によるメール設定
		/// <summary>
		/// 検索条件によるメール設定
		/// </summary>
		/// <param name="mailSetting"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<MailSetting>> GetAllAsync(MailSetting mailSetting, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.MailSetting.GetAllAsync(mailSetting, sortColumnName, sortType);
		}
		#endregion

		public Tuple<List<MailSetting>, int> GetDataPagination(int currentpage, int pageSize, MailSetting mailSetting, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MailSetting> mailSettings)
		{
			throw new NotImplementedException();
		}

		#region メール設定更新
		/// <summary>
		/// メール設定更新
		/// </summary>
		/// <param name="mailSetting"></param>
		/// <returns></returns>
		public int UpdateMailSetting(MailSetting mailSetting)
		{
			int result = ParamConst.ResultType.ERROR;

			MailSetting mail = new MailSetting();
			mail.BussinessCd = mailSetting.BussinessCd;
			List<MailSetting> lstMailSetting = UnitOfWork.MailSetting.GetAllAsync(mail).Result;

			if (lstMailSetting != null && lstMailSetting.Count > 0)
			{
				if (lstMailSetting[0].UpdateDtTm != mailSetting.UpdateDtTm)
				{
					return ParamConst.ResultType.UPDATED;
				}

				List<MailSetting> mailSettings = new List<MailSetting>();
				mailSettings.Add(mailSetting);
				bool resultUpdMailSetting = UnitOfWork.MailSetting.UpdateAsync(mailSettings).Result;

				if (resultUpdMailSetting)
				{
					result = ParamConst.ResultType.SUCCESS;
				}
			}
			else
			{
				long resultInsMailSetting = UnitOfWork.MailSetting.AddAsync(mailSetting).Result;
				if (resultInsMailSetting > 0)
				{
					result = ParamConst.ResultType.SUCCESS;
				}
			}

			return result;
		}
		#endregion
	}
}