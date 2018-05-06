using System;
using System.Collections.Generic;
using WebApiNetCore.Entities;

namespace WebApiNetCore.Dtos
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<InvoiceItemDto> InvoiceItems{ get; set; }
    }
}
