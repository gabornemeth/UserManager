using Newtonsoft.Json;
using UserManager.Contracts.Dtos;

namespace UserManager.Helpers
{
    internal class SampleData
    {
        public static IEnumerable<UserDto> GetUsers() => GetUsers<UserDto>();

        public static IEnumerable<TUserDto> GetUsers<TUserDto>() where TUserDto : UserDtoWithoutIdentifier
        {
            var sampleData = File.ReadAllText("sample.json");
            return JsonConvert.DeserializeObject<TUserDto[]>(sampleData) ?? [];
        }
    }
}
