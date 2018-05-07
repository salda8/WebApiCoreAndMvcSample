using System.Collections.Generic;
using DataStructures.Dtos;
using WebApiNetCore.Entities;

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