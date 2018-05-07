using System;
using System.Linq;
using WebApiNetCore.Entities;

namespace WebApiNetCore.Services
{
    public class SeedDataService : ISeedDataService
    {
        private IInvoiceContext context;

        public SeedDataService(IInvoiceContext context)
        {
            this.context = context;
        }

        public void EnsureSeedData()
        {
            if (!context.Invoice.Any())
            {
                context.Invoice.Add(new Invoice { Name = "PC", Type = "BestType", Amount = 2200, DueDate = DateTime.Now.AddDays(30) });
                context.InvoiceItems.Add(new InvoiceItem() { Amount = 1100, Name = "Procesor", InvoiceId = 1 });
                context.InvoiceItems.Add(new InvoiceItem() { Amount = 1100, Name = "GPU", InvoiceId = 1 });
                context.Secrets.Add(new Secret { Key = "Secret007", User = "007" });
            }
        }
    }
}