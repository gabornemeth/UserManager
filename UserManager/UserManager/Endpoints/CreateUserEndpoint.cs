using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using UserManager.Contracts.Dtos;
using UserManager.Contracts.Requests;
using UserManager.Contracts.Responses;
using UserManager.Models;
using UserManager.Services;

namespace UserManager.Endpoints
{
    [HttpPost("users")]
    [AllowAnonymous]
    public partial class CreateUserEndpoint : Endpoint<CreateUserRequest, CreateUserResponse>
    {
        private readonly UserEndpointServices _services;

        public CreateUserEndpoint(IUserService userService, IMapper mapper)
        {
            _services = new UserEndpointServices(userService, mapper);
        }

        public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
        {
            var userToCreate = _services.Mapper.Map<User>(req);
            var created = await _services.UserService.Create(userToCreate);
            if (created)
            {
                await SendOkAsync(new CreateUserResponse(userToCreate.Id), ct);
            }
            else
            {
                await SendErrorsAsync(cancellation: ct);
            }
        }
    }
}
