using Microsoft.Extensions.Configuration;

namespace FS.Store.Model.Utils
{
    public class EnvironmentUtil
    {
        public static readonly IConfiguration Configuration = new ConfigurationBuilder().AddJsonFile(GetAppSettings(), optional: false, reloadOnChange: true).Build();

        private const string Development = "Development";
        private const string DevelopmentSetgins = "appsettings.Development.json";

        private const string Production = "Production";
        private const string ProductionSettings = "appsettings.json";

        public static bool IsDevelopment
            => string.Equals(Development, AspNetCoreEnvironment(), StringComparison.OrdinalIgnoreCase);

        public static bool IsProduction
            => string.Equals(Production, AspNetCoreEnvironment(), StringComparison.OrdinalIgnoreCase);
        public static string? AspNetCoreEnvironment()
            => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        public static string GetAppSettings()
        {
            if (IsProduction)
                return ProductionSettings;

            return DevelopmentSetgins;
        }

    }
}
