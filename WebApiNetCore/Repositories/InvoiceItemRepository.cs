using System.Collections.Generic;
using WebApiNetCore.Entities;

namespace WebApiNetCore.Repositories
{
    public class InvoiceItemRepository : Repository, IInvoiceItemRepository
    {
        public InvoiceItemRepository(IInvoiceContext context) : base(context)
        {
        }

        public InvoiceItem GetSingle(int id)
        {
            return base.SingleOrDefault<InvoiceItem>(x => x.Id == id);
        }

        public IEnumerable<InvoiceItem> GetAll()
        {
            return base.GetAll<InvoiceItem>();
        }

        public void Delete(int id)
        {
            SetDeleted(base.SingleOrDefault<IEntity>(x => x.Id == id), true);
            SaveChanges();
        }

        public void Add(InvoiceItem invoiceItem)
        {
            base.Add(invoiceItem);
            SaveChanges();
        }
    }
}