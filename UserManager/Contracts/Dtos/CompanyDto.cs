using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace UserManager.Contracts.Dtos
{
    public class CompanyDto
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("catchPhrase")]
        public string? CatchPhrase { get; set; }

        [JsonProperty("bs")]
        [JsonPropertyName("bs")]
        public string? BusinessServices { get; set; }
    }
}
