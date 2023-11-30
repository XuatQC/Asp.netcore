using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace FOFB.Shared.Entities
{
	[Table("orderdetail")]
	public class OrderDetail
	{
		/// <summary>
		/// 予約詳細ID
		/// </summary>
		[Key]
		public int OrderDtlId { get; set; }

		/// <summary>
		/// 予約ID
		/// </summary>
		public string OrderId { get; set; }

		/// <summary>
		/// Sku
		/// </summary>
		public string Sku { get; set; }

		/// <summary>
		/// 予約個数
		/// </summary>
		public int Quantity { get; set; }

		/// <summary>
		/// 商品の金額
		/// </summary>
		public decimal Price { get; set; }

		/// <summary>
		/// 小計
		/// </summary>
		public decimal SubTotal { get; set; }

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

		[Write(false)]
		[Computed]
		/// <summary>
		/// 商品名
		/// </summary>
		public string ProductName { get; set; }

		[Write(false)]
		[Computed]
		/// <summary>
		/// 品番
		/// </summary>
		public string ProductCd { get; set; }

		[Write(false)]
		[Computed]
		/// <summary>
		/// サイズ名
		/// </summary>
		public string SizeName { get; set; }

		[Write(false)]
		[Computed]
		/// <summary>
		/// サイズコード
		/// </summary>
		public string SizeCd { get; set; }

		[Write(false)]
		[Computed]
		/// <summary>
		/// 色コード
		/// </summary>
		public string ColorCd { get; set; }
		[Write(false)]
		[Computed]
		/// <summary>
		/// 色名
		/// </summary>
		public string ColorName { get; set; }

		[Write(false)]
		[Computed]
		/// <summary>
		/// 注文番号一覧
		/// </summary>
		public string LstOrderId { get; set; }

		[Write(false)]
		[Computed]
		/// <summary>
		/// サイズコード一覧
		/// </summary>
		public string SizeCdArr { get; set; }

		[Write(false)]
		[Computed]
		/// <summary>
		/// サイズ名一覧
		/// </summary>
		public string SizeNameArr { get; set; }

		[Write(false)]
		[Computed]
		/// <summary>
		/// カーラーコード一覧
		/// </summary>
		public string ColorCdArr { get; set; }

		[Write(false)]
		[Computed]
		/// <summary>
		/// カーラー名一覧
		/// </summary>
		public string ColorNameArr { get; set; }

		[Write(false)]
		[Computed]
		/// <summary>
		/// 1サイズの予約可能点数
		/// </summary>
		public int MaxSizeCanOrder { get; set; }

		[Write(false)]
		[Computed]
		/// <summary>
		/// 1品番の予約可能金額
		/// </summary>
		public decimal MaxAmountCanOrder { get; set; }
	}
	public class OrderDetailSub : OrderDetail
	{
		/// <summary>
		/// お客様情報
		/// </summary>
		public MCustSub custInfo { get; set; }
		/// <summary>
		/// 商品受け情報
		/// </summary>
		public MCustrecieveSub custRecieveInfo { get; set; }
		/// <summary>
		/// 予約情報
		/// </summary>
		public OrdersSub dataOrder { get; set; }
		/// <summary>
		///  商品詳細一覧
		/// </summary>
		public List<OrderDetail> dataProduct { get; set; }
	}
}