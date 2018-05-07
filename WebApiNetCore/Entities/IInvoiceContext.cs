using Microsoft.EntityFrameworkCore;

namespace WebApiNetCore.Entities
{
    public interface IInvoiceContext
    {
        DbContext DbContext { get; set; }
        DbSet<Invoice> Invoice { get; set; }
        DbSet<InvoiceItem> InvoiceItems { get; set; }
        DbSet<Secret> Secrets { get; set; }
    }
}