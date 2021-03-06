﻿using AutoMapper;
using DataStructures.Dtos;
using System;
using System.Collections.Generic;
using WebApiNetCore.Entities;
using WebApiNetCore.Helpers;

namespace WebApiNetCore.Repositories
{
    public class InvoiceItemRepository : Repository, IInvoiceItemRepository
    {
        public InvoiceItemRepository(IInvoiceContext context) : base(context)
        {
        }

        public InvoiceItemDto GetSingle(int id)
        {
            return Mapper.Map<InvoiceItemDto>(SingleOrDefault<InvoiceItem>(x => x.Id == id && !x.IsDeleted));
        }

        public IEnumerable<InvoiceItemDto> GetAll()
        {
            return Mapper.Map<IEnumerable<InvoiceItemDto>>(base.GetAllNotDeleted<InvoiceItem>());
        }

        public void Delete(int id)
        {
            var item = base.SingleOrDefault<InvoiceItem>(x => x.Id == id && !x.IsDeleted);
            ExceptionHelpers.ThrowNotFoundIfNull(item, nameof(InvoiceItem),id);
            UpdateInvoiceAmount(item, true);
            SetDeleted(item, true);
            SaveChanges();
        }

        public void Add(InvoiceItemCreateDto invoiceItemDto)
        {
            if (invoiceItemDto == null) throw new ArgumentNullException(nameof(invoiceItemDto));
            var invoiceItem = Mapper.Map<InvoiceItem>(invoiceItemDto);
            base.Add(invoiceItem);
            UpdateInvoiceAmount(invoiceItem);
        }

        private void UpdateInvoiceAmount(InvoiceItem invoiceItem, bool substractAmount = false)
        {
            var invoice = SingleOrDefault<Invoice>(x => x.Id == invoiceItem.InvoiceId);
            ExceptionHelpers.ThrowNotFoundIfNull(invoice, nameof(invoice), invoiceItem.Id);

            if (substractAmount)
            {
                invoice.Amount -= invoiceItem.Amount;
            }
            else
            {
                invoice.Amount += invoiceItem.Amount;
            }

            UpdateSingleProperty(invoice, "Amount");
            SaveChanges();
        }
    }
}