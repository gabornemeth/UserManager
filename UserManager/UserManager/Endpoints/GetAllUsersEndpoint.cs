using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using UserManager.Contracts.Dtos;
using UserManager.Contracts.Responses;
using UserManager.Services;

namespace UserManager.Endpoints
{
    [HttpGet("users")]
    [AllowAnonymous]
    public class GetAllUsersEndpoint : EndpointWithoutRequest<GetAllUsersResponse>
    {
        private readonly IUserService _userService;
        private readonly AutoMapper.IMapper _mapper;

        public GetAllUsersEndpoint(IUserService userService, AutoMapper.IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var users = await _userService.GetAll(ct);
            await SendOkAsync(new GetAllUsersResponse(_mapper.Map<IEnumerable<UserDto>>(users)), cancellation: ct);
        }
    }
}
