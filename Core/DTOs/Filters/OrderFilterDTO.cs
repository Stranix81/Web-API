using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.DTOs.Filters
{
    /// <summary>
    /// Transferring orders filter info
    /// </summary>
    public class OrderFilterDTO
    {
        /// <summary>
        /// Order cost
        /// </summary>
        public decimal? Cost { get; set; }

        /// <summary>
        /// Order date
        /// </summary>
        public DateOnly? Date { get; set; }

        /// <summary>
        /// Order time
        /// </summary>
        public TimeOnly? Time { get; set; }

        /// <summary>
        /// Order client ID
        /// </summary>
        public int? ClientId { get; set; }

        ///// <summary>
        ///// Order status
        ///// </summary>
        //[RegularExpression("(Pending|Cancelled|Completed)",
        //    ErrorMessage = "Status must be 'Pending', 'Cancelled', or 'Completed'")]
        //public string? Status { get; set; }

        /// <summary>
        /// Order status
        /// </summary>
        [RegularExpression("(Pending|Cancelled|Completed)",
            ErrorMessage = "Status must be 'Pending', 'Cancelled', or 'Completed'")]
        public OrderStatus? Status { get; set; }
    }
}
