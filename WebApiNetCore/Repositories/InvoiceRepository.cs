using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Dynamic.Core;
using WebApiNetCore.Entities;
using WebApiNetCore.Helpers;
using WebApiNetCore.Models;

namespace WebApiNetCore.Repositories
{
    public class InvoiceRepository : Repository, IInvoiceRepository
    {
        private readonly IInvoiceContext invoiceContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public InvoiceRepository(IInvoiceContext context) : base(context)
        {
        }

        public Invoice GetSingle(int id)
        {
            var invoice = base.SingleOrDefault<Invoice>(x => x.Id == id && !x.IsDeleted);
            if (invoice == null)
            {
                return null;
            }
            return invoice;
        }

        public void Add(Invoice item)
        {
            base.Add(item);
           
            Save();
        }

        public void Delete(int id)
        {
                    
            SetDeleted(GetSingle(id), true);
            Save();
        }

        public Invoice Update(int id, Invoice item)
        {
            base.Update(item);
            SaveChanges();
            return GetSingle(id);
        }

        public IQueryable<Invoice> GetAll(QueryParameters queryParameters)
        {
            IQueryable<Invoice> _allItems = base.GetAllInner<Invoice>().OrderBy(queryParameters.OrderBy,
               queryParameters.IsDescending());

            if (queryParameters.HasQuery())
            {
                _allItems = _allItems
                    .Where(x => x.Amount.ToString().Contains(queryParameters.Query.ToLowerInvariant())
                    || x.Name.IndexOf(queryParameters.Query, StringComparison.InvariantCultureIgnoreCase) >= 0);
            }

            return _allItems.Where(x=>!x.IsDeleted)
                .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount);
        }
               

        public bool Save()
        {
            // To keep interface consistent with Controllers, Tests & EF Interfaces
            base.SaveChanges();
            return true;
        }

        public Invoice ChangeStatus(int id, Status status)
        {
            var invoice = invoiceContext.Invoice.FirstOrDefault(x => x.Id == id);
            if (invoice == null)
            {
                return null;
            }
            invoice.Status = status;
            UpdateSingleProperty(invoice, "Status");
            
            Save();
            return invoice;
        }
    }
}