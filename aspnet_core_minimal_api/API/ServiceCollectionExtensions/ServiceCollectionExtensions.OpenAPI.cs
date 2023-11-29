namespace API.ServiceCollectionExtensions;
using Microsoft.OpenApi.Models;

public static partial class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwagger();

        return builder;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo()
            {
                Description = "Minimal API Demo",
                Title = "Minimal API Demo",
                Version = "v1",
                Contact = new OpenApiContact()
                {
                    Name = "Do Huy Hung",
                    Email = "hungdh@saishunkansys.com",
                    Url = new Uri("https://vn.saishunkansys.com")
                },
                License = new OpenApiLicense()
                {
                    Name = "MIT",
                    Url = new Uri("https://vn.saishunkansys.com")
                },
                TermsOfService = new Uri("https://vn.saishunkansys.com"),
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
            }});
        });

        return services;
    }
}