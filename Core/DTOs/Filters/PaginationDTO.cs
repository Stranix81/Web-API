using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Filters
{
    /// <summary>
    /// Transferring pagination info
    /// </summary>
    public class PaginationDTO
    {
        /// <summary>
        /// Current page
        /// </summary>
        [Range(1.0, int.MaxValue, ErrorMessage = "Page must be positive")]
        public int Page { get; set; } = 1;

        /// <summary>
        /// Page size
        /// </summary>
        [Range(1.0, 100.0, ErrorMessage = "PageSize mus be between 1 and 100")]
        public int PageSize { get; set; } = 10;
    }
}
