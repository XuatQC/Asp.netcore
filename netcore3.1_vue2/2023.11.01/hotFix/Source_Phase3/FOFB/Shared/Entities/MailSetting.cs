using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("mailsetting")]
	public class MailSetting
	{
		/// <summary>
		/// 送信元
		/// </summary>
		public string MailFrom { get; set; }

		/// <summary>
		/// 送信元名
		/// </summary>
		public string MailFromName { get; set; }

		/// <summary>
		/// To
		/// </summary>
		public string MailTo { get; set; }

		/// <summary>
		/// AccessKeyID(バウンスメール取得)
		/// </summary>
		public string MailBounceAccessKeyID { get; set; }

		/// <summary>
		/// SecretAccessKey(バウンスメール取得)
		/// </summary>
		public string MailBounceSecretAccessKey { get; set; }

		/// <summary>
		/// QueueUrl(バウンスメール取得)
		/// </summary>
		public string MailBounceQueueUrl { get; set; }

		/// <summary>
		/// SMTP Server
		/// </summary>
		public string SmtpServer { get; set; }

		/// <summary>
		/// SMTP Account
		/// </summary>
		public string SmtpAccount { get; set; }

		/// <summary>
		/// SMTP PASS
		/// </summary>
		public string SmtpPass { get; set; }

		/// <summary>
		/// 業態コード
		/// </summary>
		[Key]
		public string BussinessCd { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string CreateUserCd { get; set; }

		[Write(false)]
		[Computed]
		/// <summary>
		/// 作成日時
		/// </summary>
		public DateTime CreateDtTm { get; set; }

		/// <summary>
		/// 更新者
		/// </summary>
		public string UpdateUserCd { get; set; }

		[Write(false)]
		[Computed]
		/// <summary>
		/// 更新日時
		/// </summary>
		public DateTime UpdateDtTm { get; set; }

		/// <summary>
		/// 削除フラグ
		/// </summary>
		public bool DelFlg { get; set; }
	}
}