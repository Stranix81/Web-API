﻿using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class PostPutOrderDTO
    {
        public decimal cost { get; set; }

        public DateOnly date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public TimeOnly time { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

        [Required]
        public int client_id { get; set; }

        [RegularExpression("(Pending|Cancelled|Completed)", ErrorMessage = "Status must be 'Pending', 'Cancelled', or 'Completed'")]
        public string status { get; set; } = "Pending";
    }
}
