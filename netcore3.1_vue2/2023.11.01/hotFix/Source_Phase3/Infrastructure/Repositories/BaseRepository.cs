using FOFB.Shared.Entities;
using System.Data;

namespace Infrastructure.Repositories
{
	public class BaseRepository
	{
		public SettingsConfig Config { get; private set; }
		public IDbConnection DbConn { get; private set; }

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="db"></param>
		public BaseRepository(SettingsConfig config, IDbConnection dbConn)
		{
			this.Config = config;
			this.DbConn = dbConn;
		}
	}
}
