using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace UserManager.Contracts.Dtos
{
    public class AddressDto
    {
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? Suite { get; set; }

        [JsonProperty("geo")]
        [JsonPropertyName("geo")]
        public LocationDto? Geolocation { get; set; }

        public bool IsEmpty() => string.IsNullOrEmpty(City) &&
            string.IsNullOrEmpty(ZipCode) &&
            string.IsNullOrEmpty(Street) &&
            string.IsNullOrEmpty(Suite) &&
            (Geolocation?.IsEmpty() ?? true);
    }
}
