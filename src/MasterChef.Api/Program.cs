using MasterChef.Application;
using MasterChef.Infra;
using MasterChef.Infra.Enums;
using MasterChef.Infra.Postgres;
using MasterChef.Infra.Sqlite;
using MasterChef.Domain;
using MasterChef.Infra.SqlServer;
using MasterChef.Infra.Helpers;
using Serilog;

const string applicationName = "MasterChef.Api";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.AddInfraDependency();
builder.Services.AddDomainDependency();

var databaseConfiguration = new DatabaseConfiguration(builder.Configuration, builder.Environment.IsProduction());

if (databaseConfiguration.DatabaseType == DatabaseType.Sqlite)
	builder.Services.AddSqLiteDependency(databaseConfiguration);

else if (databaseConfiguration.DatabaseType == DatabaseType.Postgres)
	builder.Services.AddPostgresDependency(databaseConfiguration);

else if (databaseConfiguration.DatabaseType == DatabaseType.SqlServer)
	builder.Services.AddSqLServerDependency(databaseConfiguration);

else
	throw new NotSupportedException("No database configuration found");

SerilogExtension.AddSerilogApi(builder.Configuration, applicationName);
builder.Host.UseSerilog(Log.Logger);

Console.Title = applicationName;

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
