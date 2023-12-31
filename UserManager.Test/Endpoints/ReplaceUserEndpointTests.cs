﻿using Microsoft.AspNetCore.Http;
using UserManager.Contracts.Requests;
using UserManager.Endpoints;
using UserManager.Models;

namespace UserManager.Test.Endpoints
{
    public class ReplaceUserEndpointTests : UserEndpointTests<ReplaceUserEndpoint>
    {
        [Fact]
        public void UseHttpPut() => ShouldUseHttpPut();

        [Fact]
        public void DoesNotAllowAnonymousAccess() => ShouldAllowAnonymous(false);

        [Fact]
        public async Task CannotUpdate_Failure()
        {
            UserService.Setup(srv => srv.Update(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            await Endpoint.HandleAsync(new ReplaceUserRequest { Id = "2", Name = "John New" }, CancellationToken.None);

            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task CanUpdate_Success()
        {
            UserService.Setup(srv => srv.Update(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            await Endpoint.HandleAsync(new ReplaceUserRequest { Id = "2", Name = "John New" }, CancellationToken.None);

            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
