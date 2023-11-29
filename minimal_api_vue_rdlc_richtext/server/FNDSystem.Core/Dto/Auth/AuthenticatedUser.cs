namespace FNDSystem.Core.Dto
{
    public class AuthenticatedUser
    {
        public required string Email { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string TenantCode { get; set; }
        public required string RoleKbn { get; set; }
    }
}