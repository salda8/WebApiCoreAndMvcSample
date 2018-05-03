using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNetCore.Dtos
{
    public class InvoiceCreateDto
    {
        [Required]
        public string Name { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime Created { get; set; }
    }
}
