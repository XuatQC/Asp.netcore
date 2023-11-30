using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Utility;

namespace FOFB.Server.Services
{
	public class MailTemplateService : BaseService, IMailTemplateService
	{
		public MailTemplateService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<long> AddAsync(MailTemplate mailTemplate)
		{
			throw new NotImplementedException();
		}

		#region MailTemplateを追加する
		/// <summary>
		/// MailTemplateを追加する
		/// </summary>
		/// <param name="mBussiness"></param>
		/// <param name="bussinessCdDuplicate"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		public long CreateMailTemplateByBussinessCd(MBussiness mBussiness, string bussinessCdDuplicate, string updateUserCd)
		{
			long resultInsMailTemplate = 0;
			StringBuilder sqlInsertMailTemplate = new StringBuilder("");

			try
			{
				using (var transactionScope = new TransactionScope())
				{
					CreateSqlMailTemplateQuery(mBussiness, sqlInsertMailTemplate, bussinessCdDuplicate, updateUserCd);

					resultInsMailTemplate = UnitOfWork.MailTemplate.InsertMailTemplateByCreateBussiness(sqlInsertMailTemplate.ToString()).Result;

					if (resultInsMailTemplate > 0)
					{
						transactionScope.Complete();
					}
				}
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
			}
			return resultInsMailTemplate;
		}
		#endregion

		public Task<bool> DeleteAsync(List<MailTemplate> mailTemplate)
		{
			throw new NotImplementedException();
		}

		#region 条件によるMailTemplate一覧を取得する
		/// <summary>
		/// 条件によるMailTemplate一覧を取得する
		/// </summary>
		/// <param name="mailTemplate"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<MailTemplate>> GetAllAsync(MailTemplate mailTemplate, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.MailTemplate.GetAllAsync(mailTemplate, sortColumnName, sortType);
		}
		#endregion

		#region 検索条件によるメールテンプレート一覧
		/// <summary>
		/// 検索条件によるメールテンプレート一覧
		/// </summary>
		/// <param name="currentpage"></param>
		/// <param name="pageSize"></param>
		/// <param name="mailTemplateSub"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Tuple<List<MailTemplateSub>, int> GetDataMailTemplatesPagination(int currentpage, int pageSize, MailTemplateSub mailTemplateSub, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.MailTemplate.GetDataMailTemplatesPagination(currentpage, pageSize, mailTemplateSub, sortColumnName, sortType);
		}
		#endregion

		public Tuple<List<MailTemplate>, int> GetDataPagination(int currentpage, int pageSize, MailTemplate entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MailTemplate> mailTemplate)
		{
			throw new NotImplementedException();
		}

		#region メールテンプレート更新
		/// <summary>
		/// メールテンプレート更新
		/// </summary>
		/// <param name="mailTemplate"></param>
		/// <returns></returns>
		public int UpdateMailTemplate(MailTemplate mailTemplate)
		{
			int result = ParamConst.ResultType.ERROR;

			MailTemplate mail = new MailTemplate();
			mail.MailTemplateId = mailTemplate.MailTemplateId;
			List<MailTemplate> lstMailTemplate = UnitOfWork.MailTemplate.GetAllAsync(mail).Result;

			if (lstMailTemplate != null)
			{
				if (lstMailTemplate[0].UpdateDtTm != mailTemplate.UpdateDtTm)
				{
					return ParamConst.ResultType.UPDATED;
				}

				List<MailTemplate> mailTemplates = new List<MailTemplate>();
				mailTemplates.Add(mailTemplate);
				bool resultUpdMailTemplate = UnitOfWork.MailTemplate.UpdateAsync(mailTemplates).Result;

				if (resultUpdMailTemplate)
				{
					result = ParamConst.ResultType.SUCCESS;
				}
			}

			return result;
		}
		#endregion

		#region Create sql insert for MailTemplate
		/// <summary>
		///  Create sql insert for MailTemplate
		/// </summary>
		/// <param name="mBussiness"></param>
		/// <param name="sqlInsertMailTemplate"></param>
		/// <param name="bussinessCdDuplicate"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		private StringBuilder CreateSqlMailTemplateQuery(MBussiness mBussiness,
														StringBuilder sqlInsertMailTemplate,
														string bussinessCdDuplicate,
														string updateUserCd)
		{
			sqlInsertMailTemplate.Append($@"
											INSERT INTO mailtemplate (
																	  Title
																	, Content
																	, SendTo
																	, Type
																	, BussinessCd
																	, CreateUserCd
																	, UpdateUserCd
																	)
															SELECT
																  Title
																, Content
																, SendTo
																, Type
																, '{ mBussiness.BussinessCd }'
																, '{ updateUserCd }'
																, '{ updateUserCd }' 
															FROM mailtemplate 
															WHERE BussinessCd = '{ bussinessCdDuplicate }';
										");

			return sqlInsertMailTemplate;
		}
		#endregion
	}
}
