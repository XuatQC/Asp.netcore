using FOFB.Server.Services;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GetMailBounce
{
	public class WorkLoader
	{
		public IConfiguration Configuration { get; }

		//public WorkLoader(IConfiguration configuration)
		public WorkLoader()
		{
			//this.Configuration = configuration;
			var builder = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json");

			Configuration = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddConfiguration<SettingsConfig>(this.Configuration, "MySettings");
			services.AddInfrastructure();
			services.AddDBService();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
		}
	}
}
