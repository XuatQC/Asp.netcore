namespace FNDSystem.API.ServiceCollectionExtensions;

using Core.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

        services.AddAuthorization();

        return services;
    }
}