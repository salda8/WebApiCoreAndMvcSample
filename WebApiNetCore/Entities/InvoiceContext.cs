using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNetCore.Entities
{
    public class InvoiceContext : DbContext, IInvoiceContext
    {
        public InvoiceContext(DbContextOptions<InvoiceContext> options)
            : base(options)
        {
            DbContext = this;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<InvoiceItem>().HasOne(x => x.Invoice).WithMany(x => x.InvoiceItems);
            modelBuilder.Entity<Invoice>().Property(x=>x.Created).HasDefaultValueSql("getDate()");

        }

       
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbContext DbContext { get; set; }
    }
}
