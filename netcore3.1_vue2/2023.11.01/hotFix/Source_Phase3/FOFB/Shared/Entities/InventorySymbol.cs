using Dapper.Contrib.Extensions;
using System;

namespace FOFB.Shared.Entities
{
	[Table("inventorysymbol")]
	public class InventorySymbol
	{
		/// <summary>
		/// �݌ɐ�
		/// </summary>
		public string InventoryNumCheck { get; set; }

		/// <summary>
		/// �ƑԃR�[�h
		/// </summary>
		[ExplicitKey]
		public string BussinessCd { get; set; }

		/// <summary>
		/// �쐬��
		/// </summary>
		public string CreateUserCd { get; set; }

		/// <summary>
		/// �쐬����
		/// </summary>
		public DateTime CreateDtTm { get; set; }

		/// <summary>
		/// �X�V��
		/// </summary>
		public string UpdateUserCd { get; set; }

		/// <summary>
		/// �X�V����
		/// </summary>
		public DateTime UpdateDtTm { get; set; }

		/// <summary>
		/// �폜�t���O
		/// </summary>
		public bool DelFlg { get; set; }

	}
}