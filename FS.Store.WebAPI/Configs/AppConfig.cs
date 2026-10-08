using FS.Store.DAL.Helper;
using FS.Store.Model.Entity;
using Microsoft.AspNetCore.Identity;

namespace FS.Store.WebAPI.Configs
{
    public static class AppConfig
    {
        public static WebApplication AddMapControllerRouteConfiguration(this WebApplication app)
        {
            app.MapControllerRoute(
                name: "Catalog",
                pattern: "{area:exists}/{controller=Public}/{action=Login}/{id?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            return app;
        }

        public static IServiceCollection ConfigureScopes(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<PasswordHelper>();
            return services;
        }
    }
}
