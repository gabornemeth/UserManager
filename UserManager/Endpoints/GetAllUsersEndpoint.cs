using FastEndpoints;
using UserManager.Contracts.Dtos;
using UserManager.Contracts.Requests;
using UserManager.Contracts.Responses;
using UserManager.Services;

namespace UserManager.Endpoints
{
    public class GetAllUsersEndpoint : Endpoint<GetAllUsersRequest, GetAllUsersResponse>
    {
        private readonly UserEndpointServices _services;

        public GetAllUsersEndpoint(IUserService userService, IMapper mapper)
        {
            _services = new UserEndpointServices(userService, mapper);
        }

        public override void Configure()
        {
            Get("users");
            Policy(p => p.HasScope(Scopes.Read));
        }

        public override async Task HandleAsync(GetAllUsersRequest req, CancellationToken ct)
        {
            var allUsers = await _services.UserService.GetAll(ct);
            var users = allUsers.Skip((req.Page - 1) * req.PerPage).Take(req.PerPage);
            var mappedUsers = _services.Mapper.Map<IEnumerable<UserDto>>(users);
            await SendOkAsync(new GetAllUsersResponse(mappedUsers), cancellation: ct);
        }
    }
}
