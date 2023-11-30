using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("sendmail")]
	public class SendMail
	{
		/// <summary>
		/// メールID
		/// </summary>
		[Key]
		public int MailId { get; set; }

		/// <summary>
		/// 予約ID
		/// </summary>
		public string OrderId { get; set; }

		/// <summary>
		/// メールテンプレートID
		/// </summary>
		public int MailTemplateId { get; set; }

		/// <summary>
		/// メール件名
		/// </summary>
		public string MailTitle { get; set; }

		/// <summary>
		/// メール本文
		/// </summary>
		public string MailContent { get; set; }

		/// <summary>
		/// 予約状態
		/// </summary>
		public string RsvStatus { get; set; }

		/// <summary>
		/// 送信先
		/// </summary>
		public string SendTo { get; set; }

		/// <summary>
		/// メールテンプレートタイプ
		/// </summary>
		public string MailType { get; set; }

		/// <summary>
		/// メール送信状態
		/// </summary>
		public string MailStatus { get; set; }

		/// <summary>
		/// バウンスメールエラーコード
		/// </summary>
		public string ErrorCd { get; set; }

		/// <summary>
		/// バウンスエラー詳細
		/// </summary>
		public string ErrorDescription { get; set; }

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

		[Write(false)]
		[Computed]
		/// <summary>
		/// 更新日時
		/// </summary>
		public string MailAddress { get; set; }

		[Write(false)]
		[Computed]
		/// <summary>
		/// BarCd画像パス
		/// </summary>
		public string UrlImgBarCd { get; set; }

		[Write(false)]
		[Computed]
		/// <summary>
		/// 注文番号一覧
		/// </summary>
		public string LstOrderId { get; set; }
	}

	public class SendMailError
	{
		/// <summary>
		/// 予約ID
		/// </summary>
		public string OrderId { get; set; }
	}
}
