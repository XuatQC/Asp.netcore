using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("urlsetting")]
	public class UrlSetting
	{

		/// <summary>
		/// Url
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// 業態コード
		/// </summary>
		[ExplicitKey]
		public string BussinessCd { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string CreateUserCd { get; set; }

		/// <summary>
		/// 作成日時
		/// </summary>
		public DateTime CreateDtTm { get; set; }

		/// <summary>
		/// 更新者
		/// </summary>
		public string UpdateUserCd { get; set; }

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