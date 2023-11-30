using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace FOFB.Shared.Entities
{
	[Table("mproduct")]
	public class MProduct
	{
		/// <summary>
		/// 品番
		/// </summary>
		[ExplicitKey]
		public string ProductCd { get; set; }

		/// <summary>
		/// 商品名
		/// </summary>
		public string ProductName { get; set; }

		/// <summary>
		/// 商品名（略称）
		/// </summary>
		public string ProductName1 { get; set; }

		/// <summary>
		/// 年
		/// </summary>
		public int Year { get; set; }

		/// <summary>
		/// ブランドID
		/// </summary>
		public int BrandRltId { get; set; }

		/// <summary>
		/// 価格
		/// </summary>
		public decimal Price { get; set; }

		/// <summary>
		/// 表示ステータス
		/// </summary>
		public bool DisplayFlg { get; set; }

		/// <summary>
		/// 1サイズの予約可能点数
		/// </summary>
		public int MaxSizeCanOrder { get; set; }

		/// <summary>
		/// 1品番の予約可能金額
		/// </summary>
		public decimal MaxAmountCanOrder { get; set; }
		/// <summary>
		/// Id
		/// </summary>
		public string SubBrandId { get; set; }

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

	public class MProductSub : MProduct
	{
		/// <summary>
		/// SKU
		/// </summary>
		public string Sku { get; set; }
		/// <summary>
		/// 業態コード
		/// </summary>
		public string BussinessCd { get; set; }

		/// <summary>
		/// 画像パス
		/// </summary>
		public string ImageUrl { get; set; }

		/// <summary>
		/// 商品一覧画面のメイン画像
		/// </summary>
		public bool IsMainInListPage { get; set; }

		/// <summary>
		/// 商品詳細画面のメイン画像
		/// </summary>
		public bool IsMainInDetailPage { get; set; }

		/// <summary>
		/// ブランドコード
		/// </summary>
		public string BrandCd { get; set; }

		/// <summary>
		/// ブランド名
		/// </summary>
		public string BrandName { get; set; }

		/// <summary>
		/// サブブランド
		/// </summary>
		public string SubBrand { get; set; }

		/// <summary>
		/// サイズ名
		/// </summary>
		public string SizeName { get; set; }

		/// <summary>
		/// サイズコード
		/// </summary>
		public string SizeCd { get; set; }

		/// <summary>
		/// 色名
		/// </summary>
		public string ColorName { get; set; }

		/// <summary>
		/// 色コード
		/// </summary>
		public string ColorCd { get; set; }

		/// <summary>
		/// 複数商品の在庫数
		/// </summary>
		public string InventoryNumberSub { get; set; }

		/// <summary>
		/// tổng số tồn kho
		/// </summary>
		public int TotalInventoryNumber { get; set; }
		/// <summary>
		/// danh sách chi tiết sản phẩm
		/// </summary>
		public List<MProductDetail> LstMProductDetail { get; set; }
		/// <summary>
		/// danh sách sản phẩm xóa
		/// </summary>
		public List<MProductDetail> lstProductDelete { get; set; }
		/// <summary>
		/// data sản phẩm
		/// </summary>
		public MProduct DataProduct { get; set; }
		/// <summary>
		/// 価格
		/// </summary>
		public string TxtPrice { get; set; }
		/// <summary>
		/// 在庫数合計
		/// </summary>
		public string InventoryNumber { get; set; }
		/// <summary>
		/// 年
		/// </summary>
		public string TxtYear { get; set; }
		/// <summary>
		/// 表示ステータス
		/// </summary>
		public string TxtDisplay { get; set; }
		/// <summary>
		/// Error
		/// </summary>
		public string TxtError{ get; set; }

	}
}
