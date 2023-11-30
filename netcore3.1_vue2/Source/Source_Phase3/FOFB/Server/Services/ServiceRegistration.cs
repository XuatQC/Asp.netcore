using FOFB.Server.Services.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace FOFB.Server.Services
{
	public static class ServiceRegistration
	{
		public static void AddConfiguration<T>(this IServiceCollection services,
												IConfiguration configuration,
												string configurationTag = null)
												where T : class
		{
			if (string.IsNullOrEmpty(configurationTag))
			{
				configurationTag = typeof(T).Name;
			}

			var instance = Activator.CreateInstance<T>();
			new ConfigureFromConfigurationOptions<T>(configuration.GetSection(configurationTag)).Configure(instance);
			services.AddSingleton(instance);
		}

		public static void AddDBService(this IServiceCollection services)
		{
			var provider = services.BuildServiceProvider();
			var dbRespository = provider.GetRequiredService<IDbRespository>();

			// Inject IDbConnection, with implementation from SqlConnection class.
			services.AddTransient((sp) => dbRespository.CreateConn());
		}

		public static void AddInfrastructure(this IServiceCollection services)
		{
			services.AddTransient<IService, Service>();
			// Repository
			services.AddTransient<IDbRespository, DbRespository>();
			services.AddTransient<IUnitOfWork, UnitOfWork>();
		}
	}
}
