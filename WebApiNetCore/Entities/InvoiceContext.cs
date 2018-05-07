using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebApiNetCore.Entities
{
    public class InvoiceContext : DbContext, IInvoiceContext
    {
        public InvoiceContext(DbContextOptions<InvoiceContext> options)
            : base(options)
        {
            DbContext = this;
        }

        public DbContext DbContext { get; set; }

        public DbSet<Invoice> Invoice { get; set; }

        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        public DbSet<Secret> Secrets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<InvoiceItem>().HasOne(x => x.Invoice).WithMany(x => x.InvoiceItems);
            modelBuilder.Entity<Invoice>().Property(x => x.Created).HasDefaultValueSql("getDate()");
        }
    }

    public class InvoiceContextFactory : IDesignTimeDbContextFactory<InvoiceContext>
    {
        public InvoiceContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var optionsBuilder = new DbContextOptionsBuilder<InvoiceContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("InvoiceDatabase"));

            return new InvoiceContext(optionsBuilder.Options);
        }
    }
}