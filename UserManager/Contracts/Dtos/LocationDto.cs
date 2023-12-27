
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace UserManager.Contracts.Dtos
{
    public class LocationDto
    {
        [JsonProperty("lat")]
        [JsonPropertyName("lat")]
        public float Latitude { get; set; } = float.NaN;

        [JsonProperty("lng")]
        [JsonPropertyName("lng")]
        public float Longitude { get; set; } = float.NaN;

        public bool IsEmpty() => float.IsNaN(Latitude) && float.IsNaN(Longitude);
    }
}
