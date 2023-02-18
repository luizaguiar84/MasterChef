using MasterChef.Infra.Helpers.ExtensionMethods;
using Serilog;
using System.Reflection;
using MasterChef.Infra;
using MasterChef.Infra.Enums;
using MasterChef.Infra.IoC;
using MasterChef.Infra.Sqlite.Identity;
using MasterChef.Infra.SqlServer.Identity;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

var databaseConfiguration = new DatabaseConfiguration(builder.Configuration, builder.Environment.IsProduction());

switch (databaseConfiguration.DatabaseType)
{
    case DatabaseType.Sqlite:
        builder.Services.AddSqLiteIdentityDependency(databaseConfiguration);
        break;
    case DatabaseType.SqlServer:
        builder.Services.AddSqlServerIdentityDependency(databaseConfiguration);
        break;
}

builder.Configuration.AddSerilogApi();
builder.Host.UseSerilog(Log.Logger);

Console.Title = Assembly.GetEntryAssembly().GetName().Name;

builder.Services.AddUIServiceIoCDependency();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseResponseCompression();

app.UseStaticFiles(new StaticFileOptions()
{
    OnPrepareResponse = ctx =>
    {
        int duration = 60 * 60 * 24 * 365;
        ctx.Context.Response.Headers[HeaderNames.CacheControl] = $"public, max-age={duration}";
    }
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();