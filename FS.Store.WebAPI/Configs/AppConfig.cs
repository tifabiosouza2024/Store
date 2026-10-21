using FS.Store.Core.Helper;
using FS.Store.Core.Interfaces;
using FS.Store.Core.Service;
using FS.Store.DAL.Context;
using FS.Store.DAL.Interfaces;
using FS.Store.DAL.Repository;
using FS.Store.Model.Entity;
using FS.Store.WebAPI.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FS.Store.WebAPI.Configs
{
    public static class AppConfig
    {
        public static WebApplication AddMapControllerRouteConfiguration(this WebApplication app)
        {
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Public}/{action=Login}/{id?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            return app;
        }

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options
                    .UseLazyLoadingProxies()
                    .UseMySQL(configuration.GetConnectionString("DefaultConnection")!);
            });
            return services;
        }
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
                {
                    options.LoginPath = "/Admin/Public/Login";
                    options.AccessDeniedPath = "/Admin/Public/AccessDenied";

                    options.Cookie.Name = "FS.Store.Auth";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.SameSite = SameSiteMode.Lax;

                    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                    options.SlidingExpiration = true;
                });
            return services;
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
            services.AddTransient<IUserService, UserService>();
        }
        private static void Repositories(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
        }

        private static void Helpers(IServiceCollection services)
        {
            services.AddTransient<PasswordHelper>();
            services.AddTransient<AuthHelper>();
        }
    }
}
