using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class PostPutOrderDTO
    {
        public decimal Cost { get; set; }

        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public TimeOnly Time { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

        [Required]
        public int ClientId { get; set; }

        [RegularExpression("(Pending|Cancelled|Completed)", ErrorMessage = "Status must be 'Pending', 'Cancelled', or 'Completed'")]
        public string Status { get; set; } = "Pending";
    }
}
