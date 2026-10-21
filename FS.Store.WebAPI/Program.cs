using FS.Store.DAL.Context;
using FS.Store.Model.Utils;
using FS.Store.WebAPI.Configs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

services.AddDbContext<AppDbContext>(options =>
{
    options
        .UseLazyLoadingProxies()
        .UseMySQL(
            EnvironmentUtil.Configuration
                .GetConnectionString("DefaultConnection")
        );
});

services.AddJwt(builder.Configuration);

services.DependencyInjections();

services.AddHttpContextAccessor();

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

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapStaticAssets();
app.AddMapControllerRouteConfiguration();


app.Run();
