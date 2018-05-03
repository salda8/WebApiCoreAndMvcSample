using System;
using System.Collections;
using System.Collections.Generic;

namespace WebApiNetCore.Entities
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public DateTime Created { get; set; }
    }

    public class Invoice
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Type { get; set; }
        public int Amount { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; }
        public DateTime Created { get; set; }
    }
}
