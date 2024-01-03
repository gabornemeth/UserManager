using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using UserManager.Contracts.Dtos;

namespace UserManager.Contracts.Requests
{
    public class UpdateUserRequest
    {
        public string Id { get; set; }

        [JsonProperty("update")]
        public JsonPatchDocument<UserDto> Update { get; set; } = new JsonPatchDocument<UserDto>();
    }
}
