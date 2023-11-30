using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("datetimesetting")]
	public class DatetimeSetting
	{

		/// <summary>
		/// 予約開始日時
		/// </summary>
		public DateTime OrdStartDtTm { get; set; }

		/// <summary>
		/// 予約終了日時
		/// </summary>
		public DateTime OrdEndDtTm { get; set; }

		/// <summary>
		/// 入金開始日時
		/// </summary>
		public DateTime RcvMoneyStartDtTm { get; set; }

		/// <summary>
		/// 入金終了日時
		/// </summary>
		public DateTime RcvMoneyEndDtTm { get; set; }

		/// <summary>
		/// 引渡開始日時
		/// </summary>
		//public DateTime DeliveryStartDtTm { get; set; }

		/// <summary>
		/// 業態コード
		/// </summary>
		[ExplicitKey]
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