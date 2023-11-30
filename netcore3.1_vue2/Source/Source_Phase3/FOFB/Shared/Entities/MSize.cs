using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("msize")]
	public class MSize
	{

		/// <summary>
		/// サイズコード
		/// </summary>
		[ExplicitKey]
		public string SizeCd { get; set; }

		/// <summary>
		/// サイズ名
		/// </summary>
		public string SizeName { get; set; }

		/// <summary>
		/// 並び順
		/// </summary>
		public int SortIndex { get; set; }

		/// <summary>
		/// サイズ分類区分
		/// </summary>
		public string TypeKbn { get; set; }

		/// <summary>
		/// サイズ分類
		/// </summary>
		public string Category { get; set; }

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