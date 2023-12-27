using AutoMapper;
using Microsoft.AspNetCore.Http;
using UserManager.Mappings;
using UserManager.Services;

namespace UserManager.Test.Endpoints
{
    public class UserEndpointTests<TUserEndpoint> where TUserEndpoint : class, IEndpoint
    {
        protected Mock<IUserService> UserService { get; }

        protected TUserEndpoint Endpoint { get; }

        protected IMapper Mapper { get; }

        protected UserEndpointTests()
        {
            UserService = new Mock<IUserService>();
            Mapper = new MapperConfiguration(config => config.AddProfile<UserProfile>()).CreateMapper();
            Endpoint = Factory.Create<TUserEndpoint>(AddTestServices, GetEndPointConstructorArguments());
        }

        protected virtual void AddTestServices(DefaultHttpContext ctx) { }

        protected virtual object[] GetEndPointConstructorArguments() => [UserService.Object, Mapper];
    }
}
