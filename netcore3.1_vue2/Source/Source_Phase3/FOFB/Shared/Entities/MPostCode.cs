using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("mpostcode")]
	public class MPostCode
	{
		/// <summary>
		/// 郵便ID
		/// </summary>
		[Key]
		public int MPostId { get; set; }

		/// <summary>
		/// 郵便番号7桁
		/// </summary>
		public string ZipCd { get; set; }

		/// <summary>
		/// 住所CD
		/// </summary>
		public int AddressCd { get; set; }

		/// <summary>
		/// 都道府県CD
		/// </summary>
		public int RegionCd { get; set; }

		/// <summary>
		/// 市区町村CD
		/// </summary>
		public int CityCd { get; set; }

		/// <summary>
		/// 町域CD
		/// </summary>
		public int StreetCd { get; set; }

		/// <summary>
		/// 通常の郵便番号
		/// </summary>
		public string ZipCodeDsp { get; set; }

		/// <summary>
		/// 事業所フラグ
		/// </summary>
		public bool CompanyFlg { get; set; }

		/// <summary>
		/// 廃止フラグ
		/// </summary>
		public bool StopFlg { get; set; }

		/// <summary>
		/// 都道府県
		/// </summary>
		public string Province { get; set; }

		/// <summary>
		/// 都道府県カナ
		/// </summary>
		public string ProvinceKana { get; set; }

		/// <summary>
		/// 市区町村
		/// </summary>
		public string City { get; set; }

		/// <summary>
		/// 市区町村カナ
		/// </summary>
		public string CityKana { get; set; }

		/// <summary>
		/// 町域
		/// </summary>
		public string Town { get; set; }

		/// <summary>
		/// 町域カナ
		/// </summary>
		public string TownKana { get; set; }

		/// <summary>
		/// 町域補足
		/// </summary>
		public string TownAlter { get; set; }

		/// <summary>
		/// 京都通り名
		/// </summary>
		public string StreetName { get; set; }

		/// <summary>
		/// 字丁目
		/// </summary>
		public string Nom { get; set; }

		/// <summary>
		/// 字丁目カナ
		/// </summary>
		public string NomKana { get; set; }

		/// <summary>
		/// 補足
		/// </summary>
		public string Addition { get; set; }

		/// <summary>
		/// 事業所名
		/// </summary>
		public string OfficeName { get; set; }

		/// <summary>
		/// 事業所名カナ
		/// </summary>
		public string OfficeNameKana { get; set; }

		/// <summary>
		/// 事業所住所
		/// </summary>
		public string OfficePlace { get; set; }

		/// <summary>
		/// 事業所住所CD
		/// </summary>
		public string OfficePlaceCd { get; set; }

		/// <summary>
		/// 優先フラグ
		/// </summary>
		public bool IsPriority { get; set; }

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
