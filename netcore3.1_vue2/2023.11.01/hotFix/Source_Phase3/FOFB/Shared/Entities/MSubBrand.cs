using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("msubbrand")]
	public class MSubBrand
	{

		/// <summary>
		/// Id
		/// </summary>
		[Key]
		public string SubBrandId { get; set; }

		/// <summary>
		/// ブランドコード
		/// </summary>
		public string BrandCd { get; set; }

		/// <summary>
		/// サブブランド
		/// </summary>
		public string SubBrand { get; set; }

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