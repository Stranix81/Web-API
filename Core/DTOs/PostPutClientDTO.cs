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
        public string name { get; set; } = "Ivan";

        /// <summary>
        /// Client lastname
        /// </summary>
        [MaxLength(20)]
        public string lastname { get; set; } = "Ivanov";

        /// <summary>
        /// Client birth date
        /// </summary>
        [Required]
        public DateOnly birth_date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
