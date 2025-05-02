using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string Name { get; set; } = "Ivan";

        [MaxLength(20)]
        public string Lastname { get; set; } = "Ivanov";

        [Required]
        public DateOnly BirthDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
