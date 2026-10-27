using FS.Store.WebAPI.Configs;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

services
    .AddDataProtection()
    .PersistKeysToFileSystem(
        new DirectoryInfo("/app/keys"))
    .SetApplicationName("FS.Store");

services.ConfigureDatabase(builder.Configuration);

services.ConfigureAuthentication();
services.AddAuthorization();

services.DependencyInjections();

services.AddHttpContextAccessor();

services.ApplyMigrationDatabase(builder.Environment);
services.ConfigureLocalization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    app.MapGet("/routes", (EndpointDataSource endpointDataSource) =>
    {
        return endpointDataSource.Endpoints
            .OfType<RouteEndpoint>()
            .Select(e => new
            {
                Route = e.RoutePattern.RawText,
                DisplayName = e.DisplayName
            })
            .OrderBy(x => x.Route);
    });
}

app.ConfigureLocalization();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.AddMapControllerRouteConfiguration();

app.Run();
