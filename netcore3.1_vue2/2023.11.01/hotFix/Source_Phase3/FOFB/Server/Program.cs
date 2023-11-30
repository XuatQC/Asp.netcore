using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace FOFB.Server
{
	public class Program
	{
		public static void Main(string[] args)
		{
			long fileLogSize = 2097152; // 2MB

			Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Debug()
			.WriteTo.File(path: "logs/Debug/Log_.txt",
					restrictedToMinimumLevel: LogEventLevel.Debug,
					rollingInterval: RollingInterval.Day,
					rollOnFileSizeLimit: true,
					fileSizeLimitBytes: fileLogSize)
			.WriteTo.File(path: "logs/Info/Log_.txt",
					restrictedToMinimumLevel: LogEventLevel.Information,
					rollingInterval: RollingInterval.Day,
					rollOnFileSizeLimit: true,
					fileSizeLimitBytes: fileLogSize)
			.WriteTo.File(path: "logs/Error/Log_.txt",
					restrictedToMinimumLevel: LogEventLevel.Error,
					rollingInterval: RollingInterval.Day,
					rollOnFileSizeLimit: true,
					fileSizeLimitBytes: fileLogSize)
			.CreateLogger();
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
