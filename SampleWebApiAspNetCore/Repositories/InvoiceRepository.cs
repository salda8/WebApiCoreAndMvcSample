using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Models;
using System.Linq.Dynamic.Core;
using SampleWebApiAspNetCore.Helpers;

namespace SampleWebApiAspNetCore.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ConcurrentDictionary<int, InvoiceItem> _storage = new ConcurrentDictionary<int, InvoiceItem>();

        public InvoiceItem GetSingle(int id)
        {
            InvoiceItem InvoiceItem;
            return _storage.TryGetValue(id, out InvoiceItem) ? InvoiceItem : null;
        }

        public void Add(InvoiceItem item)
        {
            item.Id = !_storage.Values.Any() ? 1 : _storage.Values.Max(x => x.Id) + 1;

            if (!_storage.TryAdd(item.Id, item))
            {
                throw new Exception("Item could not be added");
            }
        }

        public void Delete(int id)
        {
            InvoiceItem InvoiceItem;
            if (!_storage.TryRemove(id, out InvoiceItem))
            {
                throw new Exception("Item could not be removed");
            }
        }

        public InvoiceItem Update(int id, InvoiceItem item)
        {
            _storage.TryUpdate(id, item, GetSingle(id));
            return item;
        }

        public IQueryable<InvoiceItem> GetAll(QueryParameters queryParameters)
        {
            IQueryable<InvoiceItem> _allItems = _storage.Values.AsQueryable().OrderBy(queryParameters.OrderBy,
               queryParameters.IsDescending());

            if (queryParameters.HasQuery())
            {
                _allItems = _allItems
                    .Where(x => x.Calories.ToString().Contains(queryParameters.Query.ToLowerInvariant())
                    || x.Name.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            return _allItems
                .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount);
        }

        public ICollection<InvoiceItem> GetRandomMeal()
        {
            List<InvoiceItem> toReturn = new List<InvoiceItem>();

            toReturn.Add(GetRandomItem("Starter"));
            toReturn.Add(GetRandomItem("Main"));
            toReturn.Add(GetRandomItem("Dessert"));

            return toReturn;
        }

        public int Count()
        {
            return _storage.Count;
        }

        public bool Save()
        {
            // To keep interface consistent with Controllers, Tests & EF Interfaces
            return true;
        }
        private InvoiceItem GetRandomItem(string type)
        {
            return _storage.Values
                .Where(x => x.Type == type)
                .OrderBy(o => Guid.NewGuid())
                .FirstOrDefault();
        }
    }
}