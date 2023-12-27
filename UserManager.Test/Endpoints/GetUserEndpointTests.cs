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
                .Setup(srv => srv.Get(8, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            // act
            await Endpoint.HandleAsync(new Contracts.Requests.GetUserRequest { Id = 8 }, CancellationToken.None);

            // assert
            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task GetExistingUser_Success()
        {
            // arrange
            UserService
                .Setup(srv => srv.Get(8, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Id = 8 });
            
            //act
            await Endpoint.HandleAsync(new Contracts.Requests.GetUserRequest { Id = 8 }, CancellationToken.None);

            // assert
            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
