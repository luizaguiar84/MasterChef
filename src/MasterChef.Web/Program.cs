using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using Serilog;
using Serilog.Events;

namespace MasterChef.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.Title = "MasterChef";

			CreateHostBuilder(args).Build().Run();

			Log.CloseAndFlush();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder
						.UseKestrel()
						.UseStartup<Startup>();
				})
				.UseSerilog()

		 .ConfigureLogging((hostingContext, logging) =>
				{
					Log.Logger = new LoggerConfiguration()
						.WriteTo.Console(
							outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u4}] [{HttpContextId}] {SourceContext} {Message}{NewLine}{Exception}"
							)
						.WriteTo.File(
							path: "logs/log.txt", 
							rollingInterval: RollingInterval.Day, 
							outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u4}] [{HttpContextId}] {SourceContext} {Message}{NewLine}{Exception}"
							)
						.CreateLogger();
				});
	}
}
