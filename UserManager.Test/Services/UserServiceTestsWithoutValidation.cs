using UserManager.Models;
using UserManager.Services;

namespace UserManager.Test.Services
{
    public class UserServiceTestsWithoutValidation : UserServiceTestsBase
    {
        public UserServiceTestsWithoutValidation() : base(null)
        {
        }

        [Fact]
        public async Task CreateAlreadyExistingUser_Failure()
        {
            // arrange
            var id = "8";
            Repository.Setup(repo => repo.Get(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Id = id });

            // act
            var result = await UserService.Create(new User { Id = id });

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task CreateNewUser_Succeess()
        {
            // arrange
            var id = "9";

            // act
            var result = await UserService.Create(new User { Id = id });

            // assert
            result.Should().BeTrue();
            Repository.Verify(r => r.Create(It.Is<User>(usr => usr.Id == id), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task DeleteNonExistingUser_Failure()
        {
            // act
            var result = await UserService.Delete("8");

            // assert
            result.Should().BeFalse();

        }

        [Fact]
        public async Task DeleteExistingUser_Success()
        {
            // arrange
            var testId = "8";
            Repository.Setup(repo => repo.Get(testId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Id = testId });
            Repository.Setup(repo => repo.Delete(It.Is<User>(usr => usr.Id == testId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // act
            var result = await UserService.Delete(testId);

            // assert
            result.Should().BeTrue();
            Repository.Verify(r => r.Delete(It.Is<User>(usr => usr.Id == testId), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task UpdateNonExistingUser_Failure()
        {
            // arrange
            var id = "8";
            Repository.Setup(repo => repo.Get(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            // act
            var result = await UserService.Update(new User { Id = id });

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateExistingUser_Success()
        {
            // arrange
            var id = "8";
            Repository.Setup(repo => repo.Get(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Id = id });

            // act
            var result = await UserService.Update(new User { Id = id });

            // assert
            result.Should().BeTrue();
            Repository.Verify(r => r.Update(It.Is<User>(usr => usr.Id == id), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task GetUsers_NoUserExist()
        {
            // act
            var users = await UserService.GetAll();

            // assert
            users.Should().BeEmpty();
        }

        [Fact]
        public async Task GetUsers()
        {
            // arrange
            Repository.Setup(repo => repo.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync([new User(), new User()]);

            // act
            var users = await UserService.GetAll();

            // assert
            users.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetUser_NonExisting()
        {
            // act
            var user = await UserService.Get("10");

            // assert
            user.Should().BeNull();
        }

        [Fact]
        public async Task GetUser_Existing()
        {
            var id = "10";
            Repository.Setup(repo => repo.Get(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Id = id, Name = "Test User" });
            // act
            var user = await UserService.Get(id);

            // assert
            Assert.NotNull(user);
            user.Name.Should().Be("Test User");
        }
    }
}
