using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json;
using UserManager.Contracts.Dtos;
using UserManager.Contracts.Requests;

namespace UserManager.Test
{
    public class JsonParsingTests
    {
        [Fact]
        public void ToJson1()
        {
            var user = new UserDto { Id = 8, Name = "John Doe" };
            var json = JsonConvert.SerializeObject(user);
        }

        [Fact]
        public void ParseSampleData()
        {
            var users = SampleData.GetUsers();

            users.Should().HaveCount(10);
            var firstUser = users.First();
            firstUser.Name.Should().Be("Leanne Graham");
            firstUser.Address.Location.Should().NotBeNull();
            firstUser.Address.Location!.Latitude.Should().NotBe(0.0f);
            firstUser.Address.Location!.Latitude.Should().NotBe(0.0f);
        }

        [Fact]
        public void JsonPatchDocument_ToJson()
        {
            var patch = new JsonPatchDocument<UserDto>();
            patch.Replace(u => u.Name, "Updated User");
            var json = JsonConvert.SerializeObject(patch);
        }

        [Fact]
        public void JsonPatchDocument_FromJson()
        {
            var patchJson = """[ { "op": "replace", "path": "/name", "value": "Updated User" } ]""";
            Newtonsoft.Json.JsonConvert.DeserializeObject<JsonPatchDocument<UserDto>>(patchJson);
        }

        [Fact]
        public void PartialUpdateUserRequest_FromJson()
        {
            var patchJson = """{ update: [ { "op": "replace", "path": "/name", "value": "Updated User" } ] }""";
            Newtonsoft.Json.JsonConvert.DeserializeObject<PartialUpdateUserRequest>(patchJson);
        }
    }
}
