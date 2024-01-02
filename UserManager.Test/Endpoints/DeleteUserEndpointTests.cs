using Microsoft.AspNetCore.Http;
using UserManager.Endpoints;

namespace UserManager.Test.Endpoints
{
    public class DeleteUserEndpointTests : UserEndpointTests<DeleteUserEndpoint>
    {
        protected override object[] GetEndPointConstructorArguments() => [UserService.Object];
        
        [Fact]
        public async Task DeleteNonExistingUser_Failure()
        {
            // setup
            UserService.Setup(srv => srv.Delete(8, It.IsAny<CancellationToken>())).ReturnsAsync(false);

            // act
            await Endpoint.HandleAsync(new Contracts.Requests.DeleteUserRequest { Id = 8 }, CancellationToken.None);

            // assert
            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task DeleteExistingUser_Success()
        {
            // setup
            UserService.Setup(srv => srv.Delete(8, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            // act
            await Endpoint.HandleAsync(new Contracts.Requests.DeleteUserRequest { Id = 8 }, CancellationToken.None);

            // assert
            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
