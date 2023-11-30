using FOFB.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services.Interfaces
{
	public interface IMUserLoginService : IGenericService<MUserLogin>
	{
		Task<Tuple<MuserLoginSub, string, List<MkbnValue>>> Login(MUserLogin muserLogin, bool isAutoLogin, string currNetworkIp);

		MuserLoginSub GetMuserLoginSub(MuserLoginSub muserLoginSub);

		Task<Tuple<bool, string>> ChangePassword(MUserLogin muserLogin, string oldPassword);
		int ResetPassword(string password, string updateUserCd);
	}
}
