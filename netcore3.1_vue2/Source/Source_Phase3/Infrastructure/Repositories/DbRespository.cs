using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace Infrastructure.Repositories
{
	public class DbRespository : IDbRespository
	{
		public SettingsConfig Config { get; private set; }

		public DbRespository(SettingsConfig config)
		{
			this.Config = config;
		}

		public IDbConnection CreateConn()
		{
			var conn = new MySqlConnection(this.Config.ConnectionStringId);
			return conn;
		}
	}
}