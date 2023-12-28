using UserManager.Models;

namespace UserManager.Test
{
    internal static class ContentHelper
    {
        public static User GetValidUser(Action<User>? applyChanges = null)
        {
            var user = new User
            {
                Id = 1,
                Name = "John Doe",
                UserName = "john.doe",
                Email = "john.doe@mycompany.net",
                Address = new Address
                {
                    City = "New Jersey",
                    ZipCode = "NJ 07307",
                    Street = "Bowers street",
                    GeoLocation = new Location(40.748004f, -74.050169f)
                },
                Company = new Company
                {
                    Name = "Flex LTD",
                    CatchPhrase = "Impossible is where breakthrough begins",
                    BusinessServices = "Electronics contract manufacturer."
                }
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
