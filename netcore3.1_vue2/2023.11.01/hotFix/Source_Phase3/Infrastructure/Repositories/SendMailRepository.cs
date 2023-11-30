using Amazon.SQS;
using Amazon.SQS.Model;
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
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Infrastructure.Repositories
{

	public class SendMailRepository : BaseRepository, ISendMailRepository
	{
		public SendMailRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		#region メール送信
		/// <summary>
		/// メール送信
		/// </summary>
		/// <param name="sendMails"></param>
		/// <param name="lstEmailTo"></param>
		/// <param name="mailSetting"></param>
		/// <param name="lstUrlImgBarCd"></param>
		/// <returns></returns>
		public List<SendMail> SendMail(List<SendMail> sendMails, List<string> lstEmailTo, MailSetting mailSetting, List<string> lstUrlImgBarCd)
		{
			try
			{
				Amazon.RegionEndpoint REGION = Amazon.RegionEndpoint.USEast1;
				AmazonSQSClient sq = new AmazonSQSClient(mailSetting.MailBounceAccessKeyID, mailSetting.MailBounceSecretAccessKey, REGION);

				int PORT = 587;
				string contentID = "Image";
				int countSendMail = 0;
				const int MAX_SEND_MAIL_IN_SECONDS = 11;

				if (lstUrlImgBarCd.Count > 0)
				{
					for (int i = 0; i < sendMails.Count; i++)
					{
						sendMails[i].MailAddress = !string.IsNullOrEmpty(lstEmailTo[i]) ? lstEmailTo[i] : mailSetting.MailFrom;
						sendMails[i].MailContent = sendMails[i].MailContent.Replace(ParamConst.ItemMailTemplate.MAIL_IMAGE_BAR_CD, "<p><img src=\"cid:" + contentID + "\"></p>");
						sendMails[i].UrlImgBarCd = lstUrlImgBarCd[i];
					}
				}
				else
				{
					for (int i = 0; i < sendMails.Count; i++)
					{
						sendMails[i].MailAddress = !string.IsNullOrEmpty(lstEmailTo[i]) ? lstEmailTo[i] : mailSetting.MailFrom;
						sendMails[i].MailContent = sendMails[i].MailContent.Replace(ParamConst.ItemMailTemplate.MAIL_IMAGE_BAR_CD, "");
					}
				}

				Parallel.ForEach<SendMail>(sendMails, (mail, state, index) =>
				{
					try
					{
						MailAddress from = new MailAddress(mailSetting.MailFrom, mailSetting.MailFromName);
						MailAddress to = new MailAddress(mail.MailAddress);
						MailMessage message = new MailMessage(from, to);
						Attachment imageBarCd = null;
						message.Subject = mail.MailTitle;
						message.Body = mail.MailContent;

						if (!string.IsNullOrEmpty(mail.UrlImgBarCd))
						{
							imageBarCd = new Attachment(mail.UrlImgBarCd);
							imageBarCd.ContentId = contentID;
							imageBarCd.ContentDisposition.Inline = true;
							imageBarCd.ContentDisposition.DispositionType = DispositionTypeNames.Inline;

							if (message.Attachments.Where(s => s.ContentId == contentID).Count() == 0)
							{
								message.Attachments.Add(imageBarCd);
							}
						}

						message.IsBodyHtml = true;
						SmtpClient smtp = new SmtpClient();
						smtp.Host = mailSetting.SmtpServer;
						smtp.EnableSsl = true;
						smtp.UseDefaultCredentials = false;
						NetworkCredential NetworkCred = new NetworkCredential();
						NetworkCred.UserName = mailSetting.SmtpAccount;
						NetworkCred.Password = mailSetting.SmtpPass;
						smtp.Credentials = NetworkCred;
						smtp.Port = PORT;

						if (countSendMail == MAX_SEND_MAIL_IN_SECONDS)
						{
							System.Threading.Thread.Sleep(1000);
							countSendMail = 0;
						}
						smtp.Send(message);

						imageBarCd.Dispose();
						message.Dispose();
						smtp.Dispose();

						if (mail.SendTo == ParamConst.SendTo.SENDTO_CUSTOMER)
						{
							System.Threading.Thread.Sleep(3000);
							AmazonSesBouncedRecipient inforError = this.BounceEmails(sq, mailSetting.MailBounceQueueUrl, mail.MailAddress, mail.MailTitle);

							if (inforError != null)
							{
								mail.ErrorCd = inforError.Status;
								mail.ErrorDescription = inforError.DiagnosticCode;
								mail.MailStatus = ParamConst.MailStatus.STATUS_ERROR;
							}
							else
							{
								if (mail.MailId != 0)
								{
									mail.MailStatus = mail.MailStatus == ParamConst.MailStatus.STATUS_ERROR ?
																							ParamConst.MailStatus.STATUS_SUCCESS : ParamConst.MailStatus.STATUS_RESEND;
									mail.ErrorCd = null;
									mail.ErrorDescription = null;
								}
								else
								{
									mail.MailStatus = ParamConst.MailStatus.STATUS_SUCCESS;
								}
							}
						}
						else
						{
							mail.MailStatus = ParamConst.MailStatus.STATUS_SUCCESS;
						}
						countSendMail++;
					}
					catch (Exception e)
					{
						mail.MailStatus = ParamConst.MailStatus.STATUS_ERROR;
						Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name + " Send to: " + mail.MailAddress);
					}
				});
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
			}
			return sendMails;
		}
		#endregion

		#region SendMailを追加する
		/// <summary>
		/// SendMailを追加する
		/// </summary>
		/// <param name="sendMail"></param>
		/// <returns></returns>
		public async Task<long> AddAsync(SendMail sendMail)
		{
			try
			{
				return DbConn.Insert(sendMail);
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

		public Task<bool> DeleteAsync(List<SendMail> entity)
		{
			throw new NotImplementedException();
		}

		#region 条件によるSendMail一覧を取得する
		/// <summary>
		/// 条件によるSendMail一覧を取得する
		/// </summary>
		/// <param name="sendMail"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<SendMail>> GetAllAsync(SendMail sendMail, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic sendMailParam = new ExpandoObject();


				if (!string.IsNullOrEmpty(sendMail.RsvStatus))
				{
					condition.Append(" AND RsvStatus = @RsvStatus");
					sendMailParam.RsvStatus = sendMail.RsvStatus;
				}

				if (!string.IsNullOrEmpty(sendMail.MailType))
				{
					condition.Append(" AND MailType = @MailType");
					sendMailParam.MailType = sendMail.MailType;
				}

				if (!string.IsNullOrEmpty(sendMail.LstOrderId))
				{
					condition.Append($" AND OrderId IN ({ sendMail.LstOrderId })");
				}
				condition.Append($" AND DelFlg = { ParamConst.DelFlg.OFF } ");

				if (!string.IsNullOrEmpty(sortColumnName))
				{
					condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT
										MailId
										, OrderId
										, MailTemplateId
										, MailTitle
										, MailContent
										, RsvStatus
										, SendTo
										, MailType
										, MailStatus
										, ErrorCd
										, ErrorDescription
										, CreateUserCd
										, CreateDtTm
									FROM sendmail
									WHERE 1=1
									{condition}");
				return (List<SendMail>)await this.DbConn.QueryAsync<SendMail>(sqlGetData.ToString(), (object)sendMailParam);
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

		public Tuple<List<SendMail>, int> GetDataPagination(int currentpage, int pageSize, SendMail entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		#region SendMailを更新する
		/// <summary>
		/// SendMailを更新する
		/// </summary>
		/// <param name="sendMails"></param>
		/// <returns></returns>
		public async Task<bool> UpdateAsync(List<SendMail> sendMails)
		{
			try
			{
				return await this.DbConn.UpdateAsync(sendMails);
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

		#region メール一覧
		/// <summary>
		/// メール一覧
		/// </summary>
		/// <param name="mailAddress"></param>
		/// <param name="mailTitle"></param>
		/// <param name="bussinessCd"></param>
		/// <returns></returns>
		public async Task<SendMail> GetMailBounceError(string mailAddress, string mailTitle, string bussinessCd)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic sendMailParam = new ExpandoObject();

				if (!string.IsNullOrEmpty(mailAddress))
				{
					condition.Append(" AND mc.MailAddress = @MailAddress");
					sendMailParam.MailAddress = mailAddress;
				}

				if (!string.IsNullOrEmpty(mailTitle))
				{
					condition.Append(" AND sm.MailTitle = @MailTitle");
					sendMailParam.MailTitle = mailTitle;
				}

				if (!string.IsNullOrEmpty(bussinessCd))
				{
					condition.Append(" AND ord.BussinessCd = @BussinessCd");
					sendMailParam.BussinessCd = bussinessCd;
				}
				condition.Append($" AND sm.SendTo = '{ ParamConst.SendTo.SENDTO_CUSTOMER }' ");
				condition.Append($" AND sm.DelFlg = { ParamConst.DelFlg.OFF } ");
				condition.Append(" ORDER BY sm.UpdateDtTm DESC");

				sqlGetData.Append(@$"SELECT
										sm.MailId
										, sm.OrderId
										, sm.MailTemplateId
										, sm.MailTitle
										, sm.MailContent
										, sm.RsvStatus
										, sm.SendTo
										, sm.MailType
										, sm.MailStatus
										, sm.ErrorCd
										, sm.ErrorDescription
										, sm.CreateUserCd
										, sm.CreateDtTm
									FROM
										sendmail sm
									INNER JOIN orders ord
										ON sm.OrderId = ord.OrderId
										AND ord.DelFlg = { ParamConst.DelFlg.OFF }
									INNER JOIN mcust mc
										ON ord.CustId = mc.CustId
										AND mc.DelFlg = { ParamConst.DelFlg.OFF }
									WHERE 1=1
									{condition}");
				return await this.DbConn.QueryFirstOrDefaultAsync<SendMail>(sqlGetData.ToString(), (object)sendMailParam);
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

		#region バウンスメール情報
		/// <summary>
		/// バウンスメール情報
		/// </summary>
		/// <param name="sq"></param>
		/// <param name="queueUrl"></param>
		/// <param name="emailTo"></param>
		/// <param name="subjectMail"></param>
		/// <returns></returns>
		private AmazonSesBouncedRecipient BounceEmails(AmazonSQSClient sq, string queueUrl, string emailTo, string subjectMail)
		{
			string subject = string.Empty;
			AmazonSesBouncedRecipient amazonSesBouncedRecipient = null;

			ReceiveMessageRequest rmr = new ReceiveMessageRequest();
			rmr.QueueUrl = queueUrl;
			rmr.MaxNumberOfMessages = 10;
			ReceiveMessageResponse response = sq.ReceiveMessageAsync(rmr).Result;

			int messages = response.Messages.Count;
			if (messages > 0)
			{
				try
				{
					AmazonSqsNotification notification = null;
					AmazonSesBounceNotification bounce = null;
					DeleteMessageRequest delRequest = null;
					DeleteMessageResponse deleteMessage = null;
					foreach (var m in response.Messages)
					{
						amazonSesBouncedRecipient = new AmazonSesBouncedRecipient();
						// First, convert the Amazon SNS message into a JSON object.
						notification = Newtonsoft.Json.JsonConvert.DeserializeObject<AmazonSqsNotification>(m.Body);

						// Now access the Amazon SES bounce notification.
						bounce = Newtonsoft.Json.JsonConvert.DeserializeObject<AmazonSesBounceNotification>(notification.Message);

						// add mail bounce
						amazonSesBouncedRecipient = bounce.Bounce.BouncedRecipients[0];

						// subject mail bouce
						subject = bounce.Mail.CommonHeaders.Subject;

						if (amazonSesBouncedRecipient.EmailAddress != emailTo || subject != subjectMail)
						{
							amazonSesBouncedRecipient = null;
						}
						else
						{
							delRequest = new DeleteMessageRequest
							{
								QueueUrl = queueUrl,
								ReceiptHandle = m.ReceiptHandle
							};

							deleteMessage = sq.DeleteMessageAsync(delRequest).Result;
							if (deleteMessage.HttpStatusCode != HttpStatusCode.OK)
							{
								Log.Logger.Error("Error Code: ", deleteMessage.HttpStatusCode);
							}
						}
					}
				}
				catch (Exception e)
				{
					Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
					return null;
				}
			}
			return amazonSesBouncedRecipient;
		}
		#endregion
	}
}