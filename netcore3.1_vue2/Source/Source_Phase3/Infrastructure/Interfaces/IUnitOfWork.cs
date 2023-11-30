using FOFB.Shared.Entities;

namespace Infrastructure.Interfaces
{
	public interface IUnitOfWork
	{
		SettingsConfig Config { get; }
		IDatetimeSettingRepository DatetimeSetting { get; }
		IFrontScreenRepository FrontScreen { get; }
		IFrontScreenImgUrlRepository FrontScreenImgUrl { get; }
		IInventorySymbolRepository InventorySymbol { get; }
		IMAddressRepository MAddress { get; }
		IMAdminUserRepository MAdminUser { get; }
		IMailSettingRepository MailSetting { get; }
		IMailTemplateRepository MailTemplate { get; }
		IMBrandRepository MBrand { get; }
		IMBrandRelationRepository MBrandRelation { get; }
		IMBussinessRepository MBussiness { get; }
		IMColorRepository MColor { get; }
		IMCustRepository MCust { get; }
		IMCustRecieveRepository MCustRecieve { get; }
		IMenuRepository Menu { get; }
		IMkbnValueRepository MkbnValue { get; }
		IMProductRepository MProduct { get; }
		IMProductDetailRepository MProductDetail { get; }
		IMProductImgUrlRepository MProductImgUrl { get; }
		IMShopRepository MShop { get; }
		IMSizeRepository MSize { get; }
		IMUserLoginRepository MUserLogin { get; }
		IOrdersRepository Orders { get; }
		IOrderDetailRepository OrderDetail { get; }
		IOrdHistoryRepository OrdHistory { get; }
		IRolesRepository Roles { get; }
		IRolesDetailRepository RolesDetail { get; }
		ISendMailRepository SendMail { get; }
		IUrlsettingRepository UrlSetting { get; }
		IUserRoleRelationRepository UserRoleRelation { get; }
		IMPostCodeRepository MPostCode { get; }
		IRolesActionDetailRepository RolesActionDetail { get; }
		IMSubBrandRepository MSubBrand { get; }
		IMBankRepository MBank { get; }
		IMenuRoleRelationRepository MenuRoleRelation { get; }
	}
}
