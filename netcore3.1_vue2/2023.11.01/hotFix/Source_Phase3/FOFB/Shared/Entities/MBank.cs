using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("mbank")]
	public class MBank
	{
		/// <summary>
		/// 業態コード
		/// </summary>
		public string BussinessCd { get; set; }
		/// <summary>
		/// 金融機関名
		/// </summary>
		public string BankName { get; set; }

		/// <summary>
		/// 口座番号
		/// </summary>
		public string AccountNumber { get; set; }

		/// <summary>
		/// 口座名義
		/// </summary>
		public string AccountName { get; set; }

		/// <summary>
		/// 支店名
		/// </summary>
		public string Branch { get; set; }

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
