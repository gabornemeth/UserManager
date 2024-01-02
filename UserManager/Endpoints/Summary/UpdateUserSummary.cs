using FastEndpoints;
using Newtonsoft.Json;
using UserManager.Contracts.Requests;

namespace UserManager.Endpoints.Summary
{
    public class UpdateUserSummary : Summary<UpdateUserEndpoint>
    {
        public UpdateUserSummary()
        {
            Summary = "Updating a user";
            Description = "You can update the value of any property of the user. Only the specified properties are changed.\n" +
                "";
            var jsonRequest = """
                {
                    "id": 12,
                    update:
                    [
                        {
                            "op": "replace",
                            "path": "/website",
                            "value": "www.mywebsite.com"
                        },
                        {
                            "op": "add", "path": "/address", 
                            "value":
                            {
                                "city": "Zalaegerszeg",
                                "zipCode": 8900,
                                "street": "Kossuth u. 39",
                                "suite": "ground",
                                "geo": { "lat": "46.839361", "lng": "16.845722" } 
                            } 
                        }
                    ]
                }
                """;
            ExampleRequest = JsonConvert.DeserializeObject<PartialUpdateUserRequest>(jsonRequest);
            Response(StatusCodes.Status200OK, "User has been updated.");
            Response(StatusCodes.Status404NotFound, "The specified user has not been found.");
            Response(StatusCodes.Status400BadRequest, "Invalid modification request.");
            Response(StatusCodes.Status401Unauthorized, "Unauthorized access. You have to supply a valid JWT bearer token");
        }
    }
}
