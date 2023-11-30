using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("mkbnvalue")]
	public class MkbnValue
	{
		/// <summary>
		/// 区分コード
		/// </summary>
		[ExplicitKey]
		public string KbnCd { get; set; }

		/// <summary>
		/// 区分名称
		/// </summary>
		public string KbnName { get; set; }

		/// <summary>
		/// 区分値
		/// </summary>
		[Key]
		public string KbnValue { get; set; }

		/// <summary>
		/// 値に該当する名称
		/// </summary>
		public string KbnValueName { get; set; }

		/// <summary>
		/// パラメータの数値
		/// </summary>
		public int NumberValue { get; set; }

		/// <summary>
		/// パラメータのテキスト値
		/// </summary>
		public string StringValue { get; set; }

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
