using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using Serilog;

namespace MasterChef.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.Title = "PagHiper";

			CreateHostBuilder(args).Build().Run();

			Log.CloseAndFlush();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder
					.UseKestrel()
					.UseIISIntegration()
					.UseStartup<Startup>();
				})
				.UseSerilog()
				.ConfigureLogging((hostingContext, logging) => {
					Log.Logger = new LoggerConfiguration()
						.ReadFrom.Configuration(hostingContext.Configuration)
						.CreateLogger();

				});
	}
}
