using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace MasterChef.Infra.Helpers;

public static class ConfigurationHelpers
{
    public static IConfigurationRoot GetConfiguration()
    {
        var buider = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                optional: true);

        return buider.Build();
    }
}