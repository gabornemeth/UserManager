using FastEndpoints;
using UserManager.Contracts.Dtos;
using UserManager.Contracts.Requests;
using UserManager.Services;

namespace UserManager.Endpoints
{
    public class GetUserEndpoint : Endpoint<GetUserRequest, UserDto?>
    {
        private readonly UserEndpointServices _services;

        public GetUserEndpoint(IUserService userService, IMapper mapper)
        {
            _services = new UserEndpointServices(userService, mapper);
        }

        public override void Configure()
        {
            Get("users/{id}");
            Policy(p => p.HasScope(Scopes.Read));
        }

        public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
        {
            var user = await _services.UserService.Get(req.Id);
            if (user == null)
            {
                await SendNotFoundAsync(cancellation: ct);
            }
            else
            {
                await SendOkAsync(_services.Mapper.Map<UserDto>(user), cancellation: ct);
            }
        }
    }
}
