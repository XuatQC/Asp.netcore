
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System.Data;

namespace Infrastructure.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{

		private IDbConnection _connection;
		public bool IsDisposed { get; private set; } = false;


		public SettingsConfig Config { get; }

		public UnitOfWork(SettingsConfig config, IDbConnection connection)
		{
			_connection = connection;
			Config = config;
		}

		//################# Repository#####################
		private IDatetimeSettingRepository _datetimeSetting;
		public IDatetimeSettingRepository DatetimeSetting
		{
			get
			{
				_datetimeSetting = _datetimeSetting ?? new DatetimeSettingRepository(Config, _connection);
				return _datetimeSetting;
			}
		}
		private IFrontScreenRepository _frontScreen;
		public IFrontScreenRepository FrontScreen
		{
			get
			{
				_frontScreen = _frontScreen ?? new FrontScreenRepository(Config, _connection);
				return _frontScreen;
			}
		}
		private IFrontScreenImgUrlRepository _frontScreenImgUrl;
		public IFrontScreenImgUrlRepository FrontScreenImgUrl
		{
			get
			{
				_frontScreenImgUrl = _frontScreenImgUrl ?? new FrontScreenImgUrlRepository(Config, _connection);
				return _frontScreenImgUrl;
			}
		}
		private IInventorySymbolRepository _inventorySymbol;
		public IInventorySymbolRepository InventorySymbol
		{
			get
			{
				_inventorySymbol = _inventorySymbol ?? new InventorySymbolRepository(Config, _connection);
				return _inventorySymbol;
			}
		}

		private IMAddressRepository _mAddress;
		public IMAddressRepository MAddress
		{
			get
			{
				_mAddress = _mAddress ?? new MAddressRepository(Config, _connection);
				return _mAddress;
			}
		}

		private IMAdminUserRepository _mAdminUser;
		public IMAdminUserRepository MAdminUser
		{
			get
			{
				_mAdminUser = _mAdminUser ?? new MAdminUserRepository(Config, _connection);
				return _mAdminUser;
			}
		}
		private IMailSettingRepository _mailSetting;
		public IMailSettingRepository MailSetting
		{
			get
			{
				_mailSetting = _mailSetting ?? new MailSettingRepository(Config, _connection);
				return _mailSetting;
			}
		}

		private IMailTemplateRepository _mailTemplate;
		public IMailTemplateRepository MailTemplate
		{
			get
			{
				_mailTemplate = _mailTemplate ?? new MailTemplateRepository(Config, _connection);
				return _mailTemplate;
			}
		}

		private IMBrandRepository _mBrand;
		public IMBrandRepository MBrand
		{
			get
			{
				_mBrand = _mBrand ?? new MBrandRepository(Config, _connection);
				return _mBrand;
			}
		}
		private IMBrandRelationRepository _mBrandRelation;
		public IMBrandRelationRepository MBrandRelation
		{
			get
			{
				_mBrandRelation = _mBrandRelation ?? new MBrandRelationRepository(Config, _connection);
				return _mBrandRelation;
			}
		}
		private IMBussinessRepository _mBussiness;
		public IMBussinessRepository MBussiness
		{
			get
			{
				_mBussiness = _mBussiness ?? new MBussinessRepository(Config, _connection);
				return _mBussiness;
			}
		}
		private IMColorRepository _mColor;
		public IMColorRepository MColor
		{
			get
			{
				_mColor = _mColor ?? new MColorRepository(Config, _connection);
				return _mColor;
			}
		}

		private IMCustRepository _mCust;
		public IMCustRepository MCust
		{
			get
			{
				_mCust = _mCust ?? new MCustRepository(Config, _connection);
				return _mCust;
			}
		}

		private IMCustRecieveRepository _mCustRecieve;
		public IMCustRecieveRepository MCustRecieve
		{
			get
			{
				_mCustRecieve = _mCustRecieve ?? new MCustRecieveRepository(Config, _connection);
				return _mCustRecieve;
			}
		}
		private IMenuRepository _menu;
		public IMenuRepository Menu
		{
			get
			{
				_menu = _menu ?? new MenuRepository(Config, _connection);
				return _menu;
			}
		}

		private IMProductDetailRepository _mProductDetail;
		public IMProductDetailRepository MProductDetail
		{
			get
			{
				_mProductDetail = _mProductDetail ?? new MProductDetailRepository(Config, _connection);
				return _mProductDetail;
			}
		}
		private IMkbnValueRepository _mkbnValue;
		public IMkbnValueRepository MkbnValue
		{
			get
			{
				_mkbnValue = _mkbnValue ?? new MkbnValueRepository(Config, _connection);
				return _mkbnValue;
			}
		}
		private IMProductRepository _mproduct;
		public IMProductRepository MProduct
		{
			get
			{
				_mproduct = _mproduct ?? new MProductRepository(Config, _connection);
				return _mproduct;
			}
		}

		private IMProductImgUrlRepository _mProductImgUrl;
		public IMProductImgUrlRepository MProductImgUrl
		{
			get
			{
				_mProductImgUrl = _mProductImgUrl ?? new MProductImgUrlRepository(Config, _connection);
				return _mProductImgUrl;
			}
		}
		private IMShopRepository _mShop;
		public IMShopRepository MShop
		{
			get
			{
				_mShop = _mShop ?? new MShopRepository(Config, _connection);
				return _mShop;
			}
		}

		private IMSizeRepository _mSize;
		public IMSizeRepository MSize
		{
			get
			{
				_mSize = _mSize ?? new MSizeRepository(Config, _connection);
				return _mSize;
			}
		}
		private IMUserLoginRepository _mUserLogin;
		public IMUserLoginRepository MUserLogin
		{
			get
			{
				_mUserLogin = _mUserLogin ?? new MUserLoginRepository(Config, _connection);
				return _mUserLogin;
			}
		}

		private IOrdersRepository _orders;
		public IOrdersRepository Orders
		{
			get
			{
				_orders = _orders ?? new OrdersRepository(Config, _connection);
				return _orders;
			}
		}

		private IOrdHistoryRepository _ordHistory;
		public IOrdHistoryRepository OrdHistory
		{
			get
			{
				_ordHistory = _ordHistory ?? new OrdHistoryRepository(Config, _connection);
				return _ordHistory;
			}
		}

		private IRolesRepository _roles;
		public IRolesRepository Roles
		{
			get
			{
				_roles = _roles ?? new RolesRepository(Config, _connection);
				return _roles;
			}
		}
		private IRolesDetailRepository _rolesDetail;
		public IRolesDetailRepository RolesDetail
		{
			get
			{
				_rolesDetail = _rolesDetail ?? new RolesDetailRepository(Config, _connection);
				return _rolesDetail;
			}
		}
		private ISendMailRepository _sendMail;
		public ISendMailRepository SendMail
		{
			get
			{
				_sendMail = _sendMail ?? new SendMailRepository(Config, _connection);
				return _sendMail;
			}
		}
		private IUrlsettingRepository _urlSetting;
		public IUrlsettingRepository UrlSetting
		{
			get
			{
				_urlSetting = _urlSetting ?? new UrlsettingRepository(Config, _connection);
				return _urlSetting;
			}
		}
		private IUserRoleRelationRepository _userRoleRelation;
		public IUserRoleRelationRepository UserRoleRelation
		{
			get
			{
				_userRoleRelation = _userRoleRelation ?? new UserRoleRelationRepository(Config, _connection);
				return _userRoleRelation;
			}
		}

		private IOrderDetailRepository _orderDetail;
		public IOrderDetailRepository OrderDetail
		{
			get
			{
				_orderDetail = _orderDetail ?? new OrderDetailRepository(Config, _connection);
				return _orderDetail;
			}
		}
		private IMPostCodeRepository _mPostCode;
		public IMPostCodeRepository MPostCode
		{
			get
			{
				_mPostCode = _mPostCode ?? new MPostCodeRepository(Config, _connection);
				return _mPostCode;
			}
		}
		private IRolesActionDetailRepository _rolesActionDetail;
		public IRolesActionDetailRepository RolesActionDetail
		{
			get
			{
				_rolesActionDetail = _rolesActionDetail ?? new RolesActionDetailRepository(Config, _connection);
				return _rolesActionDetail;
			}
		}
		private IMSubBrandRepository _mSubBrand;
		public IMSubBrandRepository MSubBrand
		{
			get
			{
				_mSubBrand = _mSubBrand ?? new MSubBrandRepository(Config, _connection);
				return _mSubBrand;
			}
		}
		private IMBankRepository _mBank;
		public IMBankRepository MBank
		{
			get
			{
				_mBank = _mBank ?? new MBankRepository(Config, _connection);
				return _mBank;
			}
		}
		private IMenuRoleRelationRepository _menuRoleRelation;
		public IMenuRoleRelationRepository MenuRoleRelation
		{
			get
			{
				_menuRoleRelation = _menuRoleRelation ?? new MenuRoleRelationRepository(Config, _connection);
				return _menuRoleRelation;
			}
		}
	}
}
