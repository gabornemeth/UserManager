using FastEndpoints;

namespace UserManager.Endpoints.Summary
{
    public class DeleteUserSummary : Summary<DeleteUserEndpoint>
    {
        public DeleteUserSummary()
        {
            Summary = "Removing a user";
            Description = "You can remove the user having the specified identifier.";
            Response(StatusCodes.Status204NoContent, "The user has been deleted.");
            Response(StatusCodes.Status404NotFound, "Invalid identifier has been specified.");
        }
    }
}
