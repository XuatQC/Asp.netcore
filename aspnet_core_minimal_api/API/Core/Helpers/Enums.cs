namespace Core.Helpers.Enum;

public static class RoleKbn
{
    public static readonly string Administrator = "99";
    public static readonly string TenantOwner = "01";
    public static readonly string NormalUser = "02";
    public static readonly List<string> Values = new List<string> { "99", "01", "02" };
}