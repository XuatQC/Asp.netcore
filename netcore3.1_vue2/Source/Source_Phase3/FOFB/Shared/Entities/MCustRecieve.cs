using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("mcustrecieve")]
	public class MCustRecieve
	{
		/// <summary>
		/// 受取人ID
		/// </summary>
		[Key]
		public int RecieveId { get; set; }

		/// <summary>
		/// 予約ID
		/// </summary>
		public string OrderId { get; set; }

		/// <summary>
		/// 姓（かな）
		/// </summary>
		public string KanaFamilyName { get; set; }

		/// <summary>
		/// 名（かな）
		/// </summary>
		public string KanaFirstName { get; set; }

		/// <summary>
		/// 姓（漢字）
		/// </summary>
		public string KanjiFamilyName { get; set; }

		/// <summary>
		/// 名（漢字）
		/// </summary>
		public string KanjiFirstName { get; set; }

		/// <summary>
		/// お電話番号
		/// </summary>
		public string PhoneNumber { get; set; }

		/// <summary>
		/// お届け先ID
		/// </summary>
		public int AddrId { get; set; }

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
	public class MCustrecieveSub : MCustRecieve
	{
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
		/// 都道府県
		/// </summary>
		public string ProvinceName { get; set; }

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
		/// 更新日時
		/// </summary>
		public DateTime UpdateDtTmAddress { get; set; }
	}
}