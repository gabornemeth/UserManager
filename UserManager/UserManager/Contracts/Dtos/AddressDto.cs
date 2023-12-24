using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace UserManager.Contracts.Dtos
{
    public class AddressDto
    {
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string ZipCode { get; set; }
        public string? Suite { get; set; }

        [JsonProperty("geo")]
        [JsonPropertyName("geo")]
        public LocationDto? Location { get; set; }
    }
}
