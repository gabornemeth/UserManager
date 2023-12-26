using Newtonsoft.Json;
using UserManager.Contracts.Dtos;

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
    }
}
