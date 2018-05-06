using Microsoft.EntityFrameworkCore;

namespace WebApiNetCore.Entities
{
    public interface IInvoiceContext
    {
        DbSet<Invoice> Invoice { get; set; }
        DbSet<InvoiceItem> InvoiceItems { get; set; }
        DbContext DbContext { get; set; }
    }
}