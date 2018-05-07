﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebMvcNetCore.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public int InvoiceId { get; set; }

    }

    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Status Status { get; set; }
        public int Amount { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public DateTime DueDate { get; set; }

    }

    public enum Status
    {
        Unpaid,
        Paid
    }
}
