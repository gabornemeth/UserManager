using Microsoft.AspNetCore.Http;
using UserManager.Contracts.Requests;
using UserManager.Endpoints;
using UserManager.Models;

namespace UserManager.Test.Endpoints
{
    public class PartialUpdateUserEndpointTests : UserEndpointTests<PartialUpdateUserEndpoint>
    {
        [Fact]
        public async Task CannotFind_Failure()
        {
            UserService.Setup(srv => srv.Get(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            var patchRequest = new PartialUpdateUserRequest();
            await Endpoint.HandleAsync(patchRequest, CancellationToken.None);

            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task CannotUpdate_Failure()
        {
            UserService.Setup(srv => srv.Get(2, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Id = 2 });
            UserService.Setup(srv => srv.Update(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var patchRequest = new PartialUpdateUserRequest { Id = 2 };
            await Endpoint.HandleAsync(patchRequest, CancellationToken.None);

            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task CanUpdate_Success()
        {
            UserService.Setup(srv => srv.Get(2, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Id = 2 });
            UserService.Setup(srv => srv.Update(It.Is<User>(u => u.Id == 2), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var patchRequest = new PartialUpdateUserRequest { Id = 2 };
            patchRequest.Add(u => u.Name, "John Updated");
            patchRequest.Add(u => u.Email, "john.updated@company.net");
            await Endpoint.HandleAsync(patchRequest, CancellationToken.None);

            Func<User, bool> updateVerifier = u => u.Id == 2 && u.Name == "John Updated" && u.Email == "john.updated@company.net";

            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
            UserService.Verify(srv => srv.Update(It.Is<User>(u => updateVerifier(u)), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
