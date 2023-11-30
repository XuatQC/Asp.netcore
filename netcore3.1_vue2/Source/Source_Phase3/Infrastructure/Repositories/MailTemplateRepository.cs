using Dapper;
using Dapper.Contrib.Extensions;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Infrastructure.Repositories
{

	public class MailTemplateRepository : BaseRepository, IMailTemplateRepository
	{
		public MailTemplateRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		public Task<long> AddAsync(MailTemplate mailtemplate)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<MailTemplate> mailtemplate)
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
		public async Task<List<MailTemplate>> GetAllAsync(MailTemplate mailTemplate, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mailTemplatepParam = new ExpandoObject();

				if (mailTemplate.MailTemplateId != 0)
				{
					condition.Append(" AND MailTemplateId = @MailTemplateId");
					mailTemplatepParam.MailTemplateId = mailTemplate.MailTemplateId;
				}
				if (!string.IsNullOrEmpty(mailTemplate.SendTo))
				{
					condition.Append(" AND SendTo = @SendTo");
					mailTemplatepParam.SendTo = mailTemplate.SendTo;
				}
				if (!string.IsNullOrEmpty(mailTemplate.Type))
				{
					condition.Append(" AND Type = @Type");
					mailTemplatepParam.Type = mailTemplate.Type;
				}
				if (!string.IsNullOrEmpty(mailTemplate.BussinessCd))
				{
					condition.Append(" AND BussinessCd = @BussinessCd");
					mailTemplatepParam.BussinessCd = mailTemplate.BussinessCd;
				}
				condition.Append($" AND DelFlg = { ParamConst.DelFlg.OFF } ");

				if (!string.IsNullOrEmpty(sortColumnName))
				{
					if (!string.IsNullOrEmpty(sortType))
					{
						sortType = " ASC";
					}
					else
					{
						sortType = " DESC";
					}
					condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT	 
											MailTemplateId
											,Title
											,Content
											,SendTo
											,Type
											,BussinessCd
											,CreateUserCd
											,CreateDtTm
											,UpdateUserCd
											,UpdateDtTm
											,DelFlg
										FROM
											mailtemplate
										WHERE 1=1
											{condition}");
				return (List<MailTemplate>)await this.DbConn.QueryAsync<MailTemplate>(sqlGetData.ToString(), (object)mailTemplatepParam);
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return null;
			}
			finally
			{
				this.DbConn.Close();
			}
		}
		#endregion

		public Tuple<List<MailTemplate>, int> GetDataPagination(int currentpage, int pageSize, MailTemplate entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		#region 条件によるMailTemplate一覧を取得する
		/// <summary>
		/// 条件によるMailTemplate一覧を取得する
		/// </summary>
		/// <param name="currentpage"></param>
		/// <param name="pageSize"></param>
		/// <param name="mailTemplateSub"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Tuple<List<MailTemplateSub>, int> GetDataMailTemplatesPagination(int currentpage, int pageSize, MailTemplateSub mailTemplateSub, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder select = new StringBuilder("");
				StringBuilder table = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mailtemplateParam = new ExpandoObject();

				if (!string.IsNullOrEmpty(mailTemplateSub.Title))
				{
					condition.Append($" AND mt.Title LIKE '%{ mailTemplateSub.Title }%'");
				}
				if (!string.IsNullOrEmpty(mailTemplateSub.Type))
				{
					condition.Append(" AND mt.Type = @Type");
					mailtemplateParam.Type = mailTemplateSub.Type;
				}
				if (!string.IsNullOrEmpty(mailTemplateSub.BussinessCd))
				{
					condition.Append(" AND mt.BussinessCd = @BussinessCd");
					mailtemplateParam.BussinessCd = mailTemplateSub.BussinessCd;
				}
				condition.Append($" AND mt.DelFlg = { ParamConst.DelFlg.OFF } ");

				if (!string.IsNullOrEmpty(sortColumnName))
				{
					condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
				}

				select.Append(@$"SELECT
									mt.MailTemplateId
									, mt.Title
									, mt.Content
									, mt.SendTo
									, mt.Type
									, mt.BussinessCd
									, mt.CreateUserCd
									, mt.UpdateDtTm
									, kbn.KbnValueName as TypeName
								");

				table.Append(@$" FROM mailtemplate mt
								LEFT JOIN mkbnvalue kbn
									ON kbn.KbnCd = '{ ParamConst.MailType.KBN_CD }'
									AND kbn.KbnValue = mt.Type
									AND kbn.DelFlg = { ParamConst.DelFlg.OFF }
								WHERE 1=1
									{ condition }");

				sqlGetData.Append($@"{ select } { table }
									 LIMIT { pageSize } OFFSET { pageSize * (currentpage - 1) }");

				List<MailTemplateSub> lstMailTemplate = this.DbConn.Query<MailTemplateSub>(sqlGetData.ToString(), (object)mailtemplateParam).ToList();
				return new Tuple<List<MailTemplateSub>, int>(lstMailTemplate, GetCount(table.ToString(), (object)mailtemplateParam));
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return null;
			}
			finally
			{
				this.DbConn.Close();
			}
		}
		#endregion

		#region MailTemplateを更新する
		/// <summary>
		/// MailTemplateを更新する
		/// </summary>
		/// <param name="mailTemplates"></param>
		/// <returns></returns>
		public async Task<bool> UpdateAsync(List<MailTemplate> mailTemplates)
		{
			try
			{
				return await this.DbConn.UpdateAsync(mailTemplates);
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return false;
			}
			finally
			{
				this.DbConn.Close();
			}
		}
		#endregion

		#region MailTemplateを追加する
		/// <summary>
		/// MailTemplateを追加する
		/// </summary>
		/// <param name="sqlQuery"></param>
		/// <returns></returns>
		public async Task<long> InsertMailTemplateByCreateBussiness(string sqlQuery)
		{
			try
			{
				return (long)this.DbConn.ExecuteAsync(sqlQuery).Result;
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return 0;
			}
			finally
			{
				this.DbConn.Close();
			}
		}
		#endregion

		#region Get Count mailtemplate
		/// <summary>
		/// Get Count mailtemplate
		/// </summary>
		/// <param name="table"></param>
		/// <param name="param"></param>
		/// <returns></returns>
		private int GetCount(string table, object param)
		{
			try
			{
				string sqlGetCountData = string.Empty;
				sqlGetCountData = $@"SELECT
										COUNT(mt.MailTemplateId)
									{ table }";

				return this.DbConn.Query<int>(sqlGetCountData, param).First();
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return 0;
			}
			finally
			{
				this.DbConn.Close();
			}
		}
		#endregion
	}
}