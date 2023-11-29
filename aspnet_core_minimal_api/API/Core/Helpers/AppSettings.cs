namespace Core.Helpers.AppSettings;
public class PostgresDB
{
    public required string Master { get; set; }
    public required string Slave { get; set; }
}