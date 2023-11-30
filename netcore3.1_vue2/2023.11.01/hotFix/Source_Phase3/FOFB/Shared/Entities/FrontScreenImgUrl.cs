using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("frontscreenimgurl")]
	public class FrontScreenImgUrl
	{
		/// <summary>
		/// 画面レイアウト画像パスID
		/// </summary>
		[Key]
		public int ScrImgUrlId { get; set; }

		/// <summary>
		/// 画面レイアウトID
		/// </summary>
		public int ScrId { get; set; }

		/// <summary>
		/// 画像パス
		/// </summary>
		public string ImageUrl { get; set; }

		/// <summary>
		/// 画像表示箇所
		/// </summary>
		public string ImagePosition { get; set; }

		/// <summary>
		/// 表示順
		/// </summary>
		public int DspIndex { get; set; }

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
