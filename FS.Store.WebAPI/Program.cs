using FS.Store.WebAPI.Configs;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

services.ConfigureScopes();

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

app.UseAuthorization();

app.UseStaticFiles();

app.MapStaticAssets();
app.AddMapControllerRouteConfiguration();


app.Run();
