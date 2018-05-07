using System.ComponentModel.DataAnnotations;

namespace DataStructures.Dtos
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