using FastEndpoints;
using UserManager.Contracts.Dtos;

namespace UserManager.Endpoints.Summary
{
    public class GetUserSummary : Summary<GetUserEndpoint>
    {
        public GetUserSummary()
        {
            Summary = "Retrieving a specific user";
            Description = "Retrieving the user with the specified identifier.";
            Response<UserDto?>(StatusCodes.Status200OK, "User has been found.", example: SampleData.GetUsers().First());
            Response(StatusCodes.Status404NotFound, "User has not been found.");
            Response(StatusCodes.Status401Unauthorized, "Unauthorized access. You have to supply a valid JWT bearer token");
        }
    }
}
