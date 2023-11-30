using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("muserlogin")]
	public class MUserLogin
	{
		/// <summary>
		/// ユーザーコード
		/// </summary>
		[ExplicitKey]
		public string UserCd { get; set; }

		/// <summary>
		/// パスワード
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// 部署
		/// </summary>
		public int Department { get; set; }

		/// <summary>
		/// アクセストークンリフレッシュ
		/// </summary>
		public string RefreshToken { get; set; }

		/// <summary>
		/// トークン有効期限
		/// </summary>
		public DateTime RefreshTokenExpiryTime { get; set; }

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
	public class MuserLoginSub : MUserLogin
	{
		/// <summary>
		/// RoleId
		/// </summary>
		public int RoleId { get; set; }

		/// <summary>
		/// BussinessCd
		/// </summary>
		public string BussinessCd { get; set; }

		/// <summary>
		/// List permission of user
		/// </summary>
		public string Permissions { get; set; }

		/// <summary>
		/// Kannji name
		/// </summary>
		public string UserNameKanji { get; set; }

		/// <summary>
		/// Kana name
		/// </summary>
		public string UserNameKana { get; set; }

		/// <summary>
		/// AccessToken
		/// </summary>
		public string AccessToken { get; set; }
	}
}
