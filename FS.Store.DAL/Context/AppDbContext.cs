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
        public DbSet<CashClosing> CashClosings { get; set; } = default!;
        public DbSet<CashClosingTerminal> CashClosingTerminals { get; set; } = default!;
        public DbSet<CashMovement> CashMovements { get; set; } = default!;
        public DbSet<CashRegisterSession> CashRegisterSessions { get; set; } = default!;
        public DbSet<Operation> Operations { get; set; } = default!;
        public DbSet<OperationUserRole> OperationUserRoles { get; set; } = default!;
        public DbSet<PaymentProvider> PaymentProviders { get; set; } = default!;
        public DbSet<PaymentTerminal> PaymentTerminals { get; set; } = default!;
        public DbSet<Role> Roles { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
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

                        entity.Property(nameof(BaseEntity<>.CreatedAt))
                            .HasColumnType("datetime(0)")
                            .HasDefaultValueSql("CURRENT_TIMESTAMP")
                            .ValueGeneratedOnAdd();

                        entity.Property(nameof(BaseEntity<>.UpdatedAt))
                            .HasColumnType("datetime(0)")
                            .HasDefaultValueSql(
                                "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP")
                            .ValueGeneratedOnAddOrUpdate();

                        entity.Property(nameof(BaseEntity<>.Deleted))
                            .HasDefaultValue(false);

                        break;
                    }

                    baseType = baseType.BaseType;
                }
            }

            modelBuilder.Entity<CashRegisterSession>(entity =>
            {
                entity.Property(e => e.OpenedAt)
                    .HasColumnType("datetime(0)")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValue(true);

                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Document).IsUnique();
                entity.HasIndex(e => new { e.Document, e.Email }).IsUnique();
            });

            modelBuilder.Entity<OperationUserRole>(entity =>
            {
                entity.HasIndex(e => new
                {
                    e.UserId,
                    e.OperationId,
                    e.RoleId
                }).IsUnique();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.RememberMe).HasDefaultValue(false);
                entity.Property(e => e.IsBeta).HasDefaultValue(false);
                entity.Property(e => e.IsActive).HasDefaultValue(false);

                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
        #endregion
    }
}
