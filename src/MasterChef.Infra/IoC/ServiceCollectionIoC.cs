﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace MasterChef.Infra.IoC
{
    public static class ServiceCollectionIoC
    {
        public static IServiceCollection AddServiceIoCDependency(this IServiceCollection services)
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
            return services;
        }
    }
}
