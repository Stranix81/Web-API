using System.Text.Json.Serialization;
using Core.Models;
using Core.Enums;

namespace Core.DTOs
{
    public class GetOrderDTO
    {
        public GetOrderDTO() { }

        public GetOrderDTO(Order order)
        {
            Id = order.Id;
            Cost = order.Cost;
            Date = order.Date;
            Time = order.Time;
            ClientId = order.ClientId;
            Status = order.Status;
        }

        [JsonPropertyOrder(1)]
        public int Id { get; set; }

        [JsonPropertyOrder(2)]
        public decimal Cost { get; set; }

        [JsonPropertyOrder(3)]
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [JsonPropertyOrder(4)]
        public TimeOnly Time { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

        [JsonPropertyOrder(5)]
        public int ClientId { get; set; }

        [JsonPropertyOrder(7)]
        public OrderStatus Status { get; set; }
    }

    public class GetOrderWithClientDTO : GetOrderDTO
    {
        public GetOrderWithClientDTO() { }

        public GetOrderWithClientDTO(Order order)
        {
            Id = order.Id;
            Cost = order.Cost;
            Date = order.Date;
            Time = order.Time;
            ClientId = order.ClientId;
            Status = order.Status;
            Client = new GetClientDTO(order.Client);
        }

        [JsonPropertyOrder(6)]
        public GetClientDTO Client { get; set; }
    }
}
