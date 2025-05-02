using Core.Models;

namespace Core.DTOs
{
    public class GetClientDTO
    {
        public GetClientDTO() { }
        public GetClientDTO(Client client)
        {
            Id = client.Id;
            Name = client.Name;
            Lastname = client.Lastname;
            BirthDate = client.BirthDate;
        }

        public int Id { get; set; }

        public string Name { get; set; } = "Ivan";

        public string Lastname { get; set; } = "Ivanov";

        public DateOnly BirthDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
