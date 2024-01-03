using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace UserManager.Contracts.Dtos
{
    public class UserDto : UserDtoWithoutIdentifier
    {
        public string Id { get; set; }
    }

    public class UserDtoWithoutIdentifier
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("username")]
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        public string Email { get; set; }

        public AddressDto? Address { get; set; }

        public string? Phone { get; set; }

        public string? Website { get; set; }

        [JsonProperty("company")]
        public CompanyDto? Company { get; set; }
    }
}
