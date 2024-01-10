using AutoMapper;
using UserManager.Contracts.Dtos;
using UserManager.Mappings;
using UserManager.Models;

namespace UserManager.Test
{
    internal static class TestHelper
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(config => config.AddProfile<UserProfile>());
            return config.CreateMapper();
        }

        public static TUserDto GetValidUserDto<TUserDto>(Action<TUserDto>? applyChanges = null) where TUserDto : UserDtoBase, new()
        {
            var mapper = CreateMapper();
            var user = GetValidUser();
            var userDto = mapper.Map<TUserDto>(user);
            applyChanges?.Invoke(userDto);

            return userDto;
        }

        public static User GetValidUser(Action<User>? applyChanges = null)
        {
            var user = new User
            {
                Id = "1",
                Name = "John Doe",
                UserName = "john.doe",
                Email = "john.doe@mycompany.net",
                Address = new Address
                {
                    City = "New Jersey",
                    ZipCode = "NJ 07307",
                    Street = "Bowers street",
                    Geolocation = new Location(40.748004f, -74.050169f)
                },
                Company = new Company
                {
                    Name = "Flex LTD",
                    CatchPhrase = "Impossible is where breakthrough begins",
                    BusinessServices = "Electronics contract manufacturer."
                },
                Phone = "123-456-789",
                Website = "https://www.mycompany.net"
            };
            applyChanges?.Invoke(user);
            return user;
        }

        public static User Invalidate(this User user)
        {
            user.UserName = "";
            return user;
        }
    }
}
