using FastEndpoints;
using UserManager.Contracts.Requests;
using UserManager.Models;
using UserManager.Services;

namespace UserManager.Endpoints
{
    public class ReplaceUserEndpoint : Endpoint<ReplaceUserRequest>
    {
        private readonly UserEndpointServices _services;

        public ReplaceUserEndpoint(IUserService userService, IMapper mapper)
        {
            _services = new UserEndpointServices(userService, mapper);
        }

        public override void Configure()
        {
            Put("users/{id}");
            Policy(p => p.HasScope(Scopes.Write));
        }

        public override async Task HandleAsync(ReplaceUserRequest req, CancellationToken ct)
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
