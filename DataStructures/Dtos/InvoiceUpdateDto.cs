using System;

namespace DataStructures.Dtos
{
    public class InvoiceUpdateDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime? DueDate { get; set; }
    }
}