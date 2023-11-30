
namespace FOFB.Server.Services.Interfaces
{
    public interface IService
    {
        IDatetimeSettingService DatetimeSetting { get; }
        IFrontScreenService FrontScreen { get; }
        IFrontScreenImgUrlService FrontScreenImgUrl { get; }
        IInventorySymbolService InventorySymbol { get; }
        IMAddressService MAddress { get; }
        IMAdminUserService MAdminUser { get; }
        IMailTemplateService Mailtemplate { get; }
        IMBrandService MBrand { get; }
        IMBrandRelationService MBrandRelation { get; }
        IMBussinessService MBussiness { get; }
        IMColorService MColor { get; }
        IMCustService MCust { get; }
        IMCustRecieveService MCustRecieve { get;}
        IMenuService Menu { get; }
        IMkbnValueService MkbnValue { get; }
        IMPostCodeService MPostCode { get; }
        IMProductService MProduct { get; }
        IMProductDetailService MProductDetail { get; }
        IMProductImgUrlService MProductImgUrl { get; }
        IMShopService MShop { get; }
        IMSizeService MSize { get; }
        IMUserLoginService MUserLogin { get; }
        IOrdersService Orders { get; }
        IOrderDetailService OrderDetail { get; }
        IOrdHistoryService OrdHistory { get; }
        IRolesService Roles { get; }
        IRolesDetailService RolesDetail { get; }
        ISendMailService SendMail { get; }
        IUrlSettingService UrlSetting { get; }
        IUserRoleRelationService UserRoleRelation { get; }
        IRolesActionDetailService RolesActionDetail { get; }
        IMailSettingService MailSetting { get; }
        IMSubBrandService SubBrand { get; }
        IMBankService MBank { get; }
        IBaseService BaseService { get; }
    }
}
