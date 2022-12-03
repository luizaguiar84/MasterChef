using Microsoft.Extensions.DependencyInjection;

namespace MasterChef.Infra
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddInfraDependency(this IServiceCollection services)
		{
			// Registro dos repositórios
			// services.AddTransient<ILeadRepository, LeadRepository>();
			
			return services;
		}
    }
}
