using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class PostPutOrderDTO
    {
        public decimal cost { get; set; }

        public DateOnly date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public TimeOnly time { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

        [Required]
        public int client_id { get; set; }

        [RegularExpression("(Pending|Cancelled|Completed)", ErrorMessage = "Status must be 'Pending', 'Cancelled', or 'Completed'")]
        public string status { get; set; } = "Pending";
    }
}
