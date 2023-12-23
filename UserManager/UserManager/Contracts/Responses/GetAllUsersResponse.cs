using UserManager.Dtos;

namespace UserManager.Contracts.Responses
{
    public class GetAllUsersResponse : List<User>
    {
        public GetAllUsersResponse(IEnumerable<User> users) : base(users) { }
    }
}
