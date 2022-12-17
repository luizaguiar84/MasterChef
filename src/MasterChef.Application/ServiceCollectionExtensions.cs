using FluentValidation;
using MasterChef.Application.Interfaces;
using MasterChef.Application.Services;
using MasterChef.Application.Validations;
using MasterChef.Domain.Entities;
using MasterChef.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MasterChef.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IRecipeAppService, RecipeAppAppService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IUserAppService, UserAppAppService>();
            services.AddTransient<IIngredientAppService, IngredientAppAppService>();
            services.AddTransient<IValidator<Recipe>, RecipeValidations>();


            return services;
        }

    }
}