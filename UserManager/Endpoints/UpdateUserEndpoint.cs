using FastEndpoints;
using UserManager.Contracts.Dtos;
using UserManager.Contracts.Requests;
using UserManager.Models;
using UserManager.Services;

namespace UserManager.Endpoints
{
    public class UpdateUserEndpoint : Endpoint<UpdateUserRequest>
    {
        private readonly UserEndpointServices _services;

        public UpdateUserEndpoint(IUserService userService, IMapper mapper)
        {
            _services = new UserEndpointServices(userService, mapper);
        }

        public override void Configure()
        {
            Patch("users/{id}");
            Policy(p => p.HasScope(Scopes.Write));
        }

        public override async Task HandleAsync(UpdateUserRequest req, CancellationToken ct)
        {
            var userToPatch = await _services.UserService.Get(req.Id, ct);
            if (userToPatch == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            try
            {
                var dtoToPatch = _services.Mapper.Map<UserDto>(userToPatch);
                req.Update.ApplyTo(dtoToPatch);
                userToPatch = _services.Mapper.Map<User>(dtoToPatch);
            }
            catch
            {
                await SendErrorsAsync(cancellation: ct);
                return;
            }

            var result = await _services.UserService.Update(userToPatch, ct);
            if (result == false)
            {
                await SendErrorsAsync(cancellation: ct);
                return;
            }

            await SendOkAsync(ct);
        }
    }
}
