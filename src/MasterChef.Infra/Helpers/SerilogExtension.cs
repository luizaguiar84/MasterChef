﻿using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.Hosting.Internal;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Filters;
using Serilog.Sinks.SystemConsole.Themes;

namespace MasterChef.Infra.Helpers
{
    public static class SerilogExtension
    {
        public static void AddSerilogApi(IConfiguration configuration, string applicationName)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithCorrelationId()
                .Enrich.WithProperty(applicationName, $"API Serilog - {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}")
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                .Filter.ByExcluding(z => z.MessageTemplate.Text.Contains("Business error"))
                .WriteTo.Async(wt => wt.Console(
                    theme: AnsiConsoleTheme.Code,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u4}] [{HttpContextId}] {SourceContext} {Message}{NewLine}{Exception}{NewLine}"
                    ))
                .WriteTo.Async(wt => wt.File(
                    path: "logs/log.txt",
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u4}] [{HttpContextId}] {SourceContext} {Message}{NewLine}{Exception}{NewLine}"
                    ))
                .CreateLogger();
        }

    }
}
