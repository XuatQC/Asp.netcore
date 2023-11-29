using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public partial class DBMasterContext : DbContext
    {
        public DbSet<Tenant>? Tenants { get; set; }
        public DbSet<Account>? Accounts { get; set; }

        public DBMasterContext(DbContextOptions<DBMasterContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>().HasMany(a => a.Accounts).WithOne(t => t.Tenant).HasPrincipalKey(q => q.Code).HasForeignKey(s => s.TenantCode);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }

    public partial class DBSlaveContext : DbContext
    {
        public DbSet<Tenant>? Tenants { get; set; }
        public DbSet<Account>? Accounts { get; set; }
        public DBSlaveContext(DbContextOptions<DBSlaveContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>().HasMany(a => a.Accounts).WithOne(t => t.Tenant).HasPrincipalKey(q => q.Code).HasForeignKey(s => s.TenantCode);
        }
    }
}