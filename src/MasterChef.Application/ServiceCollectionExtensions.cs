using MasterChef.Application.Directors;
using MasterChef.Application.Interfaces;
using MasterChef.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MasterChef.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            
            services.AddTransient<ILeadAppService, LeadAppAppService>();
            
            services.AddTransient<ILeadCreator, LeadCreator>();
            return services;
        }

    }
}