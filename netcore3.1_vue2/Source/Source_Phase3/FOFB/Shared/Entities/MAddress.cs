using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("maddress")]
	public class MAddress
	{
		/// <summary>
		/// 住所ID
		/// </summary>
		[Key]
		public int AddrId { get; set; }

		/// <summary>
		/// 郵便番号
		/// </summary>
		public string ZipCd { get; set; }

		/// <summary>
		/// 通常の郵便番号
		/// </summary>
		public string ZipCodeDsp { get; set; }

		/// <summary>
		/// 都道府県
		/// </summary>
		public string Province { get; set; }

		/// <summary>
		/// 住所１
		/// </summary>
		public string AdrCd1 { get; set; }

		/// <summary>
		/// 住所２
		/// </summary>
		public string AdrCd2 { get; set; }

		/// <summary>
		/// 住所３(建物名等）
		/// </summary>
		public string AdrCd3 { get; set; }

		/// <summary>
		/// 住所タイプ
		/// </summary>
		public string AdrType { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string CreateUserCd { get; set; }

		/// <summary>
		/// 作成日時
		/// </summary>
		[Write(false)]
		[Computed]
		public DateTime CreateDtTm { get; set; }

		/// <summary>
		/// 更新者
		/// </summary>
		public string UpdateUserCd { get; set; }

		/// <summary>
		/// 更新日時
		/// </summary>
		[Write(false)]
		[Computed]
		public DateTime UpdateDtTm { get; set; }

		/// <summary>
		/// 削除フラグ
		/// </summary>
		public bool DelFlg { get; set; }
	}
}