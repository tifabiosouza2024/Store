using FS.Store.Core.Helper;
using FS.Store.Core.Interfaces;
using FS.Store.Core.Service;
using FS.Store.DAL.Interfaces;
using FS.Store.DAL.Repository;
using FS.Store.Model.Entity;
using FS.Store.WebAPI.Helpers;
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

        public static IServiceCollection DependencyInjections(this IServiceCollection services)
        {
            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
            Services(services);
            Helpers(services);
            Repositories(services);
            return services;
        }

        private static void Services(IServiceCollection services)
        {
            services.AddTransient(typeof(IUserService), typeof(UserService));
        }
        private static void Repositories(IServiceCollection services)
        {
            services.AddTransient(typeof(IUserRepository), typeof(UserRepository));
        }

        private static void Helpers(IServiceCollection services)
        {
            services.AddTransient<PasswordHelper>();
            services.AddTransient<AuthHelper>();
            services.AddTransient<CookieHelper>();
        }
    }
}
