using FastEndpoints;
using UserManager.Contracts.Requests;
using UserManager.Contracts.Responses;
using UserManager.Models;
using UserManager.Services;

namespace UserManager.Endpoints
{
    public class CreateUserEndpoint : Endpoint<CreateUserRequest, CreateUserResponse>
    {
        private readonly UserEndpointServices _services;

        public CreateUserEndpoint(IUserService userService, IMapper mapper)
        {
            _services = new UserEndpointServices(userService, mapper);
        }

        public override void Configure()
        {
            Post("users");
            Permissions(Scopes.Write);
        }

        public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
        {
            var userToCreate = _services.Mapper.Map<User>(req);
            var created = await _services.UserService.Create(userToCreate);
            if (created)
            {
                var response = new CreateUserResponse(userToCreate.Id, userToCreate.Name);
                await SendCreatedAtAsync<GetUserEndpoint>(new { Id = response.Id }, response, cancellation: ct, generateAbsoluteUrl: false);
            }
            else
            {
                await SendErrorsAsync(cancellation: ct);
            }
        }
    }
}
