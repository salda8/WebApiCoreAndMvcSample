using System.Collections.Generic;
using System.Linq;
using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Models;

namespace SampleWebApiAspNetCore.Repositories
{
    public interface IInvoiceRepository
    {
        InvoiceItem GetSingle(int id);
        void Add(InvoiceItem item);
        void Delete(int id);
        InvoiceItem Update(int id, InvoiceItem item);
        IQueryable<InvoiceItem> GetAll(QueryParameters queryParameters);

        ICollection<InvoiceItem> GetRandomMeal();
        int Count();

        bool Save();
    }
}
