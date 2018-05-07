using DataStructures.Dtos;
using System.Collections.Generic;

namespace WebApiNetCore.Repositories
{
    public interface IInvoiceItemRepository
    {
        void Add(InvoiceItemCreateDto invoiceItem);

        void Delete(int id);

        IEnumerable<InvoiceItemDto> GetAll();

        InvoiceItemDto GetSingle(int id);
    }
}