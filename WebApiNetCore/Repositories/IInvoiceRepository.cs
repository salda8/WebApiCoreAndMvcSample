using System.Collections.Generic;
using System.Linq;
using WebApiNetCore.Entities;
using WebApiNetCore.Models;

namespace WebApiNetCore.Repositories
{
    public interface IInvoiceRepository
    {
        Invoice GetSingle(int id);
        void Add(InvoiceItem item);
        void Delete(int id);
        Invoice Update(int id, Invoice item);
        Invoice ChangeStatus(int id, Status status);

        IQueryable<Invoice> GetAll(QueryParameters queryParameters);
        int Count();

        bool Save();
    }
}
