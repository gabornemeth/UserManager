using System.Text.Json.Serialization;

namespace UserManager.Dtos
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Suite { get; set; }

        [JsonPropertyName("geo")]
        public Location Location { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("lat")]
        public float Latitude { get; set; }

        [JsonPropertyName("lng")]
        public float Longitude { get; set; }
    }
}
