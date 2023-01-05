using System.Reflection;
using MasterChef.Infra.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MasterChef.Infra.SqlServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqLServerDependency(this IServiceCollection services, DatabaseConfiguration configuration)
        {
            services.AddDbContext<MasterChef.Infra.Context.DatabaseContext, SqlServerContext>(options =>
            {
                options.EnableSensitiveDataLogging();

                options.UseSqlServer(configuration.ConnectionString, builder =>
                {
                    builder.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                });

            });

            services.BuildServiceProvider().MigrateDatabase();

            return services;
        }

    }
}
