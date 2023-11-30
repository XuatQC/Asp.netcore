using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("mailtemplate")]
	public class MailTemplate
	{
		/// <summary>
		/// メールテンプレートID
		/// </summary>
		[Key]
		public int MailTemplateId { get; set; }

		/// <summary>
		/// メールテンプレート件名
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// メールテンプレート内容
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// 送信先
		/// </summary>
		public string SendTo { get; set; }

		/// <summary>
		/// メールテンプレートタイプ
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// 業態コード
		/// </summary>
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

	public class MailTemplateSub : MailTemplate
	{
		/// <summary>
		/// メール種類テキスト
		/// </summary>
		public string TypeName { get; set; }
	}
}
