using FluentValidation;
using MasterChef.Application.Interfaces;
using MasterChef.Application.Services;
using MasterChef.Application.Validations;
using MasterChef.Domain.Entities;
using MasterChef.Dto.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace MasterChef.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IRecipeAppService, RecipeAppService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IUserAppService, UserAppAppService>();
            services.AddTransient<IIngredientAppService, IngredientAppService>();
            services.AddTransient<IValidator<RecipeDto>, RecipeValidations>();


            return services;
        }

    }
}