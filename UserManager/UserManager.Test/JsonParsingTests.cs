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
    }
}