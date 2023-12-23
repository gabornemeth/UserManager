using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using UserManager.Contracts.Responses;
using UserManager.Services;

namespace UserManager.Endpoints
{
    [HttpGet("users")]
    [AllowAnonymous]
    public class GetAllUsersEndpoint : EndpointWithoutRequest<GetAllUsersResponse>
    {
        private readonly IUserService _userService;

        public GetAllUsersEndpoint(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var users = await _userService.GetAll(ct);
            await SendOkAsync(new GetAllUsersResponse(users), cancellation: ct);
        }
    }
}
