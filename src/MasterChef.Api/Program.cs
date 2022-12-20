using System.Reflection;
using MasterChef.Api.Controllers.Config;
using MasterChef.Application;
using MasterChef.Infra;
using MasterChef.Infra.Enums;
using MasterChef.Infra.Postgres;
using MasterChef.Infra.Sqlite;
using MasterChef.Infra.SqlServer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using MasterChef.Infra.Helpers.ExtensionMethods;
using MasterChef.Infra.IoC;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApiServiceIoCDependency();
builder.Services.AddServices();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "MasterChef Api",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Digite 'Bearer' [space] e o token.
                      \r\n\r\Exemplo: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddAuthentication(
        x =>
        {
            x.DefaultAuthenticateScheme = "Jwt";
            x.DefaultChallengeScheme = "Jwt";
        })
    .AddJwtBearer("Jwt",
        o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidAudience = "clients-api",
                ValidIssuer = "api",
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Security.GetKey()),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(5)
            };
        }
    );

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
    case DatabaseType.Postgres:
        builder.Services.AddPostgresDependency(databaseConfiguration);
        break;
    case DatabaseType.SqlServer:
        builder.Services.AddSqLServerDependency(databaseConfiguration);
        break;
    default:
        throw new NotSupportedException("No database configuration found");
}

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

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
