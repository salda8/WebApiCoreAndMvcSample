using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using WebApiNetCore.Dtos;
using WebApiNetCore.Entities;
using WebApiNetCore.Helpers;
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

        public InvoiceDto GetSingle(int id)
        {
            var invoice = base.FindInner<Invoice>(x => x.Id == id && !x.IsDeleted).Include(x => x.InvoiceItems).SingleOrDefault();
            if (invoice == null)
            {
                return null;
            }
            return Mapper.Map<InvoiceDto>(invoice);
        }

        public void Add(InvoiceCreateDto item)
        {
            base.Add(Mapper.Map<Invoice>(item));
            SaveChanges();
        }

        public void Delete(int id)
        {
            SetDeleted(base.SingleOrDefault<Invoice>(x => x.Id == id && !x.IsDeleted), true);
            Save();
        }

        public InvoiceDto Update(int id, InvoiceUpdateDto item)
        {
            var invoice = base.FindInner<Invoice>(x => x.Id == id && !x.IsDeleted).Include(x => x.InvoiceItems).SingleOrDefault();
            invoice = Mapper.Map(item, invoice);
            base.Update(invoice);
            SaveChanges();
            return GetSingle(id);
        }

        public IEnumerable<InvoiceDto> GetAll(QueryParameters queryParameters)
        {
            IQueryable<Invoice> _allItems = base.GetAllInner<Invoice>().Include(x => x.InvoiceItems).OrderBy(queryParameters.OrderBy,
               queryParameters.IsDescending());

            if (queryParameters.HasQuery())
            {
                _allItems = _allItems
                    .Where(x => x.Amount.ToString().Contains(queryParameters.Query.ToLowerInvariant())
                    || x.Name.IndexOf(queryParameters.Query, StringComparison.InvariantCultureIgnoreCase) >= 0);
            }

            return Mapper.Map<IEnumerable<InvoiceDto>>(_allItems.Where(x => !x.IsDeleted)
                .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount).ToList());
        }

        public bool Save()
        {
            // To keep interface consistent with Controllers, Tests & EF Interfaces
            base.SaveChanges();
            return true;
        }

        public InvoiceDto ChangeStatus(int id, Status status)
        {
            var invoice = base.SingleOrDefault<Invoice>(x=>x.Id==id);
            if (invoice == null)
            {
                return null;
            }
            invoice.Status = status;
            UpdateSingleProperty(invoice, "Status");

            Save();
            return Mapper.Map<InvoiceDto>(invoice);
        }
    }
}