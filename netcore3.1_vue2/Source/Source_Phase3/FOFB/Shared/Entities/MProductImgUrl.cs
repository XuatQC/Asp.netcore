using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("mproductimgurl")]
	public class MProductImgUrl
	{
		/// <summary>
		/// 商品画像パスId
		/// </summary>
		[Key]
		public int ProductImgUrlId { get; set; }

		/// <summary>
		/// 品番
		/// </summary>
		public string ProductCd { get; set; }

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
		/// co chứa file
		/// </summary>
		[Write(false)]
		[Computed]
		public bool IsHaveFile { get; set; }
	}
}
