namespace API.ServiceCollectionExtensions;

using System.Security.Claims;
using Core.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

public static partial class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddJWTAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddJWTAuthorization();

        return builder;
    }

    public static IServiceCollection AddJWTAuthorization(this IServiceCollection services)
    {
        var secretKey = StringUtils.GenerateSecretByte();

        services.AddAuthentication(config =>
        {
            config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(config =>
        {
            config.RequireHttpsMetadata = false;
            config.SaveToken = true;
            config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("SystemOwner", policy => policy.RequireAuthenticatedUser().RequireClaim("Email", new[] { "vn@saishunkansys.com", "hungdh@saishunkansys.com" }));
            options.AddPolicy("TenantOwner", policy => policy.RequireAuthenticatedUser().RequireClaim("RoleKbn", new[] { "01" }));
            options.AddPolicy("Owner", policy => policy.RequireAuthenticatedUser()
            .RequireAssertion((context) =>
            {
                return context.User.HasClaim((claim) => (claim.Type == "Email"
                            && claim.Value == "vn@saishunkansys.com") || (claim.Type == "RoleKbn" && claim.Value == "01"));
            }));
        });

        return services;
    }
}