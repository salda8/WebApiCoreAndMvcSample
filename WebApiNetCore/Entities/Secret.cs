using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
