using Utility;

namespace FOFB.Server.Services.Interfaces
{
	public interface IBaseService
	{
		public Common Common { get; }
		public bool IsPaymentAble(string bussinessCd);
		public bool IsAdminPaymentAble(string bussinessCd);
		//public bool IsDeliveryAble(string bussinessCd);
	}
}

