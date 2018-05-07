using DataStructures.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiNetCore.Entities
{
    public class Invoice : IEntity
    {
        public int Amount { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime DueDate { get; set; }

        [Key]
        public int Id { get; set; }

        public ICollection<InvoiceItem> InvoiceItems { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public string Type { get; set; }
    }
}