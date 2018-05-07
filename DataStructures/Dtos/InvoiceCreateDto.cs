using System;
using System.ComponentModel.DataAnnotations;

namespace DataStructures.Dtos
{
    public class InvoiceCreateDto : Dto
    {
        [Required]
        public string Name { get; set; }

        public string Type { get; set; }
        public DateTime DueDate { get; set; }
    }

    public class Dto
    {
    }
}