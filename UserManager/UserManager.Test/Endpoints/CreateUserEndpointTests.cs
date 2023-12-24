using Microsoft.AspNetCore.Http;
using UserManager.Contracts.Dtos;
using UserManager.Endpoints;
using UserManager.Models;

namespace UserManager.Test.Endpoints
{
    public class CreateUserEndpointTests : UserEndpointTests<CreateUserEndpoint>
    {
        [Fact]
        public async Task CreateNewUser_Success()
        {
            UserService.Setup(srv => srv.Create(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            await Endpoint.HandleAsync(new UserDto { Id = 1 }, CancellationToken.None);

            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task CreateUser_Failure()
        {
            UserService.Setup(srv => srv.Create(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            await Endpoint.HandleAsync(new UserDto { Id = 1 }, CancellationToken.None);

            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}
