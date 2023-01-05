using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MasterChef.Infra.Sqlite.Context;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MasterChef.Infra.Sqlite
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddSqLiteDependency(this IServiceCollection services, DatabaseConfiguration configuration)
		{
			services.AddDbContext<MasterChef.Infra.Context.DatabaseContext, SqliteContext>(options =>
			{
				options.EnableSensitiveDataLogging();
				options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryPossibleUnintendedUseOfEqualsWarning));

				options.UseSqlite(configuration.ConnectionString, options =>
				{
					options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
				});
				
			});

            services.BuildServiceProvider().MigrateDatabase();
			
            return services;
		}
	}
}
