using AutoMapper;
using DataStructures.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using WebApiNetCore.Entities;
using WebApiNetCore.Models;

namespace WebApiNetCore.Repositories
{
    public class InvoiceRepository : Repository, IInvoiceRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public InvoiceRepository(IInvoiceContext context) : base(context)
        {
        }

        public void Add(InvoiceCreateDto item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            base.Add(Mapper.Map<Invoice>(item));
            SaveChanges();
        }

        public InvoiceDto ChangeStatus(int id, Status status)
        {
            var invoice = base.SingleOrDefault<Invoice>(x => x.Id == id);
            if (invoice == null)
            {
                return null;
            }
            invoice.Status = status;
            UpdateSingleProperty(invoice, "Status");

            Save();
            return Mapper.Map<InvoiceDto>(invoice);
        }

        public void Delete(int id)
        {
            SetDeleted(base.SingleOrDefault<Invoice>(x => x.Id == id && !x.IsDeleted), true);
            Save();
        }

        public IEnumerable<InvoiceDto> GetAll(QueryParameters queryParameters)
        {
            var toReturn = Context.Invoice.Where(x => !x.IsDeleted)
                .Select(x =>
                  new
                  {
                      invoice = x,
                      items = x.InvoiceItems.Where(y => !y.IsDeleted)
                  }).ToList()
                .Select(y =>
              {
                  var x = y.invoice;
                  x.InvoiceItems = y.items.ToList();
                  return x;
              });
            return Mapper.Map<IEnumerable<InvoiceDto>>(toReturn);
        }

        public InvoiceDto GetSingle(int id)
        {
            var invoice = base.FindInner<Invoice>(x => x.Id == id && !x.IsDeleted).Include(x => x.InvoiceItems).SingleOrDefault();
            if (invoice == null)
            {
                return null;
            }
            return Mapper.Map<InvoiceDto>(invoice);
        }

        public bool Save()
        {
            // To keep interface consistent with Controllers, Tests & EF Interfaces
            base.SaveChanges();
            return true;
        }

        public InvoiceDto Update(int id, InvoiceUpdateDto item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var invoice = base.FindInner<Invoice>(x => x.Id == id && !x.IsDeleted).Include(x => x.InvoiceItems).SingleOrDefault();
            invoice = Mapper.Map(item, invoice);
            base.Update(invoice);
            SaveChanges();
            return GetSingle(id);
        }
    }
}