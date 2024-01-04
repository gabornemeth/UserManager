using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using UserManager.Contracts.Requests;
using UserManager.Endpoints;
using UserManager.Models;

namespace UserManager.Test.Endpoints
{
    public class UpdateUserEndpointTests : UserEndpointTests<UpdateUserEndpoint>
    {
        [Fact]
        public void UseHttpPatch() => ShouldUseHttpPatch();

        [Fact]
        public void DoesNotAllowAnonymousAccess() => ShouldAllowAnonymous(false);

        [Fact]
        public async Task CannotFind_Failure()
        {
            UserService.Setup(srv => srv.Get(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            var patchRequest = new UpdateUserRequest();
            await Endpoint.HandleAsync(patchRequest, CancellationToken.None);

            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task CannotUpdate_Failure()
        {
            var id = "2";
            UserService.Setup(srv => srv.Get(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Id = id });
            UserService.Setup(srv => srv.Update(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var patchRequest = new UpdateUserRequest { Id = id };
            await Endpoint.HandleAsync(patchRequest, CancellationToken.None);

            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task InvalidUpdate_BadRequest()
        {
            var id = "2";
            UserService.Setup(srv => srv.Get(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Id = id });
            UserService.Setup(srv => srv.Update(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var jsonPatch = """{ "id": "2", "update": [ { "op": "replace", "path": "taxId", "value": "ABCD1234" } ] }""";
            var patchRequest = JsonConvert.DeserializeObject<UpdateUserRequest>(jsonPatch);

            Assert.NotNull(patchRequest); ;

            await Endpoint.HandleAsync(patchRequest, CancellationToken.None);

            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }


        [Fact]
        public async Task CanUpdate_Success()
        {
            var id = "2";
            UserService.Setup(srv => srv.Get(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Id = id });
            UserService.Setup(srv => srv.Update(It.Is<User>(u => u.Id == id), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var patchRequest = new UpdateUserRequest { Id = id };
            patchRequest.Update.Add(u => u.Name, "John Updated");
            patchRequest.Update.Add(u => u.Email, "john.updated@company.net");
            await Endpoint.HandleAsync(patchRequest, CancellationToken.None);

            Func<User, bool> updateVerifier = u => u.Id == id && u.Name == "John Updated" && u.Email == "john.updated@company.net";

            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
            UserService.Verify(srv => srv.Update(It.Is<User>(u => updateVerifier(u)), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
