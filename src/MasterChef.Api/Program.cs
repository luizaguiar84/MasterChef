using MasterChef.Application;
using MasterChef.Infra;
using MasterChef.Infra.Enums;
using MasterChef.Infra.MySql;
using MasterChef.Infra.Postgres;
using MasterChef.Infra.Sqlite;
using MasterChef.Domain;
using MasterChef.Infra.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.AddInfraDependency();
builder.Services.AddDomainDependency();

var DatabaseConfiguration = new DatabaseConfiguration(builder.Configuration, builder.Environment.IsProduction());

if (DatabaseConfiguration.DatabaseType == DatabaseType.MySQL)
	builder.Services.AddMySqlDependency(DatabaseConfiguration);

else if (DatabaseConfiguration.DatabaseType == DatabaseType.Sqlite)
	builder.Services.AddSqLiteDependency(DatabaseConfiguration);

else if (DatabaseConfiguration.DatabaseType == DatabaseType.Postgres)
	builder.Services.AddPostgresDependency(DatabaseConfiguration);

else if (DatabaseConfiguration.DatabaseType == DatabaseType.SqlServer)
	builder.Services.AddSqLServerDependency(DatabaseConfiguration);

else
	throw new NotSupportedException("No database configuration found");

//builder.Services.BuildServiceProvider().MigrateDatabase();

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
