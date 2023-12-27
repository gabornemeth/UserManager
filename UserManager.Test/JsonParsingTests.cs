using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Namotion.Reflection;
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
        public void PartialUpdateUserRequest_ReplaceName_FromJson()
        {
            var patchJson = """{ update: [ { "op": "replace", "path": "/name", "value": "Updated User" } ] }""";
            Newtonsoft.Json.JsonConvert.DeserializeObject<PartialUpdateUserRequest>(patchJson);
        }

        [Fact]
        public void PartialUpdateUserRequest_ReplaceEmail_FromJson()
        {
            var user = new UserDto();
            var patchJson = """{ update: [ { "op": "replace", "path": "/email", "value": "new_user@old_domain.com" } ] }""";
            var patch = JsonConvert.DeserializeObject<PartialUpdateUserRequest>(patchJson);
            patch.Update.ApplyTo(user);

            user.Email.Should().Be("new_user@old_domain.com");
        }

        [Fact]
        public void PartialUpdateUserRequest_ReplaceCompanyName_FromJson()
        {
            var user = new UserDto();
            var patchJson = """{ update: [ { op: "replace", path: "/company/name", value: "Microsoft" } ] }""";
            var patch = JsonConvert.DeserializeObject<PartialUpdateUserRequest>(patchJson);
            user.ApplyPatch(patch.Update);

            user.Company.Should().NotBeNull();
            user.Company!.Name.Should().Be("Microsoft");
        }

        [Fact]
        public void PartialUpdateUserRequest_ReplaceCompany_FromJson()
        {
            var user = new UserDto();
            var patchJson = """[ { op: "replace", "path": "/company", "value": { "name": "Microsoft", "catchPhrase": "M$", "bs": "software development" } } ]""";
            var patch = JsonConvert.DeserializeObject<JsonPatchDocument<UserDto>>(patchJson);
            user.ApplyPatch(patch);

            user.Company.Should().NotBeNull();
            user.Company!.Name.Should().Be("Microsoft");
            user.Company!.CatchPhrase.Should().Be("M$");
            user.Company!.BusinessServices.Should().Be("software development");
        }

        [Fact]
        public void PartialUpdateUserRequest_RemoveCompany_FromJson()
        {
            var user = new UserDto { Company = new CompanyDto { Name = "Microsoft", CatchPhrase = "M$", BusinessServices = "software development" } };
            var patchJson = """[ { op: "remove", "path": "/company" } ]""";
            var patch = JsonConvert.DeserializeObject<JsonPatchDocument<UserDto>>(patchJson);
            user.ApplyPatch(patch);

            user.Company.Should().BeNull();
            //user.Company!.Name.Should().Be("Microsoft");
            //user.Company!.CatchPhrase.Should().Be("M$");
            //user.Company!.BusinessServices.Should().Be("software development");
        }

    }
}
