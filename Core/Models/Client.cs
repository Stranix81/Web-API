using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Client
    {
        public int id { get; set; }

        [Required]
        [MaxLength(15)]
        public string name { get; set; } = "Ivan";

        [MaxLength(20)]
        public string lastname { get; set; } = "Ivanov";

        [Required]
        public DateOnly birth_date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
