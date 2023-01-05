using System.Reflection;
using MasterChef.Infra.Postgres.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MasterChef.Infra.Postgres
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPostgresDependency(this IServiceCollection services,
            DatabaseConfiguration configuration)
        {
            services.AddDbContext<Infra.Context.DatabaseContext, PostgresContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                //options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
                //options.UseLoggerFactory();

                options.UseNpgsql(configuration.ConnectionString, options =>
                {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                });
            });

            services.BuildServiceProvider().MigrateDatabase();
            
            return services;
        }
    }
}