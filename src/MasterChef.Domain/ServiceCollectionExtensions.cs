using Microsoft.Extensions.DependencyInjection;
using MasterChef.Domain.Interface;
using MasterChef.Domain.Service;

namespace MasterChef.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainDependency(this IServiceCollection services)
        {
            // Registro dos repositórios

            services.AddScoped<IEventService, EventService>();

            return services;
        }
    }
}
