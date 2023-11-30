using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("mbrandrelation")]
	public class MBrandRelation
	{

		/// <summary>
		/// Id
		/// </summary>
		[Key]
		public int BrandRltId { get; set; }

		/// <summary>
		/// ブランドコード
		/// </summary>
		public string BrandCd { get; set; }

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