using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Core.Models;
using Core.Enums;

namespace Core.DTOs
{
    public class GetOrderDTO
    {
        public GetOrderDTO() { }

        public GetOrderDTO(Order order)
        {
            id = order.id;
            cost = order.cost;
            date = order.date;
            time = order.time;
            client_id = order.client_id;
            status = order.status;
        }

        [JsonPropertyOrder(1)]
        public int id { get; set; }

        [JsonPropertyOrder(2)]
        public decimal cost { get; set; }

        [JsonPropertyOrder(3)]
        public DateOnly date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [JsonPropertyOrder(4)]
        public TimeOnly time { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

        [JsonPropertyOrder(5)]
        public int client_id { get; set; }

        [JsonPropertyOrder(7)]
        public OrderStatus status { get; set; }
    }

    public class GetOrderWithClientDTO : GetOrderDTO
    {
        [JsonPropertyOrder(6)]
        public GetClientDTO client { get; set; }
    }
}
