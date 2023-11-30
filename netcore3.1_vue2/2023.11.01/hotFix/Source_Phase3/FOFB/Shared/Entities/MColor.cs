using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("mcolor")]
	public class MColor
	{

		/// <summary>
		/// 色コード
		/// </summary>
		[Key]
		public string ColorCd { get; set; }

		/// <summary>
		/// 色名
		/// </summary>
		public string ColorName { get; set; }

		/// <summary>
		/// 並び順
		/// </summary>
		public int SortIndex { get; set; }

		/// <summary>
		/// 色分類
		/// </summary>
		public string ColorGroup { get; set; }

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
		public DateTime UpdateDtTm { get; set; }

		/// <summary>
		/// 削除フラグ
		/// </summary>
		public bool DelFlg { get; set; }

	}
}