using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class PostPutClientDTO
    {
        /// <summary>
        /// Client name
        /// </summary>
        [MaxLength(15)]
        [Required]
        public string Name { get; set; } = "Ivan";

        /// <summary>
        /// Client lastname
        /// </summary>
        [MaxLength(20)]
        public string Lastname { get; set; } = "Ivanov";

        /// <summary>
        /// Client birth date
        /// </summary>
        [Required]
        public DateOnly BirthDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
