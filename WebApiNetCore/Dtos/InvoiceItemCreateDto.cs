using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNetCore.Dtos
{
    public class InvoiceItemCreateDto
    {
        [Required]
        public string Name { get; set; }
        public string Type { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public int InvoiceId { get; set; }
       
    }
}

