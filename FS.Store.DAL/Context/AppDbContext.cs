using FS.Store.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace FS.Store.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        #region DbSets
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Role> Roles { get; set; } = default!;
        #endregion

        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;

                var baseType = clrType.BaseType;

                while (baseType != null)
                {
                    if (baseType.IsGenericType &&
                        baseType.GetGenericTypeDefinition() == typeof(BaseEntity<>))
                    {
                        var entity = modelBuilder.Entity(clrType);

                        entity.Property(nameof(BaseEntity<int>.CreatedAt))
                            .HasColumnType("datetime(0)")
                            .HasDefaultValueSql("CURRENT_TIMESTAMP")
                            .ValueGeneratedOnAdd();

                        entity.Property(nameof(BaseEntity<int>.UpdatedAt))
                            .HasColumnType("datetime(0)")
                            .HasDefaultValueSql(
                                "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP")
                            .ValueGeneratedOnAddOrUpdate();

                        entity.Property(nameof(BaseEntity<int>.Deleted))
                            .HasDefaultValue(false);

                        break;
                    }

                    baseType = baseType.BaseType;
                }
            }

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity("user_roles");
        }
        #endregion
    }
}
