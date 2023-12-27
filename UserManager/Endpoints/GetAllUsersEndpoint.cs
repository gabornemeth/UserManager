using FastEndpoints;
using UserManager.Contracts.Dtos;
using UserManager.Contracts.Responses;
using UserManager.Services;

namespace UserManager.Endpoints
{
    public class GetAllUsersEndpoint : EndpointWithoutRequest<GetAllUsersResponse>
    {
        private readonly UserEndpointServices _services;

        public override void Configure()
        {
            Get("users");
            //Roles("poweruser", "admin");
        }

        public GetAllUsersEndpoint(IUserService userService, IMapper mapper)
        {
            _services = new UserEndpointServices(userService, mapper);
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var users = await _services.UserService.GetAll(ct);
            var mappedUsers = _services.Mapper.Map<IEnumerable<UserDto>>(users);
            await SendOkAsync(new GetAllUsersResponse(mappedUsers), cancellation: ct);
        }
    }
}
