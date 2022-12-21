using System.Reflection;
using MasterChef.Infra.SqlServer.Identity.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace MasterChef.Infra.SqlServer.Identity;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSqlServerIdentityDependency(this IServiceCollection services, DatabaseConfiguration configuration)
    {
        services.AddDbContext<SqlServerIdentityContext>(options =>
        {
            options.EnableSensitiveDataLogging();
            options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryPossibleUnintendedUseOfEqualsWarning));
            
            options.UseSqlServer(configuration.ConnectionString, o =>
            {
                o.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
				
        });

        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<SqlServerIdentityContext>();
        
        services.BuildServiceProvider().MigrateIdentityDatabase();
			
        return services;
    }
    
    private static void MigrateIdentityDatabase(this IServiceProvider provider)
    {
        Task.Factory.StartNew(() =>
        {
            using var scope = provider.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<SqlServerIdentityContext>();
            context.Database.Migrate();
        });
    }
}