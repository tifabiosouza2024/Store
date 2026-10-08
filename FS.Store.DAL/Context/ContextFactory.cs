using FS.Store.Model.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

#pragma warning disable CS8603 // Possível retorno de referência nula.
namespace FS.Store.DAL.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        private const string ConnectionStringsNameSection = "DefaultConnection";
        public AppDbContext CreateDbContext(string[] args)
        {
            var connectionString = ConnectionString;

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "A connection string DefaultConnection não foi encontrada.");
            }

            var builder = new MySqlConnectionStringBuilder(connectionString);

            if (string.IsNullOrWhiteSpace(builder.Server))
            {
                throw new InvalidOperationException(
                    "O host da conexão MySQL está vazio.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder
                .UseLazyLoadingProxies()
                .UseMySQL(ConnectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
        private static string ConnectionString
            => EnvironmentUtil.Configuration.GetConnectionString(ConnectionStringsNameSection);
    }
}
#pragma warning restore CS8603 // Possível retorno de referência nula.
