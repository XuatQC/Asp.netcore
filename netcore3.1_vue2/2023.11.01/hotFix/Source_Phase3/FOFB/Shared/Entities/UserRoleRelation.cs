using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("userrolerelation")]
	public class UserRoleRelation
	{

		/// <summary>
		/// ユーザーによる権限ID
		/// </summary>
		[Key]
		public int UserRoleRltId { get; set; }

		/// <summary>
		/// 権限ID
		/// </summary>
		public int RoleId { get; set; }

		/// <summary>
		/// ユーザーコード
		/// </summary>
		public string UserCd { get; set; }

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