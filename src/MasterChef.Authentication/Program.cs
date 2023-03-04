using System.Reflection;
using MasterChef.Application.Interfaces;
using MasterChef.Application.Services;
using MasterChef.Infra;
using MasterChef.Infra.Enums;
using MasterChef.Infra.Helpers.ExtensionMethods;
using MasterChef.Infra.Interfaces;
using MasterChef.Infra.Repositories;
using MasterChef.Infra.Sqlite;
using MasterChef.Infra.SqlServer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IUserAppService, UserAppAppService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddSerilogApi();
builder.Host.UseSerilog(Log.Logger);

Console.Title = Assembly.GetEntryAssembly().GetName().Name;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();