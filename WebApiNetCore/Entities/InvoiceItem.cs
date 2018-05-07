using System.ComponentModel.DataAnnotations;

namespace WebApiNetCore.Entities
{
    public class InvoiceItem : IEntity
    {
        public int Amount { get; set; }

        [Key]
        public int Id { get; set; }

        public Invoice Invoice { get; set; }
        public int InvoiceId { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public string Type { get; set; }
    }
}