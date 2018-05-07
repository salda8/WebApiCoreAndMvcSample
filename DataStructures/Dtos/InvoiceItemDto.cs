using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiNetCore.Dtos
{
    public class InvoiceItemDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public int InvoiceId { get; set; }

    }
}
