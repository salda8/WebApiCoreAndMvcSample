using System.ComponentModel.DataAnnotations;

namespace WebApiNetCore.Entities
{
    public class Secret
    {
        [Key]
        public int Id { get; set; }

        public string Key { get; set; }
        public string User { get; set; }
    }
}