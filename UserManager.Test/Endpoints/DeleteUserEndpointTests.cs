using Microsoft.AspNetCore.Http;
using UserManager.Endpoints;

namespace UserManager.Test.Endpoints
{
    public class DeleteUserEndpointTests : UserEndpointTests<DeleteUserEndpoint>
    {
        protected override object[] GetEndPointConstructorArguments() => [UserService.Object];

        [Fact]
        public void UseHttpDelete() => ShouldUseHttpDelete();

        [Fact]
        public void DoesNotAllowAnonymousAccess() => ShouldAllowAnonymous(false);

        [Fact]
        public async Task DeleteNonExistingUser_NotFound()
        {
            // setup
            var userId = "8";
            UserService.Setup(srv => srv.Delete(userId, It.IsAny<CancellationToken>())).ReturnsAsync(false);

            // act
            await Endpoint.HandleAsync(new Contracts.Requests.DeleteUserRequest { Id = userId }, CancellationToken.None);

            // assert
            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task DeleteExistingUser_Success()
        {
            // setup
            var userId = "8";
            UserService.Setup(srv => srv.Delete(userId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            // act
            await Endpoint.HandleAsync(new Contracts.Requests.DeleteUserRequest { Id = userId }, CancellationToken.None);

            // assert
            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }
    }
}
