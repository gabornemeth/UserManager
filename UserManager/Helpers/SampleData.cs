using MongoDB.Bson;
using Newtonsoft.Json;
using System.Reflection;
using UserManager.Contracts.Dtos;

namespace UserManager.Helpers
{
    internal class SampleData
    {
        public static IEnumerable<UserDto> GetUsers() => GetUsers<UserDto>();

        public static IEnumerable<TUserDto> GetUsers<TUserDto>() where TUserDto : UserDtoBase
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("UserManager.sample.json")!;
            using var reader = new StreamReader(stream);
            var sampleData = reader.ReadToEnd(); 
            var parsedUsers = JsonConvert.DeserializeObject<TUserDto[]>(sampleData)!;
            // change the identifiers to some better string representation to be more consistent with the demo data too
            foreach (var user in parsedUsers.OfType<UserDto>())
            {
                user.Id = ObjectId.GenerateNewId().ToString();
            }

            return parsedUsers;
        }
    }
}
