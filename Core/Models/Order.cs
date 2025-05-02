using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.Models
{
    public class Order
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [Required]
        public TimeOnly Time { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

        [Required]
        public int ClientId { get; set; }

        public Client? Client { get; set; }

        [Required]
        public OrderStatus Status { get; set; }
    }
}
