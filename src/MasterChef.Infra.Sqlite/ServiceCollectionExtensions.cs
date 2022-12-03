using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MasterChef.Infra;
using MasterChef.Infra.Context;
using Paghiper.Infra.Sqlite.Context;

namespace Paghiper.Infra.Sqlite
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddSqLiteDependency(this IServiceCollection services, DatabaseConfiguration configuration)
		{
			services.AddDbContext<CrudDbContext, SqliteCrudDbContext>(options =>
			{
				options.EnableSensitiveDataLogging();
				//options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
				//options.UseLoggerFactory();

				options.UseSqlite(configuration.ConnectionString, options =>
				{
					options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
				});
				
			});

			
			return services;
		}
	}
}
