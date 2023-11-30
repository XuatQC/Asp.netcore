using FOFB.Server.Services;
using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;

namespace GetMailBounce
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Task get mail bounce is running...");

			Log.Logger.Debug("Start run task...");

			string logFilesPath = Directory.GetParent(AppContext.BaseDirectory).FullName;
			string logFile = logFilesPath + "/Log/LogMailBounce_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";

			try
			{
				// write Log
				Log.Logger = new LoggerConfiguration()
					.WriteTo.Console()
					.WriteTo.File(logFile)
					.CreateLogger();

				IServiceCollection services = new ServiceCollection();
				WorkLoader startup = new WorkLoader();
				startup.ConfigureServices(services);
				IServiceProvider serviceProvider = services.BuildServiceProvider();

				Log.Logger.Information("Call get mail bounce");

				//using IHost host = CreateHostBuilder(args).Build();
				//host.RunAsync();
				//var service = host.Services.GetService<IService>();

				var service = serviceProvider.GetService<IService>();

				GetMailBounceAWS getMailObj = new GetMailBounceAWS(service);
				getMailObj.BounceEmails();

			}
			catch (Exception ex)
			{
				Log.Logger.Error(ex, "Exception Caught");
				Console.WriteLine("Get Mail Bouce Error, please looking at log file: ");
				Console.WriteLine(logFile);
				Console.ReadKey();
			}
			Log.Logger.Information("-----------------------------------");
			Log.Logger.Debug("Finished run task...");
		}

		//public static IHostBuilder CreateHostBuilder(string[] args) =>
		//	Host.CreateDefaultBuilder(args)
		//		.ConfigureWebHostDefaults(webBuilder =>
		//		{
		//			webBuilder.UseStartup<WorkLoader>();
		//		});
	}
}
