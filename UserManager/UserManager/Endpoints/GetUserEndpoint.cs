using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using UserManager.Contracts.Dtos;
using UserManager.Contracts.Requests;
using UserManager.Services;
using IMapper = AutoMapper.IMapper;

namespace UserManager.Endpoints
{
    [HttpGet("users/{id:int}")]
    [AllowAnonymous]
    public class GetUserEndpoint : Endpoint<GetUserRequest, UserDto?>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetUserEndpoint(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
        {
            var user = await _userService.Get(req.Id);
            if (user == null)
            {
                await SendNotFoundAsync(cancellation: ct);
            }
         
            await SendOkAsync(_mapper.Map<UserDto>(user), cancellation: ct);
        }
    }
}
