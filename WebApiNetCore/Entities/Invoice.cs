using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApiNetCore.Dtos;

namespace WebApiNetCore.Entities
{
    public class Invoice : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Status Status { get; set; }
        public int Amount { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
