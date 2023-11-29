using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using Core.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Core.Helpers;
public partial class StringUtils
{
    internal static string SecretKey = "6ceccd7405ef4b00b2630009be568cfa";
    public static byte[] GenerateSecretByte() =>
        Encoding.UTF8.GetBytes(SecretKey);
    public static string GenerateJWTToken(Account account)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = GenerateSecretByte();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Code", account.Code),
                new Claim("Name", account.Name),
                new Claim("RoleKbn", account.RoleKbn),
                new Claim("Email", account.Email),
                new Claim("TenantCode", account.TenantCode),
            }),
            Expires = DateTime.UtcNow.AddMinutes(120),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public static JwtSecurityToken? VerifyJWTToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = GenerateSecretByte();
            var hmacsha256 = new HMACSHA256(key);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(hmacsha256.Key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero,
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            return jwtToken;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static string PasswordHash(string rawPassword)
    {
        string hashPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(rawPassword, HashType.SHA512);
        return hashPassword;
    }

    public static bool VerifyPasswordHash(string rawPassword, string hashPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(rawPassword, hashPassword, HashType.SHA512);
    }
}