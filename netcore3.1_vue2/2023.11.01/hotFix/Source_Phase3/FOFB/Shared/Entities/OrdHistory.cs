using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("ordhistory")]
	public class OrdHistory
	{
		/// <summary>
		/// 予約履歴ID
		/// </summary>
		[Key]
		public int OrdHistId { get; set; }

		/// <summary>
		/// 予約番号
		/// </summary>
		public string OrderId { get; set; }

		/// <summary>
		/// 履歴タイプ
		/// </summary>
		public string HistType { get; set; }

		/// <summary>
		/// 旧状態
		/// </summary>
		public string LastStatus { get; set; }

		/// <summary>
		/// 新状態
		/// </summary>
		public string UpdatedStatus { get; set; }

		/// <summary>
		/// メールID
		/// </summary>
		public int? MailId { get; set; }

		/// <summary>
		/// メールタイプ
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
		/// 品番
		/// </summary>
		public string ProductCd { get; set; }

		/// <summary>
		/// サイズ
		/// </summary>
		public string Size { get; set; }

		/// <summary>
		/// 色
		/// </summary>
		public string Color { get; set; }

		/// <summary>
		/// 旧数量（変更前）
		/// </summary>
		public int LastQuantity { get; set; }

		/// <summary>
		/// 新数量（変更後）
		/// </summary>
		public int UpdatedlQuantity { get; set; }

		/// <summary>
		/// サイズ（変更前）
		/// </summary>
		public string LastSize { get; set; }

		/// <summary>
		/// サイズ（変更後）
		/// </summary>
		public string UpdatedSize { get; set; }

		/// <summary>
		/// 旧色（変更前）
		/// </summary>
		public string LastColor { get; set; }

		/// <summary>
		/// 新色（変更後）
		/// </summary>
		public string UpdatedColor { get; set; }

		/// <summary>
		/// 旧店舗コード（変更前）
		/// </summary>
		public string LastShopCd { get; set; }

		/// <summary>
		/// 新店舗コード（変更後）
		/// </summary>
		public string UpdatedShopCd { get; set; }

		/// <summary>
		/// 更新部署
		/// </summary>
		public int Department { get; set; }

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
	public class OrdHistorySub : OrdHistory
	{
		/// <summary>
		/// 予約履歴タイプ
		/// </summary>
		public string HistTypeName { get; set; }
		/// <summary>
		/// メールタイプ名
		/// </summary>
		public string MailTypeName { get; set; }
		/// <summary>
		/// 商品名称
		/// </summary>
		public string ProductName { get; set; }
		/// <summary>
		/// 店名（変更前）
		/// </summary>
		public string LastShopName { get; set; }
		/// <summary>
		/// 店名（変更後）
		/// </summary>
		public string UpdatedShopName { get; set; }

		/// <summary>
		/// 決済方法
		/// </summary>
		public string PaymentWay { get; set; }

		/// <summary>
		/// 商品受け取り方法
		/// </summary>
		public string ReceiveWay { get; set; }
	}
}