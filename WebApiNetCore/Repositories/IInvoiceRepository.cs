using System.Collections.Generic;
using System.Linq;
using WebApiNetCore.Dtos;
using WebApiNetCore.Entities;
using WebApiNetCore.Models;

namespace WebApiNetCore.Repositories
{
    public interface IInvoiceRepository
    {
        InvoiceDto GetSingle(int id);
        void Add(InvoiceCreateDto item);
        void Delete(int id);
        InvoiceDto Update(int id, InvoiceUpdateDto item);
        InvoiceDto ChangeStatus(int id, Status status);
        IEnumerable<InvoiceDto> GetAll(QueryParameters queryParameters);
          
        bool Save();
    }
}
