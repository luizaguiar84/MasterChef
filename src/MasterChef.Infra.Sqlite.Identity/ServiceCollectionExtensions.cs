using System.Reflection;
using MasterChef.Infra.Sqlite.Identity.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace MasterChef.Infra.Sqlite.Identity;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSqLiteIdentityDependency(this IServiceCollection services, DatabaseConfiguration configuration)
    {
        services.AddDbContext<SqliteIdentityContext>(options =>
        {
            options.EnableSensitiveDataLogging();
            options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryPossibleUnintendedUseOfEqualsWarning));

            options.UseSqlite(configuration.ConnectionString, options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
				
        });

        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<SqliteIdentityContext>();
        
        services.BuildServiceProvider().MigrateIdentityDatabase();
			
        return services;
    }
    
    private static void MigrateIdentityDatabase(this IServiceProvider provider)
    {
        Task.Factory.StartNew(() =>
        {
            using var scope = provider.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<SqliteIdentityContext>();
            context.Database.Migrate();
        });
    }
}