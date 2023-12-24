using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using UserManager.Contracts.Dtos;
using UserManager.Contracts.Requests;
using UserManager.Services;

namespace UserManager.Endpoints
{
    [HttpGet("users/{id:int}")]
    [AllowAnonymous]
    public class GetUserEndpoint : Endpoint<GetUserRequest, UserDto?>
    {
        private readonly IUserService _userService;

        public GetUserEndpoint(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
        {
            var user = await _userService.Get(req.Id);
            if (user == null)
            {
                await SendNotFoundAsync(cancellation: ct);
            }
         
            await SendOkAsync(user, cancellation: ct);
        }
    }
}
