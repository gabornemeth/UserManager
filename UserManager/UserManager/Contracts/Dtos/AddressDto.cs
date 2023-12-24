
using Newtonsoft.Json;

namespace UserManager.Contracts.Dtos
{
    public class AddressDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Suite { get; set; }

        [JsonProperty("geo")]
        public LocationDto Location { get; set; }
    }

    public class LocationDto
    {
        [JsonProperty("lat")]
        public float Latitude { get; set; }

        [JsonProperty("lng")]
        public float Longitude { get; set; }
    }
}
