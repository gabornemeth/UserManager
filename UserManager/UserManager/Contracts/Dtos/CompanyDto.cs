using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace UserManager.Contracts.Dtos
{
    public class CompanyDto
    {
        public required string Name { get; set; }
        
        public string? CatchPhrase { get; set; }

        [JsonProperty("bs")]
        [JsonPropertyName("bs")]
        public string? BusinessServices { get; set; }
    }
}
