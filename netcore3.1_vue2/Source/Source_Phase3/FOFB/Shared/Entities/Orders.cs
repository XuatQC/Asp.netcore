using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace FOFB.Shared.Entities
{
	[Table("orders")]
	public class Orders
	{
		/// <summary>
		/// 予約ID
		/// </summary>
		[ExplicitKey]
		public string OrderId { get; set; }

		/// <summary>
		/// お客様ID
		/// </summary>
		public int CustId { get; set; }

		/// <summary>
		/// 予約状態
		/// </summary>
		public string Status { get; set; }

		/// <summary>
		/// 店舗コード
		/// </summary>
		public string ShopCd { get; set; }

		/// <summary>
		/// バーコード
		/// </summary>
		public string BarCd { get; set; }

		/// <summary>
		/// 商品受け取り方法
		/// </summary>
		public string ReceiveWay { get; set; }

		/// <summary>
		/// 決済方法
		/// </summary>
		public string PaymentWay { get; set; }

		/// <summary>
		/// 早期割引特典
		/// </summary>
		public bool? IsDiscount { get; set; }

		/// <summary>
		/// 予約合計金額
		/// </summary>
		public decimal Total { get; set; }

		/// <summary>
		/// 合計数
		/// </summary>
		public int TotalQuantity { get; set; }

		/// <summary>
		/// メモ
		/// </summary>
		public string Memo { get; set; }

		/// <summary>
		/// 業態コード
		/// </summary>
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
		[Write(false)]
		[Computed]
		public DateTime UpdateDtTm { get; set; }

		/// <summary>
		/// 削除フラグ
		/// </summary>
		public bool DelFlg { get; set; }
	}
	public class OrdersSub : Orders
	{
		/// <summary>
		/// ブランドコード
		/// </summary>
		public string BrandCd { get; set; }
		/// <summary>
		/// ブランド名
		/// </summary>
		public string BrandName { get; set; }
		/// <summary>
		/// エリア2
		/// </summary>
		public int Area2Cd { get; set; }
		/// <summary>
		/// 店舗名
		/// </summary>
		public string ShopName { get; set; }
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
		/// メールアドレス
		/// </summary>
		public string MailAddress { get; set; }
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
		/// 都道府県名
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
		/// 姓（かな）
		/// </summary>
		public string KanaFamilyNameMCustReceive { get; set; }
		/// <summary>
		/// 名（かな）
		/// </summary>
		public string KanaFirstNameMCustReceive { get; set; }
		/// <summary>
		/// 姓（漢字）
		/// </summary>
		public string KanjiFamilyNameMCustReceive { get; set; }
		/// <summary>
		/// 名（漢字）
		/// </summary>
		public string KanjiFirstNameMCustReceive { get; set; }
		/// <summary>
		/// お電話番号
		/// </summary>
		public string PhoneNumberMCustReceive { get; set; }
		/// <summary>
		/// 郵便番号
		/// </summary>
		public string ZipCdMCustReceive { get; set; }
		/// <summary>
		/// 通常の郵便番号
		/// </summary>
		public string ZipCodeDspMCustReceive { get; set; }
		/// <summary>
		/// 都道府県
		/// </summary>
		public string ProvinceMCustReceive { get; set; }
		/// <summary>
		/// 都道府県名
		/// </summary>
		public string ProvinceNameMCustReceive { get; set; }
		/// <summary>
		/// 住所１
		/// </summary>
		public string AdrCd1MCustReceive { get; set; }
		/// <summary>
		/// 住所２
		/// </summary>
		public string AdrCd2MCustReceive { get; set; }
		/// <summary>
		/// 住所３(建物名等）
		/// </summary>
		public string AdrCd3MCustReceive { get; set; }
		/// <summary>
		/// 商品受け取り方法
		/// </summary>
		public string ReceiveWayName { get; set; }
		/// <summary>
		/// 決済方法
		/// </summary>
		public string PaymentWayName { get; set; }
		/// <summary>
		/// 早期割引特典
		/// </summary>
		public string DiscountName { get; set; }
		/// <summary>
		/// 予約ステータステキスト
		/// </summary>
		public string StatusName { get; set; }
		/// <summary>
		/// 予約メール送信ステータス
		/// </summary>
		public string OrderMailStatus { get; set; }
		/// <summary>
		/// 予約メール送信ステータス名
		/// </summary>
		public string OrderMailStatusName { get; set; }
		/// <summary>
		/// 入金リマインドメール送信ステータス
		/// </summary>
		public string RemindMailStatus { get; set; }
		/// <summary>
		/// 入金リマインドメール送信ステータス名
		/// </summary>
		public string RemindMailStatusName { get; set; }
		/// <summary>
		/// 入金完了メールのステータス
		/// </summary>
		public string FinishedMailStatus { get; set; }
		/// <summary>
		/// 入金完了メールのステータス名
		/// </summary>
		public string FinishedMailStatusName { get; set; }
		/// <summary>
		/// バウンスメールコード
		/// </summary>
		public string MailBounce { get; set; }
		/// <summary>
		/// バウンスメール名
		/// </summary>
		public string MailBounceName { get; set; }
		/// <summary>
		/// バウンスメール詳細名
		/// </summary>
		public string MailBounceDescription { get; set; }
		/// <summary>
		/// 作成者
		/// </summary>
		public string CreateUserCdCustRecieve { get; set; }
		/// <summary>
		/// 更新日時
		/// </summary>
		public DateTime UpdateDtTmCustRecieve { get; set; }
		/// <summary>
		/// 更新日時
		/// </summary>
		public DateTime UpdateDtTmAddress { get; set; }
		/// <summary>
		/// 更新日時
		/// </summary>
		public DateTime UpdateDtTmSendMail { get; set; }
		/// <summary>
		/// お客様名前
		/// </summary>
		public string CustName { get; set; }
		/// <summary>
		/// 注文情報
		/// </summary>
		public OrdersSub Order { get; set; }
		/// <summary>
		/// 注文詳細
		/// </summary>
		public List<OrderDetail> OrderDetails { get; set; }
		/// <summary>
		/// 削除された注文の詳細
		/// </summary>
		public List<OrderDetail> OrderDetailRemoves { get; set; }
		/// <summary>
		/// 注文情報
		/// </summary>
		public List<OrdersSub> OrdersSubs { get; set; }
		/// <summary>
		/// 注文情報
		/// </summary>
		public List<OrdersSub> OrderUnSelected { get; set; }
		/// <summary>
		/// メール送信状態
		/// </summary>
		public string ReserveMailStatus { get; set; }
		/// <summary>
		/// 注文番号一覧
		/// </summary>
		public string LstOrderId { get; set; }
		/// <summary>
		/// 注文番号一覧
		/// </summary>
		public string LstOrderIdUnCheck { get; set; }
		/// <summary>
		/// 業態名
		/// </summary>
		public string BussinessName { get; set; }
	}
}