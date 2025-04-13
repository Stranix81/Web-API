using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.Models
{
    public class Order
    {
        [Required]
        public int id { get; set; }

        [Required]
        public decimal cost { get; set; }

        [Required]
        public DateOnly date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [Required]
        public TimeOnly time { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

        [Required]
        public int client_id { get; set; }

        public Client? client { get; set; }

        [Required]
        public OrderStatus status { get; set; }
    }
}
