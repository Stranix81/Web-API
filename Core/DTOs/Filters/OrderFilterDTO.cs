using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public decimal? cost { get; set; }

        /// <summary>
        /// Order date
        /// </summary>
        public DateOnly? date { get; set; }

        /// <summary>
        /// Order time
        /// </summary>
        public TimeOnly? time { get; set; }

        /// <summary>
        /// Order client ID
        /// </summary>
        public int? client_id { get; set; }

        /// <summary>
        /// Order status
        /// </summary>
        [RegularExpression("(Pending|Cancelled|Completed)", ErrorMessage = "Status must be 'Pending', 'Cancelled', or 'Completed'")]
        public string? status { get; set; }
    }
}
