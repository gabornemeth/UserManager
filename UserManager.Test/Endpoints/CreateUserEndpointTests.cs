using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using UserManager.Contracts.Requests;
using UserManager.Contracts.Responses;
using UserManager.Endpoints;
using UserManager.Models;

namespace UserManager.Test.Endpoints
{
    public class CreateUserEndpointTests : UserEndpointTests<CreateUserEndpoint>
    {
        [Fact]
        public void UseHttpPost() => ShouldUseHttpPost();

        [Fact]
        public void DoesNotAllowAnonymousAccess() => ShouldAllowAnonymous(false);

        [Fact]
        public async Task CreateNewUser_Success()
        {
            UserService.Setup(srv => srv.Create(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Returns<User, CancellationToken>((user, cancellation) =>
                {
                    user.Id = "11";
                    return Task.FromResult(true);
                });

            await Endpoint.HandleAsync(new CreateUserRequest(), CancellationToken.None);

            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status201Created);
            Endpoint.HttpContext.Response.Headers.Location.Should().BeEquivalentTo("somewhere/11");
        }

        protected override void AddTestServices(DefaultHttpContext ctx)
        {
            ctx.AddTestServices(services =>
            {
                services.AddServicesForUnitTesting();

                var linkGenerator = new Mock<LinkGenerator>();
                linkGenerator.Setup(x => x.GetPathByAddress(It.IsAny<string>(), It.IsAny<RouteValueDictionary>(), It.IsAny<PathString>(), It.IsAny<FragmentString>(), It.IsAny<LinkOptions?>()))
                    .Returns<string, RouteValueDictionary, PathString, FragmentString, LinkOptions?>(
                    (address, values, pathBase, fragment, options) =>
                    {
                        return $"somewhere/{values[nameof(CreateUserResponse.Id)]}";
                    });
                services.AddSingleton(linkGenerator.Object);
            });
        }

        [Fact]
        public async Task CreateUser_Failure()
        {
            UserService.Setup(srv => srv.Create(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            await Endpoint.HandleAsync(new CreateUserRequest(), CancellationToken.None);

            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}
