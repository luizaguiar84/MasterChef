using System.Reflection;
using MasterChef.Infra.Context;
using MasterChef.Infra.MySql.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MasterChef.Infra.MySql
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddMySqlDependency(this IServiceCollection services, DatabaseConfiguration configuration)
		{
			services.AddDbContext<Infra.Context.Context, MySqlContext>(options =>
			{
				options.EnableSensitiveDataLogging();
				//options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
				//options.UseLoggerFactory();

				options.UseMySQL(configuration.ConnectionString, options =>
				{
					options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
				});

			});

			
			return services;
		}
	}
}
