using FOFB.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IMUserLoginRepository : IGenericRepository<MUserLogin>
	{
		MuserLoginSub Login(string userCd, string password);

		MuserLoginSub GetMuserLoginSub(MuserLoginSub muserLoginSub);
		Task<long> AddListAsync(List<MUserLogin> userLogins);

	}
}