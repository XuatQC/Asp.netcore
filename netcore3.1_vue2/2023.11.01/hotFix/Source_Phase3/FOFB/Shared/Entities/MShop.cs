using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace FOFB.Shared.Entities
{
	[Table("mshop")]
	public class MShop
	{
		/// <summary>
		/// 店舗コード
		/// </summary>
		[ExplicitKey]
		public string ShopCd { get; set; }
		/// <summary>
		/// 店名
		/// </summary>
		public string ShopName { get; set; }
		/// <summary>
		/// エリア
		/// </summary>
		public int AreaCd { get; set; }
		/// <summary>
		/// エリア2
		/// </summary>
		public int Area2Cd { get; set; }
		/// <summary>
		/// 店舗住所コード
		/// </summary>
		public int AddrId { get; set; }
		/// <summary>
		/// メールアドレス
		/// </summary>
		public string MailAddress { get; set; }
		/// <summary>
		/// お電話番号
		/// </summary>
		public string PhoneNumber { get; set; }
		/// <summary>
		/// Fax
		/// </summary>
		public string FaxNumber { get; set; }
		/// <summary>
		/// 出退店区分
		/// </summary>
		public bool? Status { get; set; }
		/// <summary>
		/// 開店日
		/// </summary>
		public string StartDate { get; set; }
		/// <summary>
		/// 閉店日
		/// </summary>
		public string EndDate { get; set; }
		/// <summary>
		/// 所属部署
		/// </summary>
		public string AffiliateDepartmentCd { get; set; }
		/// <summary>
		/// 所属部門名
		/// </summary>
		public string AffiliateDepartmentName { get; set; }
		/// <summary>
		/// 営業時間(from-to)
		/// </summary>
		public string OpenTime { get; set; }
		/// <summary>
		/// 坪数
		/// </summary>
		public float? Square { get; set; }
		/// <summary>
		/// 業態コード
		/// </summary>
		public string BussinessCd { get; set; }
		/// <summary>
		/// 営業担当者
		/// </summary>
		public string SalePersonCd { get; set; }
		/// <summary>
		/// 営業担当者名
		/// </summary>
		public string SalePersonName { get; set; }
		/// <summary>
		/// 免税使用
		/// </summary>
		public bool? UsedDutyFree { get; set; }
		/// <summary>
		/// 契約形態
		/// </summary>
		public int? ContractType { get; set; }
		/// <summary>
		/// 表示ステータス
		/// </summary>
		public bool? DisplayFlg { get; set; }
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
		/// 店舗コード
		/// </summary>
		[Write(false)]
		[Computed]
		public string ListShopCd { get; set; }
	}

	public class MShopSub : MShop
	{
		/// <summary>
		/// エリア2
		/// </summary>
		public string Area2Name { get; set; }
		/// <summary>
		/// 住所
		/// </summary>
		public string Address { get; set; }
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
		/// 更新日時
		/// </summary>
		public DateTime UpdateDtTmAddress { get; set; }
		/// <summary>
		/// Error
		/// </summary>
		public string TxtError { get; set; }
		/// <summary>
		/// テキストエリア2
		/// </summary>
		public string TxtArea2Cd { get; set; }
		/// <summary>
		/// テキストエリア
		/// </summary>
		public string TxtAreaCd { get; set; }
		/// <summary>
		/// テキスト出退店区分
		/// </summary>
		public string TextStatus { get; set; }
		/// <summary>
		/// テキスト坪数
		/// </summary>
		public string TextSquare { get; set; }
		/// <summary>
		/// テキスト免税使用
		/// </summary>
		public string TextUsedDutyFree { get; set; }
		/// <summary>
		/// テキスト契約形態
		/// </summary>
		public string TextContractType { get; set; }
		/// <summary>
		/// テキスト表示ステータス
		/// </summary>
		public string TextDisplayFlg { get; set; }
		/// <summary>
		/// 追加
		/// </summary>
		public bool IsAdd { get; set; }
		/// <summary>
		/// 店舗
		/// </summary>
		public List<MShopSub> MShopSubmit { get; set; }
	}
}
