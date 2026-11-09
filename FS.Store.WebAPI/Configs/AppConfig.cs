using FS.Store.Core.Helper;
using FS.Store.Core.Interfaces;
using FS.Store.Core.Service;
using FS.Store.DAL.Context;
using FS.Store.DAL.Interfaces;
using FS.Store.DAL.Repository;
using FS.Store.Model.Entity;
using FS.Store.WebAPI.Helpers;
using FS.Store.WebAPI.Services;
using FS.Store.WebAPI.Services.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

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

                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
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
            services.AddTransient<ICashClosingService, CashClosingService>();
            services.AddTransient<ICashMovementService, CashMovementService>();
            services.AddTransient<ICashRegisterSessionService, CashRegisterSessionService>();
            services.AddTransient<ICurrentCashSessionService, CurrentCashSessionService>();
            services.AddTransient<IOperationService, OperationService>();
            services.AddTransient<IOperationUserRoleService, OperationUserRoleService>();
            services.AddTransient<IPaymentProviderService, PaymentProviderService>();
            services.AddTransient<IPaymentTerminalService, PaymentTerminalService>();
            services.AddTransient<IUserService, UserService>();
        }
        private static void Repositories(IServiceCollection services)
        {
            services.AddTransient<ICashClosingRepository, CashClosingRepository>();
            services.AddTransient<ICashMovementRepository, CashMovementRepository>();
            services.AddTransient<ICashRegisterSessionRepository, CashRegisterSessionRepository>();
            services.AddTransient<IOperationRepository, OperationRepository>();
            services.AddTransient<IOperationUserRoleRepository, OperationUserRoleRepository>();
            services.AddTransient<IPaymentProviderRepository, PaymentProviderRepository>();
            services.AddTransient<IPaymentTerminalRepository, PaymentTerminalRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }

        private static void Helpers(IServiceCollection services)
        {
            services.AddScoped<AuthHelper>();
            services.AddScoped<CashSessionHelper>();
            services.AddScoped<CurrentUserHelper>();
            services.AddScoped<PasswordHelper>();
        }

        public static void ApplyMigrationDatabase(this IServiceCollection services, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }
        }

        public static void ConfigureLocalization(this IServiceCollection services)
        {
            var culture = new CultureInfo("pt-BR");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(culture);
                options.SupportedCultures = [culture];
                options.SupportedUICultures = [culture];
            });
        }

        public static void ConfigureLocalization(this WebApplication app)
        {
            var localizationOptions = app.Services
                .GetRequiredService<IOptions<RequestLocalizationOptions>>()
                .Value;

            app.UseRequestLocalization(localizationOptions);
        }
    }
}
