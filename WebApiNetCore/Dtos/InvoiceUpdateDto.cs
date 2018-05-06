using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNetCore.Dtos
{
    public class InvoiceUpdateDto
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; }
        public DateTime DueDate { get; set; }
       
    }
}
