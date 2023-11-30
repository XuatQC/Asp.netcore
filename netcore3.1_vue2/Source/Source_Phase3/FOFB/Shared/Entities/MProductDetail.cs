using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("mproductdetail")]
	public class MProductDetail
	{
		/// <summary>
		/// SKU
		/// </summary>
		[ExplicitKey]
		public string Sku { get; set; }

		/// <summary>
		/// 品番
		/// </summary>
		public string ProductCd { get; set; }

		/// <summary>
		/// 色コード
		/// </summary>
		public string ColorCd { get; set; }

		/// <summary>
		/// 在庫数合計
		/// </summary>
		public int InventoryNumber { get; set; }

		/// <summary>
		/// サイズコード
		/// </summary>
		public string SizeCd { get; set; }

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

		/// <summary>
		/// 品番
		/// </summary>
		[Write(false)]
		[Computed]
		public string LstProductCd { get; set; }
	}
}