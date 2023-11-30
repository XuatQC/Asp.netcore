using System.Data;

namespace Infrastructure.Interfaces
{
	public interface IDbRespository
	{
		IDbConnection CreateConn();
	}
}
