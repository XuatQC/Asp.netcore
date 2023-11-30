using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("rolesdetail")]
	public class RolesDetail
	{

		/// <summary>
		/// 権限詳細ID
		/// </summary>
		[Key]
		public int RoleDtlId { get; set; }

		/// <summary>
		/// 権限ID
		/// </summary>
		public int RoleId { get; set; }

		/// <summary>
		/// 実施可能操作
		/// </summary>
		public string ActionType { get; set; }

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