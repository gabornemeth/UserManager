using UserManager.Endpoints;
using UserManager.Models;

namespace UserManager.Test.Endpoints
{
    public class GetAllUsersEndpointTests : UserEndpointTests<GetAllUsersEndpoint>
    {
        [Fact]
        public async Task NoUsers()
        {
            // setup
            UserService.Setup(s => s.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(Array.Empty<User>());

            // act
            await Endpoint.HandleAsync(CancellationToken.None);

            // assert
            Endpoint.HttpContext.Response.StatusCode.Should().Be(200);
            Endpoint.Response.Should().BeEmpty();
        }

        [Fact]
        public async Task UsersExist()
        {
            // setup
            UserService.Setup(s => s.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User[5]);

            // act
            await Endpoint.HandleAsync(CancellationToken.None);

            // assert
            Endpoint.HttpContext.Response.StatusCode.Should().Be(200);
            Endpoint.Response.Should().HaveCount(5);
        }
    }
}
