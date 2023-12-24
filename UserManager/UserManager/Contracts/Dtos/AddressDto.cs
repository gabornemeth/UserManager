using System.Text.Json.Serialization;

namespace UserManager.Contracts.Dtos
{
    public class AddressDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Suite { get; set; }

        [JsonPropertyName("geo")]
        public LocationDto Location { get; set; }
    }

    public class LocationDto
    {
        [JsonPropertyName("lat")]
        public float Latitude { get; set; }

        [JsonPropertyName("lng")]
        public float Longitude { get; set; }
    }
}
