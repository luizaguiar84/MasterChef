using System;
using System.Threading.Tasks;
using MasterChef.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MasterChef.Infra
{
	public static class MigrationExtensions
	{
		public static void MigrateDatabase(this IServiceProvider provider)
		{
			//Task.Factory.StartNew(() =>
			//{
			//	using var scope = provider.CreateScope();
			//	using var context = scope.ServiceProvider.GetRequiredService<Context.DatabaseContext>();
			//	context.Database.Migrate();
			//});
		}
	}
}