using WebApiNetCore.Repositories;
using System;
using WebApiNetCore.Entities;
using System.Linq;

namespace WebApiNetCore.Services
{
    public class SeedDataService : ISeedDataService
    {
        IInvoiceContext context;
       

        public SeedDataService(IInvoiceContext context)
        {
            this.context = context;
            
        }

        public void EnsureSeedData()
        {
            if (!context.Invoice.Any())
            {
                context.Invoice.Add(new Invoice { Name = "PC", Type = "BestType", DueDate = DateTime.Now.AddDays(30) });
                context.InvoiceItems.Add(new InvoiceItem() { Amount = 1100, Name = "Procesor", InvoiceId = 1 });
                context.InvoiceItems.Add(new InvoiceItem() { Amount = 1100, Name = "GPU", InvoiceId = 1 });
            }
           
        }
    }
}
