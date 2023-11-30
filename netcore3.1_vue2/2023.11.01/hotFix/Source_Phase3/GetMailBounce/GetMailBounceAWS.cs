using Amazon.SQS;
using Amazon.SQS.Model;
using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Utility;

namespace GetMailBounce
{
	public class GetMailBounceAWS
	{
		private IService Service;
		public GetMailBounceAWS(IService _service)
		{
			Service = _service;
		}

		public void BounceEmails()
		{
			Log.Logger.Debug("Start get mail bounce...");
			Log.Logger.Information("Function: " + System.Reflection.MethodBase.GetCurrentMethod().Name);

			//get mailsetting
			MailSetting mailSetting = new MailSetting();
			List<MailSetting> mailSettings = Service.MailSetting.GetAllAsync(mailSetting).Result;

			if (mailSettings != null && mailSettings.Count > 0)
			{
				Amazon.RegionEndpoint REGION = Amazon.RegionEndpoint.USEast1;

				AmazonSQSClient sqsClient = null;
				ReceiveMessageRequest receiveMessRequest = null;
				ReceiveMessageResponse response = null;
				AmazonSqsNotification notification = null;
				AmazonSesBounceNotification bounce = null;
				DeleteMessageRequest delRequest = null;
				DeleteMessageResponse deleteMessage = null;
				TaskAwaiter<ReceiveMessageResponse> responses = new TaskAwaiter<ReceiveMessageResponse>();
				int messages = 0;
				List<SendMail> mailErrs = null;
				SendMail mailErr = null;
				List<OrdHistory> ordHistories = null;
				List<OrdHistory> ordHistoriesUpd = null;
				OrdHistory ordHistory = null;
				bool updMailBounce = false;
				bool updMailHistory = false;
				string subject = string.Empty;

				for (int i = 0; i < mailSettings.Count; i++)
				{
					Log.Logger.Information("------------Parameters---------------");
					Log.Logger.Information("bussinessCd: " + mailSettings[i].BussinessCd);
					Log.Logger.Information("accessKeyID: " + mailSettings[i].MailBounceAccessKeyID);
					Log.Logger.Information("secretAccessKey: " + mailSettings[i].MailBounceSecretAccessKey);
					Log.Logger.Information("queueUrl: " + mailSettings[i].MailBounceQueueUrl);

					sqsClient = new AmazonSQSClient(mailSettings[i].MailBounceAccessKeyID, mailSettings[i].MailBounceSecretAccessKey, REGION);

					receiveMessRequest = new ReceiveMessageRequest();
					receiveMessRequest.QueueUrl = mailSettings[i].MailBounceQueueUrl;
					receiveMessRequest.MaxNumberOfMessages = 10;
					receiveMessRequest.WaitTimeSeconds = 10;

					try
					{
						responses = sqsClient.ReceiveMessageAsync(receiveMessRequest).GetAwaiter();
						response = new ReceiveMessageResponse();
						response = responses.GetResult();

						messages = response.Messages.Count;

						if (messages > 0)
						{
							foreach (var message in response.Messages)
							{
								// First, convert the Amazon SNS message into a JSON object.
								notification = Newtonsoft.Json.JsonConvert.DeserializeObject<AmazonSqsNotification>(message.Body);

								// Now access the Amazon SES bounce notification.
								bounce = Newtonsoft.Json.JsonConvert.DeserializeObject<AmazonSesBounceNotification>(notification.Message);

								Log.Logger.Information("-----------------------------------");
								Log.Logger.Information("Get mail sended to compare with mail bounce");

								subject = bounce.Mail.CommonHeaders.Subject;

								mailErr = Service.SendMail.GetMailBounceError(bounce.Bounce.BouncedRecipients[0].EmailAddress,
																				subject,
																				mailSettings[i].BussinessCd).Result;

								if (mailErr != null)
								{
									mailErr.MailStatus = ParamConst.MailStatus.STATUS_ERROR;
									mailErr.ErrorCd = bounce.Bounce.BouncedRecipients[0].Status;
									mailErr.ErrorDescription = bounce.Bounce.BouncedRecipients[0].DiagnosticCode;
									mailErr.UpdateUserCd = "auto";

									Log.Logger.Information("Update bounce");

									//update mail
									mailErrs = new List<SendMail>();
									mailErrs.Add(mailErr);
									updMailBounce = Service.SendMail.UpdateAsync(mailErrs).Result;

									if (updMailBounce)
									{
										Log.Logger.Information("Update bounce success");

										// add log
										Log.Logger.Information("------------ Mail Infor ------------");
										Log.Logger.Information("OrderId: " + mailErr.OrderId);
										Log.Logger.Information("Subject: " + subject);
										Log.Logger.Information("Email Address: " + bounce.Bounce.BouncedRecipients[0].EmailAddress);
										Log.Logger.Information("------------ Error Infor ------------");
										Log.Logger.Information("Status: " + bounce.Bounce.BouncedRecipients[0].Status);
										Log.Logger.Information("Description: " + bounce.Bounce.BouncedRecipients[0].DiagnosticCode);

										Log.Logger.Information("Get mail history");
										// get histoy mail by mailId
										ordHistory = new OrdHistory();
										ordHistory.MailId = mailErr.MailId;
										ordHistories = Service.OrdHistory.GetAllAsync(ordHistory, "UpdateDtTm", "DESC").Result;

										if (ordHistories.Count > 0)
										{
											Log.Logger.Information("Update mail history");

											ordHistories[0].MailStatus = ParamConst.MailStatus.STATUS_ERROR;
											ordHistories[0].ErrorCd = bounce.Bounce.BouncedRecipients[0].Status;
											ordHistories[0].ErrorDescription = bounce.Bounce.BouncedRecipients[0].DiagnosticCode;
											ordHistories[0].UpdateUserCd = "auto";

											ordHistoriesUpd = new List<OrdHistory>();
											ordHistoriesUpd.Add(ordHistories[0]);

											updMailHistory = Service.OrdHistory.UpdateAsync(ordHistoriesUpd).Result;

											if (updMailHistory)
											{
												Log.Logger.Information("Update mail history success");

												// add log
												Log.Logger.Information("------------ Mail hitory Infor -------");
												Log.Logger.Information("OrdHistId: " + ordHistories[0].OrdHistId);
												Log.Logger.Information("MailId: " + ordHistories[0].MailId);
											}
											else
											{
												Log.Logger.Error("Update mail history error");
											}
										}
										else
										{
											Log.Logger.Information("No have mail history update");
										}

										delRequest = new DeleteMessageRequest
										{
											QueueUrl = mailSettings[i].MailBounceQueueUrl,
											ReceiptHandle = message.ReceiptHandle
										};

										deleteMessage = sqsClient.DeleteMessageAsync(delRequest).Result;
										if (deleteMessage.HttpStatusCode != System.Net.HttpStatusCode.OK)
										{
											Log.Logger.Error("-----------------------------------");
											Log.Logger.Error("Error Code: ", deleteMessage.HttpStatusCode);
										}
									}
									else
									{
										Log.Logger.Error("Update bounce error");
									}
								}
								else
								{
									Log.Logger.Information("No have mail bounce update");
								}
							}
							Log.Logger.Information("Finised get mail bounce with bussinessCd: " + mailSettings[i].BussinessCd);
						}
						else
						{
							Log.Logger.Information("No have mail bounce... ");
						}
					}
					catch (Exception ex)
					{
						Log.Logger.Error("Error: ", ex);
					}
				}
			}
			else
			{
				Log.Logger.Information("Not get mail setting...");
			}
		}
	}
}
