using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace FOFB.Shared.Entities
{
	[Table("madminuser")]
	public class MAdminUser
	{

		/// <summary>
		/// 管理者コード
		/// </summary>
		[ExplicitKey]
		public string UserCd { get; set; }

		/// <summary>
		/// 名前（漢字）
		/// </summary>
		public string UserNameKanji { get; set; }

		/// <summary>
		/// 名前（かな）
		/// </summary>
		public string UserNameKana { get; set; }

		/// <summary>
		/// メールアドレス
		/// </summary>
		public string EmailAddress { get; set; }

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

		/// <summary>
		/// 店舗コード
		/// </summary>
		[Write(false)]
		[Computed]
		public string ListUserCd { get; set; }
	}
	public class MAdminUserSub : MAdminUser
	{
		/// <summary>
		/// 権限ID
		/// </summary>
		public int RoleId { get; set; }
		public string RoleIdTxt { get; set; }
		/// <summary>
		/// 権限名
		/// </summary>
		public string RoleName { get; set; }
		/// <summary>
		/// パスワード
		/// </summary>
		public string Password { get; set; }
		public string TxtError { get; set; }
		/// <summary>
		/// 追加
		/// </summary>
		public bool IsAdd { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public List<MAdminUserSub> MUserSubmit { get; set; }
	}
}