using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace UserManager.Contracts.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("username")]
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        public string Email { get; set; }

        public AddressDto Address { get; set; }

        public string Phone { get; set; }

        public string? Website { get; set; }

        public CompanyDto Company { get; set; }
    }
}
