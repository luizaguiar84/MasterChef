using System;
using MasterChef.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace MasterChef.Infra.IoC
{
    public static class ServiceCollectionIoC
    {
        public static IServiceCollection AddUIServiceIoCDependency(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });


            services.AddResponseCompression(o =>
            {
                o.Providers.Add<GzipCompressionProvider>();
            });
            
            services.AddControllersWithViews();
            services.AddClientDependency();
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
        
        public static IServiceCollection AddApiServiceIoCDependency(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddInfraDependency();
            services.AddDomainDependency();

            services.AddCors(x =>
            {
                x.AddPolicy("Default", b =>
                {
                    b.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            
            return services;
        }
    }
}
