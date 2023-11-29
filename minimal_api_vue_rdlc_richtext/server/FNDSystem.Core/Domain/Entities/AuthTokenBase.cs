namespace FNDSystem.Core.Domain.Entities
{
    public class AuthTokenBase
    {
        public AuthTokenBase() { }

        public string SecretKey { get; set; } = string.Empty;
    }
}
