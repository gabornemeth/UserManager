using FastEndpoints;

namespace UserManager.Endpoints.Summary
{
    public class GetAllUsersSummary : Summary<GetAllUsersEndpoint>
    {
        public GetAllUsersSummary()
        {
            Summary = "Retrieving all users";
            Response(StatusCodes.Status200OK, example: SampleData.GetUsers());
            Response(StatusCodes.Status401Unauthorized, "Unauthorized access. You have to supply a valid JWT bearer token");
        }
    }
}
