using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("mbrand")]
	public class MBrand
	{

		/// <summary>
		/// ブランドコード
		/// </summary>
		[ExplicitKey]
		public string BrandCd { get; set; }

		/// <summary>
		/// ブランド名
		/// </summary>
		public string BrandName { get; set; }

		/// <summary>
		/// サブブランド
		/// </summary>
		[Write(false)]
		[Computed]
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
	public class MBrandSub : MBrand
	{
		/// <summary>
		/// Id
		/// </summary>
		public int BrandRltId { get; set; }
		
		/// <summary>
		/// 業態コード
		/// </summary>
		public string BussinessCd { get; set; }
	}
}