using UserManager.Dtos;
using UserManager.Endpoints;
using UserManager.Services;

namespace UserManager.Test
{
    public class GetAllUsersEndpointTests
    {
        [Fact]
        public async Task NoUsers()
        {
            // setup
            var userService = new Mock<IUserService>();
            userService.Setup(s => s.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(Array.Empty<UserDto>());
            var endpoint = Factory.Create<GetAllUsersEndpoint>(userService.Object);

            // act
            await endpoint.HandleAsync(CancellationToken.None);

            // assert
            endpoint.HttpContext.Response.StatusCode.Should().Be(200);
            endpoint.Response.Should().BeEmpty();
        }

        [Fact]
        public async Task UsersExist()
        {
            // setup
            var userService = new Mock<IUserService>();
            userService.Setup(s => s.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new UserDto[5]);
            var endpoint = Factory.Create<GetAllUsersEndpoint>(userService.Object);

            // act
            await endpoint.HandleAsync(CancellationToken.None);

            // assert
            endpoint.HttpContext.Response.StatusCode.Should().Be(200);
            endpoint.Response.Should().HaveCount(5);
        }
    }
}
