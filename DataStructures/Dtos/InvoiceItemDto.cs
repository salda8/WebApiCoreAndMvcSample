using System.ComponentModel.DataAnnotations;

namespace DataStructures.Dtos
{
    public class InvoiceItemDto : Dto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public int InvoiceId { get; set; }

    }
}
