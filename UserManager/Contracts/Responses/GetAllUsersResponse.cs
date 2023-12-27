using UserManager.Contracts.Dtos;

namespace UserManager.Contracts.Responses
{
    public class GetAllUsersResponse : List<UserDto>
    {
        public GetAllUsersResponse(IEnumerable<UserDto> users) : base(users) { }
    }
}
