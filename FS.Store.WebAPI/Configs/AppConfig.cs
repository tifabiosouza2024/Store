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
    }
}
