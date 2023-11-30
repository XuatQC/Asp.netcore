using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("rolesactiondetail")]
	public class RolesActionDetail
	{

		/// <summary>
		/// RoleActDtlId
		/// </summary>
		[Key]
		public int RoleActDtlId { get; set; }

		/// <summary>
		/// 権限ID
		/// </summary>
		public string ActionUrl { get; set; }

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