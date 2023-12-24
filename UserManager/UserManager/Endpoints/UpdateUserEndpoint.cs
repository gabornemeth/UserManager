using FastEndpoints;
using UserManager.Contracts.Dtos;
using UserManager.Models;
using UserManager.Services;

namespace UserManager.Endpoints
{
    [HttpPut("users")]
    public class UpdateUserEndpoint : Endpoint<UserDto>
    {
        private readonly UserEndpointServices _services;

        public UpdateUserEndpoint(IUserService userService, IMapper mapper)
        {
            _services = new UserEndpointServices(userService, mapper);
        }

        public override async Task HandleAsync(UserDto req, CancellationToken ct)
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
