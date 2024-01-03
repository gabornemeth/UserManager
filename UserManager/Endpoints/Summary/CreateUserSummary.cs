using FastEndpoints;
using UserManager.Contracts.Requests;
using UserManager.Contracts.Responses;

namespace UserManager.Endpoints.Summaries
{
    public class CreateUserSummary : Summary<CreateUserEndpoint>
    {
        public CreateUserSummary()
        {
            Summary = "Creating a new user";
            var users = SampleData.GetUsers<CreateUserRequest>();
            ExampleRequest = users.First();
            Response(StatusCodes.Status201Created, "New user has been created.",
                example: new CreateUserResponse("1234-abcd", users.First().Name));
            Response(StatusCodes.Status400BadRequest, "Invalid user details have been specified");
            Response(StatusCodes.Status401Unauthorized, "Unauthorized access. You have to supply a valid JWT bearer token");
        }
    }
}
