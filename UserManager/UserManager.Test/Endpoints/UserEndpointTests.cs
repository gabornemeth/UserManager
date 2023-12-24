using AutoMapper;
using UserManager.Mappings;
using UserManager.Services;

namespace UserManager.Test.Endpoints
{
    public class UserEndpointTests<TUserEndpoint> where TUserEndpoint : class, IEndpoint
    {
        private readonly IMapper _mapper;

        protected Mock<IUserService> UserService { get; }

        protected TUserEndpoint Endpoint { get; }

        protected UserEndpointTests()
        {
            UserService = new Mock<IUserService>();
            _mapper = new MapperConfiguration(config => config.AddProfile<UserProfile>()).CreateMapper();
            Endpoint = Factory.Create<TUserEndpoint>(GetEndPointConstructorArguments());
        }

        protected virtual object[] GetEndPointConstructorArguments() => [UserService.Object, _mapper];
    }
}
