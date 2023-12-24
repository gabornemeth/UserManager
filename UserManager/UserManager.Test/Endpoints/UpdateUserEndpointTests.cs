using Microsoft.AspNetCore.Http;
using UserManager.Contracts.Dtos;
using UserManager.Endpoints;
using UserManager.Models;

namespace UserManager.Test.Endpoints
{
    public class UpdateUserEndpointTests : UserEndpointTests<UpdateUserEndpoint>
    {
        [Fact]
        public async Task CannotUpdate_Failure()
        {
            UserService.Setup(srv => srv.Update(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            await Endpoint.HandleAsync(new UserDto { Id = 2, Name = "New John" }, CancellationToken.None);

            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task CanUpdate_Success()
        {
            UserService.Setup(srv => srv.Update(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            await Endpoint.HandleAsync(new UserDto { Id = 2, Name = "New John" }, CancellationToken.None);

            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
