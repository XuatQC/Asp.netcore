using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace FOFB.Shared.Entities
{
	[Table("roles")]
	public class Roles
	{

		/// <summary>
		/// 権限ID
		/// </summary>
		[Key]
		public int RoleId { get; set; }

		/// <summary>
		/// 権限名
		/// </summary>
		public string RoleName { get; set; }

		/// <summary>
		/// 記述
		/// </summary>
		public string RoleDescript { get; set; }

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

	public class RolesSub : Roles
	{
		/// <summary>
		/// 実施可能操作
		/// </summary>
		public string ActionType { get; set; }
		/// <summary>
		/// 実施可能操作
		/// </summary>
		public List<string> ActionTypeArr { get; set; }
		/// <summary>
		/// 実施可能操作
		/// </summary>
		public List<string> OldActionTypeArr { get; set; }
	}
}