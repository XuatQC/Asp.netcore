using FOFB.Server.Services;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Text;

namespace FOFB.Server
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddConfiguration<SettingsConfig>(this.Configuration, "MySettings");
			services.AddSpaStaticFiles(configuration: options => { options.RootPath = "wwwroot/dist"; }); // add client built folder
			services.AddDistributedMemoryCache();
			services.AddSession(options => {
				options.IdleTimeout = TimeSpan.FromMinutes(60); // Can change able
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});
			services.AddControllers();
			services.AddCors(policy =>
			{
				policy.AddDefaultPolicy(opt => opt
					.AllowAnyOrigin()
					.SetIsOriginAllowed(origin => true)
					.AllowAnyHeader()
					.AllowAnyMethod()
					);
			});
			services.AddInfrastructure();
			services.AddDBService();

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			}).AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = Configuration["MySettings:Jwt:Issuer"],
					ValidAudience = Configuration["MySettings:Jwt:Issuer"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["MySettings:Jwt:Key"]))
				};
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseSpaStaticFiles();
			app.UseSession();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseStaticFiles(new StaticFileOptions()
			{
				FileProvider = new PhysicalFileProvider(
				Path.Combine(Directory.GetCurrentDirectory(), @"Images")),
				RequestPath = new PathString("/Images")
			});
			app.UseSpa(configuration: builder =>
			{
				if (env.IsDevelopment())
				{
					// create proxy to run with 1 url
					builder.UseProxyToSpaDevelopmentServer(baseUri: "http://localhost:8080");
				}
			});
		}
	}
}
