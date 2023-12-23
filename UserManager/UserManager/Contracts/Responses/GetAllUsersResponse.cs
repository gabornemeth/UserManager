using UserManager.Dtos;

namespace UserManager.Contracts.Responses
{
    public class GetAllUsersResponse
    {
        public IEnumerable<User> Users { get; set; } = Enumerable.Empty<User>();
    }
}
