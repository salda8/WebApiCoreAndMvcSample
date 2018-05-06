using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiNetCore.Entities
{
    public class InvoiceItem : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class InvoiceItemDto 
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public int InvoiceId { get; set; }
       
    }

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
        public DateTime DueDate{get;set;}
        public bool IsDeleted { get; set; }
    }
}
