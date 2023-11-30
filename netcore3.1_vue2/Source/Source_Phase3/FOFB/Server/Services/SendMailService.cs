using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services
{
	public class SendMailService : BaseService, ISendMailService
	{
		public SendMailService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public Task<long> AddAsync(SendMail entity)
		{
			return UnitOfWork.SendMail.AddAsync(entity);
		}

		public Task<bool> DeleteAsync(List<SendMail> entity)
		{
			throw new NotImplementedException();
		}

		public Task<List<SendMail>> GetAllAsync(SendMail entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Tuple<List<SendMail>, int> GetDataPagination(int currentpage, int pageSize, SendMail entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		#region メール一覧
		/// <summary>
		/// メール一覧
		/// </summary>
		/// <param name="mailAddress"></param>
		/// <param name="mailTitle"></param>
		/// <param name="bussinessCd"></param>
		/// <returns></returns>
		public Task<SendMail> GetMailBounceError(string mailAddress, string mailTitle, string bussinessCd)
		{
			return UnitOfWork.SendMail.GetMailBounceError(mailAddress, mailTitle, bussinessCd);
		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sendMails"></param>
		/// <param name="lstEmailTo"></param>
		/// <param name="mailSetting"></param>
		/// <param name="lstUrlImgBarCd"></param>
		/// <returns></returns>
		public List<SendMail> SendMail(List<SendMail> sendMails, List<string> lstEmailTo, MailSetting mailSetting, List<string> lstUrlImgBarCd)
		{
			return UnitOfWork.SendMail.SendMail(sendMails, lstEmailTo, mailSetting, lstUrlImgBarCd);
		}

		#region SendMailを更新する
		/// <summary>
		/// SendMailを更新する
		/// </summary>
		/// <param name="sendMails"></param>
		/// <returns></returns>
		public Task<bool> UpdateAsync(List<SendMail> sendMails)
		{
			return UnitOfWork.SendMail.UpdateAsync(sendMails);
		}
		#endregion
	}
}