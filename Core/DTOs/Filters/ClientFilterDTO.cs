using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Filters
{
    /// <summary>
    /// Client filter info
    /// </summary>
    public class ClientFilterDTO
    {
        /// <summary>
        /// Client name
        /// </summary>
        [MaxLength(15)]
        public string? Name { get; set; }

        /// <summary>
        /// Client lastname
        /// </summary>
        [MaxLength(20)]
        public string? Lastname { get; set; }

        /// <summary>
        /// Client birth date
        /// </summary>
        public DateOnly? BirthDate { get; set; }
    }
}
