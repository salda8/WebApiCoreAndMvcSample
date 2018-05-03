using System;

namespace WebApiNetCore.Dtos
{
    public class InvoiceItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime Created { get; set; }
    }
}
