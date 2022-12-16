using MasterChef.Infra.Interfaces;
using MasterChef.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MasterChef.Infra
{
    public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddInfraDependency(this IServiceCollection services)
		{
			// Registro dos repositórios
			 services.AddTransient<IRecipeRepository, RecipeRepository>();
			 
			services.AddTransient<IUserRepository, UserRepository>();
			
			return services;
		}
    }
}
