using FastEndpoints;
using UserManager.Contracts.Requests;
using UserManager.Services;

namespace UserManager.Endpoints
{
    [HttpDelete("users/{id}")]
    public class DeleteUserEndpoint : Endpoint<DeleteUserRequest>
    {
        private readonly IUserService _userService;

        public DeleteUserEndpoint(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
        {
            var success = await _userService.Delete(req.Id);
            if (success)
            {
                await SendOkAsync(ct);
            }
            else
            {
                await SendErrorsAsync(cancellation: ct);
            }
        }
    }
}
