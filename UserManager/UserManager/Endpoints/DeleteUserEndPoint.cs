using FastEndpoints;
using UserManager.Contracts.Requests;
using UserManager.Services;

namespace UserManager.Endpoints
{
    [HttpDelete("users/{id}")]
    public class DeleteUserEndPoint : Endpoint<DeleteUserRequest>
    {
        private readonly IUserService _userService;

        public DeleteUserEndPoint(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
        {
            await _userService.Delete(req.Id);
            await SendOkAsync(ct);
        }
    }
}
