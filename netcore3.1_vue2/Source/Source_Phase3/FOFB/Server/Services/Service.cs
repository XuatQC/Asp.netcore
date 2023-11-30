
using FOFB.Server.Services.Interfaces;
using Infrastructure.Interfaces;

namespace FOFB.Server.Services
{
	public class Service : BaseService, IService
	{
		public Service(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}
		private IBaseService _baseService;
		public IBaseService BaseService 
		{
			get
			{
				_baseService = _baseService ?? new BaseService(this.UnitOfWork);
				return _baseService;
			}
		}

		private IFrontScreenService _frontScreen;
		public IFrontScreenService FrontScreen
		{
			get
			{
				_frontScreen = _frontScreen ?? new FrontScreenService(this.UnitOfWork);
				return _frontScreen;
			}
		}
		private IDatetimeSettingService _datetimeSetting;
		public IDatetimeSettingService DatetimeSetting
		{
			get
			{
				_datetimeSetting = _datetimeSetting ?? new DatetimeSettingService(this.UnitOfWork);
				return _datetimeSetting;
			}
		}
		private IMkbnValueService _mkbnvalue;
		public IMkbnValueService Mkbnvalue
		{
			get
			{
				_mkbnvalue = _mkbnvalue ?? new MkbnValueService(this.UnitOfWork);
				return _mkbnvalue;
			}
		}
		private IMProductService _mProduct;
		public IMProductService MProduct
		{
			get
			{
				_mProduct = _mProduct ?? new MProductService(this.UnitOfWork);
				return _mProduct;
			}
		}
		private IMProductImgUrlService _mProductImgUrl;
		public IMProductImgUrlService MProductImgUrl
		{
			get
			{
				_mProductImgUrl = _mProductImgUrl ?? new MProductImgUrlService(this.UnitOfWork);
				return _mProductImgUrl;
			}
		}
		private IInventorySymbolService _inventorysymbol;
		public IInventorySymbolService InventorySymbol
		{
			get
			{
				_inventorysymbol = _inventorysymbol ?? new InventorySymbolService(this.UnitOfWork);
				return _inventorysymbol;
			}
		}
		private IFrontScreenImgUrlService _frontScreenImgUrl;
		public IFrontScreenImgUrlService FrontScreenImgUrl
		{
			get
			{
				_frontScreenImgUrl = _frontScreenImgUrl ?? new FrontScreenImgUrlService(this.UnitOfWork);
				return _frontScreenImgUrl;
			}
		}
		private IMAddressService _mAddress;
		public IMAddressService MAddress
		{
			get
			{
				_mAddress = _mAddress ?? new MAddressService(this.UnitOfWork);
				return _mAddress;
			}
		}
		private IMAdminUserService _mAdminUser;
		public IMAdminUserService MAdminUser
		{
			get
			{
				_mAdminUser = _mAdminUser ?? new MAdminUserService(this.UnitOfWork);
				return _mAdminUser;
			}
		}
		private IMailTemplateService _mailtemplate;
		public IMailTemplateService Mailtemplate
		{
			get
			{
				_mailtemplate = _mailtemplate ?? new MailTemplateService(this.UnitOfWork);
				return _mailtemplate;
			}
		}
		private IMBrandService _mBrand;
		public IMBrandService MBrand
		{
			get
			{
				_mBrand = _mBrand ?? new MBrandService(this.UnitOfWork);
				return _mBrand;
			}
		}
		private IMBrandRelationService _mBrandRelation;
		public IMBrandRelationService MBrandRelation
		{
			get
			{
				_mBrandRelation = _mBrandRelation ?? new MBrandRelationService(this.UnitOfWork);
				return _mBrandRelation;
			}
		}
		private IMBussinessService _mBussiness;
		public IMBussinessService MBussiness
		{
			get
			{
				_mBussiness = _mBussiness ?? new MBussinessService(this.UnitOfWork);
				return _mBussiness;
			}
		}
		private IMColorService _mColor;
		public IMColorService MColor
		{
			get
			{
				_mColor = _mColor ?? new MColorService(this.UnitOfWork);
				return _mColor;
			}
		}
		private IMCustService _mCust;
		public IMCustService MCust
		{
			get
			{
				_mCust = _mCust ?? new MCustService(this.UnitOfWork);
				return _mCust;
			}
		}
		private IMCustRecieveService _mCustRecieve;
		public IMCustRecieveService MCustRecieve
		{
			get
			{
				_mCustRecieve = _mCustRecieve ?? new MCustRecieveService(this.UnitOfWork);
				return _mCustRecieve;
			}
		}
		private IMenuService _menu;
		public IMenuService Menu
		{
			get
			{
				_menu = _menu ?? new MenuService(this.UnitOfWork);
				return _menu;
			}
		}
		private IMkbnValueService _mkbnValue;
		public IMkbnValueService MkbnValue
		{
			get
			{
				_mkbnValue = _mkbnValue ?? new MkbnValueService(this.UnitOfWork);
				return _mkbnValue;
			}
		}
		private IMPostCodeService _mPostCode;
		public IMPostCodeService MPostCode
		{
			get
			{
				_mPostCode = _mPostCode ?? new MPostCodeService(this.UnitOfWork);
				return _mPostCode;
			}
		}
		private IMProductDetailService _mProductDetail;
		public IMProductDetailService MProductDetail
		{
			get
			{
				_mProductDetail = _mProductDetail ?? new MProductDetailService(this.UnitOfWork);
				return _mProductDetail;
			}
		}
		private IMShopService _mShop;
		public IMShopService MShop
		{
			get
			{
				_mShop = _mShop ?? new MShopService(this.UnitOfWork);
				return _mShop;
			}
		}
		private IMSizeService _mSize;
		public IMSizeService MSize
		{
			get
			{
				_mSize = _mSize ?? new MSizeService(this.UnitOfWork);
				return _mSize;
			}
		}
		private IMUserLoginService _mUserLogin;
		public IMUserLoginService MUserLogin
		{
			get
			{
				_mUserLogin = _mUserLogin ?? new MUserLoginService(this.UnitOfWork);
				return _mUserLogin;
			}
		}
		private IOrdersService _orders;
		public IOrdersService Orders
		{
			get
			{
				_orders = _orders ?? new OrdersService(this.UnitOfWork);
				return _orders;
			}
		}
		private IOrderDetailService _orderDetail;
		public IOrderDetailService OrderDetail
		{
			get
			{
				_orderDetail = _orderDetail ?? new OrderDetailService(this.UnitOfWork);
				return _orderDetail;
			}
		}
		private IOrdHistoryService _ordHistory;
		public IOrdHistoryService OrdHistory
		{
			get
			{
				_ordHistory = _ordHistory ?? new OrdHistoryService(this.UnitOfWork);
				return _ordHistory;
			}
		}
		private IRolesService _roles;
		public IRolesService Roles
		{
			get
			{
				_roles = _roles ?? new RolesService(this.UnitOfWork);
				return _roles;
			}
		}
		private IRolesDetailService _rolesDetail;
		public IRolesDetailService RolesDetail
		{
			get
			{
				_rolesDetail = _rolesDetail ?? new RolesDetailService(this.UnitOfWork);
				return _rolesDetail;
			}
		}
		private ISendMailService _sendMail;
		public ISendMailService SendMail
		{
			get
			{
				_sendMail = _sendMail ?? new SendMailService(this.UnitOfWork);
				return _sendMail;
			}
		}
		private IUrlSettingService _urlSetting;
		public IUrlSettingService UrlSetting
		{
			get
			{
				_urlSetting = _urlSetting ?? new UrlSettingService(this.UnitOfWork);
				return _urlSetting;
			}
		}
		private IUserRoleRelationService _userRoleRelation;
		public IUserRoleRelationService UserRoleRelation
		{
			get
			{
				_userRoleRelation = _userRoleRelation ?? new UserRoleRelationService(this.UnitOfWork);
				return _userRoleRelation;
			}
		}
		private IRolesActionDetailService _rolesActionDetail;
		public IRolesActionDetailService RolesActionDetail
		{
			get
			{
				_rolesActionDetail = _rolesActionDetail ?? new RolesActionDetailService(this.UnitOfWork);
				return _rolesActionDetail;
			}
		}
		private IMailSettingService _mailSetting;
		public IMailSettingService MailSetting
		{
			get
			{
				_mailSetting = _mailSetting ?? new MailSettingService(this.UnitOfWork);
				return _mailSetting;
			}
		}

		private IMSubBrandService _subBrand;
		public IMSubBrandService SubBrand
		{
			get
			{
				_subBrand = _subBrand ?? new MSubBrandService(this.UnitOfWork);
				return _subBrand;
			}
		}
		private IMBankService _mBank;
		public IMBankService MBank
		{
			get
			{
				_mBank = _mBank ?? new MBankService(this.UnitOfWork);
				return _mBank;
			}
		}
	}
}
