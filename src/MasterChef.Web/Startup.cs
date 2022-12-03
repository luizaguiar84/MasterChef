using System;
using MasterChef.Infra;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MasterChef.Application;
using MasterChef.Infra.MySql;
using MasterChef.Infra.Postgres;
using Paghiper.Infra.Sqlite;

namespace MasterChef.Web
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public DatabaseConfiguration DatabaseConfiguration { get; }
		public IHostEnvironment Environment { get; }

		public Startup(IConfiguration configuration, IHostEnvironment environment)
		{
			Configuration = configuration;
			Environment = environment;
			DatabaseConfiguration = new DatabaseConfiguration(configuration, Environment.IsProduction());
		}


		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();

			services.AddServices();
			services.AddInfraDependency();

			if (DatabaseConfiguration.DatabaseType == DatabaseType.MySQL)
				services.AddMySqlDependency(DatabaseConfiguration);
			
			else if (DatabaseConfiguration.DatabaseType == DatabaseType.Sqlite)
				services.AddSqLiteDependency(DatabaseConfiguration);
			
			else if (DatabaseConfiguration.DatabaseType == DatabaseType.Postgres)
				services.AddPostgresDependency(DatabaseConfiguration);
			
			else
				throw new NotSupportedException("No database configuration found");
		}


		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.ApplicationServices.MigrateDatabase();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
