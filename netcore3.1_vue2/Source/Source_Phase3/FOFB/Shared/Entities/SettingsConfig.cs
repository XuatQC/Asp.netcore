
namespace FOFB.Shared.Entities
{
	public sealed class SettingsConfig
	{
		public bool Log { get; set; }
		public string Domain { get; set; }
		public string ConnectionStringId { get; set; }
		public string AdminAccessIP { get; set; }
		public Parameters Parameters { get; set; }
		public Jwt Jwt { get; set; }

	}

	public sealed class Parameters
	{
		public bool IsProduction { get; set; }
	}

	public sealed class Jwt
	{
		public string Key { get; set; }
		public string Issuer { get; set; }
	}
}
