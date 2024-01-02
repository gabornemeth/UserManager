using FastEndpoints;

namespace UserManager.Endpoints.Summary
{
    public class ReplaceUserSummary : Summary<ReplaceUserEndpoint>
    {
        public ReplaceUserSummary()
        {
            Summary = "Replacing the user";
            Description = "Replacing the user entirely with the specified properties";
            ExampleRequest = SampleData.GetUsers().First();
            Response(StatusCodes.Status200OK, "User has been updated.");
            Response(StatusCodes.Status404NotFound, "The specified user has not been found.");
            Response(StatusCodes.Status400BadRequest, "Invalid user details have been specified.");
            Response(StatusCodes.Status401Unauthorized, "Unauthorized access. You have to supply a valid JWT bearer token");
        }
    }
}
