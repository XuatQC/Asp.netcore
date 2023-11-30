using Dapper.Contrib.Extensions;
using System;


namespace FOFB.Shared.Entities
{
	[Table("menu")]
	public class Menu
	{

		/// <summary>
		/// メニューID
		/// </summary>
		[Key]
		public int MenuId { get; set; }

		/// <summary>
		/// メニュー名
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 表示順
		/// </summary>
		public int DisplayOrder { get; set; }

		/// <summary>
		/// メニューID
		/// </summary>
		public int ParrentId { get; set; }

		/// <summary>
		/// パス
		/// </summary>
		public string Href { get; set; }

		/// <summary>
		/// アイコン
		/// </summary>
		public string Icon { get; set; }

		/// <summary>
		/// 部署
		/// </summary>
		public int Department { get; set; }

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

		/// <summary>
		/// 権限ID
		/// </summary>
		public int RoleId { get; set; }


	}
}