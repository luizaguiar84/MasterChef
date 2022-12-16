using MasterChef.Application.Interfaces;
using MasterChef.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MasterChef.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IRecipeService, RecipeService>();
            
            services.AddTransient<ITokenService, TokenService>();
            
            services.AddTransient<IUserService, UserService>();
            
            return services;
        }

    }
}