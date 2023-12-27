using Microsoft.AspNetCore.JsonPatch;
using UserManager.Contracts.Dtos;

namespace UserManager.Contracts.Requests
{
    public class PartialUpdateUserRequest : JsonPatchDocument<UserDto>
    {

    }
}
