using UserManager.Services;

namespace UserManager.Endpoints
{
    internal class UserEndpointServices(IUserService userService, IMapper mapper)
    {
        public IUserService UserService => userService;
        public IMapper Mapper => mapper;
    }
}
