using System.Collections.Generic;
using WebApiNetCore.Entities;

namespace WebApiNetCore.Repositories
{
    public interface IInvoiceItemRepository
    {
        void Add(InvoiceItem invoiceItem);
        void Delete(int id);
        IEnumerable<InvoiceItem> GetAll();
        InvoiceItem GetSingle(int id);
    }
}