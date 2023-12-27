using FastEndpoints;
using UserManager.Contracts.Dtos;
using UserManager.Contracts.Requests;
using UserManager.Models;
using UserManager.Services;

namespace UserManager.Endpoints
{
    [HttpPut("users")]
    public class UpdateUserEndpoint : Endpoint<UpdateUserRequest>
    {
        private readonly UserEndpointServices _services;

        public UpdateUserEndpoint(IUserService userService, IMapper mapper)
        {
            _services = new UserEndpointServices(userService, mapper);
        }

        public override async Task HandleAsync(UpdateUserRequest req, CancellationToken ct)
        {
            var userToUpdate = _services.Mapper.Map<User>(req);
            var updated = await _services.UserService.Update(userToUpdate);
            if (updated)
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
