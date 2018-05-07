using DataStructures.Dtos;
using System.Collections.Generic;
using WebApiNetCore.Models;

namespace WebApiNetCore.Repositories
{
    public interface IInvoiceRepository
    {
        void Add(InvoiceCreateDto item);

        InvoiceDto ChangeStatus(int id, Status status);

        void Delete(int id);

        IEnumerable<InvoiceDto> GetAll(QueryParameters queryParameters);

        InvoiceDto GetSingle(int id);

        bool Save();

        InvoiceDto Update(int id, InvoiceUpdateDto item);
    }
}