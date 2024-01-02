using FastEndpoints;

namespace UserManager.Endpoints.Summary
{
    public class DeleteUserSummary : Summary<DeleteUserEndpoint>
    {
        public DeleteUserSummary()
        {
            Summary = "Removing a user";
            Description = "You can remove the user having the specified identifier.";
            Response(StatusCodes.Status200OK, "The user has been deleted.");
            Response(StatusCodes.Status400BadRequest, "Invalid identifier has been specified.");
            Response(StatusCodes.Status401Unauthorized, "Unauthorized access. You have to supply a valid JWT bearer token");
        }
    }
}
