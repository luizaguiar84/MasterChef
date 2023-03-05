using Microsoft.Extensions.Configuration;
using System;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Filters;
using Serilog.Sinks.SystemConsole.Themes;
using System.Reflection;
using Elastic.CommonSchema.Serilog;
using Serilog.Sinks.Elasticsearch;

namespace MasterChef.Infra.Helpers.ExtensionMethods
{
    public static class SerilogExtension
    {
        public static void AddSerilogApi(this IConfiguration configuration)
        {

            var applicationName = Assembly.GetCallingAssembly().GetName().Name;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithCorrelationId()
                .Enrich.WithProperty(applicationName, $"API Serilog - {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}")
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                .Filter.ByExcluding(z => z.MessageTemplate.Text.Contains("Business error"))
                .WriteTo.Async(wt => wt.Console(
                    theme: AnsiConsoleTheme.Sixteen,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u4}] [{HttpContextId}] {SourceContext} {Message}{NewLine}{Exception}{NewLine}"
                    ))
                .WriteTo.Async(wt => wt.File(
                    path: "logs/log.txt",
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u4}] [{HttpContextId}] {SourceContext} {Message}{NewLine}{Exception}"
                    ))
                .WriteTo.Async(writeTo => writeTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticsearchSettings:uri"]))
                {
                    TypeName = null,
                    AutoRegisterTemplate = true,
                    IndexFormat = configuration["ElasticsearchSettings:defaultIndex"],
                    BatchAction = ElasticOpType.Create,
                    CustomFormatter = new EcsTextFormatter(),
                    ModifyConnectionSettings = x => x.BasicAuthentication(configuration["ElasticsearchSettings:username"], configuration["ElasticsearchSettings:password"])
                }))
                .CreateLogger();
        }

    }
}
