using System.Reflection;
using Elastic.Apm.AspNetCore;
using MasterChef.Api.Controllers.Config;
using MasterChef.Application;
using MasterChef.Infra;
using MasterChef.Infra.Enums;
using MasterChef.Infra.Sqlite;
using MasterChef.Infra.SqlServer;
using Serilog;
using MasterChef.Infra.Helpers.ExtensionMethods;
using MasterChef.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices();

builder.Services.AddApiServiceIoCDependency();

builder.Services.AddControllers().ConfigureApiBehaviorOptions(o =>
{
    o.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ProduceErrorResponse;
});

var databaseConfiguration = new DatabaseConfiguration(builder.Configuration, builder.Environment.IsProduction());

switch (databaseConfiguration.DatabaseType)
{
    case DatabaseType.Sqlite:
        builder.Services.AddSqLiteDependency(databaseConfiguration);
        break;
    case DatabaseType.SqlServer:
        builder.Services.AddSqLServerDependency(databaseConfiguration);
        break;
    default:
        throw new NotSupportedException("No database configuration found");
}

builder.Configuration.AddSerilogApi();

if (Convert.ToBoolean(builder.Configuration["useElasticSearch"]))
    builder.Services.AddElasticsearch(builder.Configuration);

builder.Logging.ClearProviders();

builder.Host.UseSerilog(Log.Logger, true);

Console.Title = Assembly.GetEntryAssembly().GetName().Name;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s => { s.DisplayRequestDuration(); });
}

if (Convert.ToBoolean(builder.Configuration["useElasticSearch"]))
    app.UseElasticApm(builder.Configuration);

app.UseResponseCompression();

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();