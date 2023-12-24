using Newtonsoft.Json;
using UserManager.Dtos;

namespace UserManager.Helpers
{
    internal class SampleData
    {
        public static IEnumerable<UserDto> GetUsers()
        {
            var sampleData = File.ReadAllText("sample.json");
            return JsonConvert.DeserializeObject<UserDto[]>(sampleData) ?? [];
        }
    }
}
