using Microsoft.AspNetCore.Http;
using UserManager.Contracts.Requests;
using UserManager.Endpoints;
using UserManager.Models;

namespace UserManager.Test.Endpoints
{
    public class GetAllUsersEndpointTests : UserEndpointTests<GetAllUsersEndpoint>
    {
        [Fact]
        public void UseHttpGet() => ShouldUseHttpGet();

        [Fact]
        public void DoesNotAllowAnonymousAccess() => ShouldAllowAnonymous(false);

        [Fact]
        public async Task NoUsers()
        {
            // setup
            UserService.Setup(s => s.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(Array.Empty<User>());

            // act
            await Endpoint.HandleAsync(new GetAllUsersRequest(), CancellationToken.None);

            // assert
            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
            Endpoint.Response.Should().BeEmpty();
        }

        [Fact]
        public async Task UsersExist()
        {
            // setup
            UserService.Setup(s => s.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User[5]);

            // act
            await Endpoint.HandleAsync(new GetAllUsersRequest(), CancellationToken.None);

            // assert
            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
            Endpoint.Response.Should().HaveCount(5);
        }


        [Fact]
        public async Task Paging()
        {
            // setup
            var users = Enumerable.Range(1, 100).Select(idx => new User
            {
                Id = idx.ToString(),
                Name = $"User {idx}",
                UserName = $"user.{idx}"
            });
            UserService.Setup(s => s.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(users);

            // act
            await Endpoint.HandleAsync(new GetAllUsersRequest { Page = 3, PerPage = 20}, CancellationToken.None);

            // assert
            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
            Endpoint.Response.Should().HaveCount(20);
            Endpoint.Response.Should().BeEquivalentTo(users.Skip(40).Take(20));
        }

        [Fact]
        public async Task InvalidPagingConfiguration1()
        {
            // setup
            var users = Enumerable.Range(1, 100).Select(idx => new User
            {
                Id = idx.ToString(),
                Name = $"User {idx}",
                UserName = $"user.{idx}"
            });
            UserService.Setup(s => s.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(users);

            // act
            await Endpoint.HandleAsync(new GetAllUsersRequest { Page = -1, PerPage = 5 }, CancellationToken.None);

            // assert
            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
            Endpoint.Response.Should().HaveCount(5);
            Endpoint.Response.Should().BeEquivalentTo(users.Take(5));
        }

        [Fact]
        public async Task InvalidPagingConfiguration2()
        {
            // setup
            var users = Enumerable.Range(1, 100).Select(idx => new User
            {
                Id = idx.ToString(),
                Name = $"User {idx}",
                UserName = $"user.{idx}"
            });
            UserService.Setup(s => s.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(users);

            // act
            await Endpoint.HandleAsync(new GetAllUsersRequest { Page = 100, PerPage = 5 }, CancellationToken.None);

            // assert
            Endpoint.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
            Endpoint.Response.Should().HaveCount(0);
        }
    }
}
