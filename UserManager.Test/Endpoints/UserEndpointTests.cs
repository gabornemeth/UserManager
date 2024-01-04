using AutoMapper;
using Microsoft.AspNetCore.Http;
using UserManager.Mappings;
using UserManager.Services;

namespace UserManager.Test.Endpoints
{
    public class UserEndpointTests<TUserEndpoint> where TUserEndpoint : BaseEndpoint
    {
        protected Mock<IUserService> UserService { get; }

        protected TUserEndpoint Endpoint { get; }

        protected IMapper Mapper { get; }

        protected UserEndpointTests()
        {
            UserService = new Mock<IUserService>();
            Mapper = TestHelper.CreateMapper();
            Endpoint = Factory.Create<TUserEndpoint>(AddTestServices, GetEndPointConstructorArguments());
        }

        protected virtual void AddTestServices(DefaultHttpContext ctx) { }

        protected virtual object[] GetEndPointConstructorArguments() => [UserService.Object, Mapper];

        protected void ShouldAllowAnonymous(bool allow)
        {
            Endpoint.Configure();
            if (allow)
            {
                Endpoint.Definition.AnonymousVerbs.Should().NotBeEmpty();
            }
            else
            {
                Endpoint.Definition.AnonymousVerbs.Should().BeNullOrEmpty();
            }
        }

        protected void ShouldUseHttpGet() => ShouldUseHttpVerb("GET");
        protected void ShouldUseHttpPost() => ShouldUseHttpVerb("POST");
        protected void ShouldUseHttpPut() => ShouldUseHttpVerb("PUT");
        protected void ShouldUseHttpPatch() => ShouldUseHttpVerb("PATCH");
        protected void ShouldUseHttpDelete() => ShouldUseHttpVerb("DELETE");

        protected void ShouldUseHttpVerb(string verb)
        {
            Endpoint.Configure();
            Endpoint.Definition.Verbs.Should().BeEquivalentTo([verb]);
        }
    }
}
