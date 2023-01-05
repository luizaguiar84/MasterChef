using Microsoft.EntityFrameworkCore;

namespace MasterChef.UI.Data;

public static class MigrationExtension
{
    public static void MigrateIdentityDatabase(this IServiceProvider provider)
    {
        Task.Factory.StartNew(() =>
        {
            using var scope = provider.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<Data.ApplicationDbContext>();
            context.Database.Migrate();
        });
    }
}