using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("frontscreen")]
	public class FrontScreen
	{
		/// <summary>
		/// 画面レイアウトID
		/// </summary>
		[Key]
		public int ScrId { get; set; }

		/// <summary>
		/// 画面タイプ
		/// </summary>
		public string ScrType { get; set; }

		/// <summary>
		/// ラベルテキスト1
		/// </summary>
		public string TextTitle1 { get; set; }

		/// <summary>
		/// ラベルテキスト2
		/// </summary>
		public string TextTitle2 { get; set; }

		/// <summary>
		/// ラベルテキスト3
		/// </summary>
		public string TextTitle3 { get; set; }

		/// <summary>
		/// スクロール内テキストエリア１
		/// </summary>
		public string TextArea1 { get; set; }

		/// <summary>
		/// スクロール内テキストエリア２
		/// </summary>
		public string TextArea2 { get; set; }

		/// <summary>
		/// ボタン1テキスト
		/// </summary>
		public string TextButton1 { get; set; }

		/// <summary>
		/// ボタン2テキスト
		/// </summary>
		public string TextButton2 { get; set; }

		/// <summary>
		/// 注意事項1テキスト
		/// </summary>
		public string TextNortify1 { get; set; }

		/// <summary>
		/// 注意事項2テキスト
		/// </summary>
		public string TextNortify2 { get; set; }

		/// <summary>
		/// チェックボックスの表示・非表示
		/// </summary>
		public int CheckBoxFlg1 { get; set; }

		/// <summary>
		/// チェックボックスの表示・非表示
		/// </summary>
		public int CheckBoxFlg2 { get; set; }

		/// <summary>
		/// ボタンの表示・非表示
		/// </summary>
		public int ButtonFlg { get; set; }

		/// <summary>
		/// 業態コード
		/// </summary>
		public string BussinessCd { get; set; }

		/// <summary>
		/// 商品コード
		/// </summary>
		public string ProductCd { get; set; }

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

	public class FrontScreenSub : FrontScreen
	{
		/// <summary>
		/// 画像表示箇所
		/// </summary>
		public string ImagePosition { get; set; }

		/// <summary>
		/// 画像パス
		/// </summary>
		public string ImageUrl { get; set; }
		/// <summary>
		/// 画面レイアウト画像パスID
		/// </summary>
		public string ScrImgUrlId { get; set; }

		/// <summary>
		/// 表示順
		/// </summary>
		public int DspIndex { get; set; }
	}
}
