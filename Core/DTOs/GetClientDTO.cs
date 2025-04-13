using Core.Models;

namespace Core.DTOs
{
    public class GetClientDTO
    {
        public GetClientDTO() { }
        public GetClientDTO(Client client)
        {
            id = client.id;
            name = client.name;
            lastname = client.lastname;
            birth_date = client.birth_date;
        }

        public int id { get; set; }

        public string name { get; set; } = "Ivan";

        public string lastname { get; set; } = "Ivanov";

        public DateOnly birth_date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
