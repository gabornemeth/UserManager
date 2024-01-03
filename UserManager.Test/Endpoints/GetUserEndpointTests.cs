using Microsoft.AspNetCore.Http;
using UserManager.Endpoints;
using UserManager.Models;

namespace UserManager.Test.Endpoints
{
    public class GetUserEndpointTests : UserEndpointTests<GetUserEndpoint>
    {
        [Fact]
        public async Task GetNonExistingUser_Failure()
        {
            // arrange
            UserService
                .Setup(srv => srv.Get("8", It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            // act
            await Endpoint.HandleAsync(new Contracts.Requests.GetUserRequest { Id = "8" }, CancellationToken.None);

            // assert
            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task GetExistingUser_Success()
        {
            // arrange
            var id = "8";
            UserService
                .Setup(srv => srv.Get(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Id = id });
            
            //act
            await Endpoint.HandleAsync(new Contracts.Requests.GetUserRequest { Id = id }, CancellationToken.None);

            // assert
            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
