using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace JFS_Task
{
    public class DomainModelPostgreSqlContext : DbContext
    {
        public DbSet<Balance> Balances { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<TurnoverBalance> TurnoverBalance { get; set; }

        //public DbSet<Balance> DataEventRecordsBalance { get; set; }
        //public DbSet<Payment> DataEventRecordsPayments { get; set; }

        public DomainModelPostgreSqlContext(DbContextOptions<DomainModelPostgreSqlContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("DataAccessPostgreSqlProvider");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Balance>().HasKey(m => m.RecId);
            builder.Entity<Payment>().HasKey(m => m.RecId);
            builder.Entity<TurnoverBalance>().HasKey(m => m.Period);

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            updateUpdatedProperty<Balance>();
            updateUpdatedProperty<Payment>();
            updateUpdatedProperty<TurnoverBalance>();

            return base.SaveChanges();
        }

        private void updateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
        }
    }
}
