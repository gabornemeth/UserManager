using UserManager.Models;
using UserManager.Services;

namespace UserManager.Test.Services
{
    public class UserServiceTestsWithoutValidation
    {
        private readonly Mock<IUserRepository> _repository;
        private readonly UserService _userService;

        public UserServiceTestsWithoutValidation()
        {
            _repository = new Mock<IUserRepository>();
            _userService = new UserService(_repository.Object, null);
        }

        [Fact]
        public async Task CreateAlreadyExistingUser_NotSucceed()
        {
            // arrange
            _repository.Setup(repo => repo.Get(8, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Id = 8 });

            // act
            var result = await _userService.Create(new User { Id = 8 });

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task CreateNewUser_Succeess()
        {
            // arrange
            _repository.Setup(repo => repo.Get(8, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Id = 8 });

            // act
            var result = await _userService.Create(new User { Id = 9 });

            // assert
            result.Should().BeTrue();
            _repository.Verify(r => r.Create(It.Is<User>(usr => usr.Id == 9), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task DeleteNonExistingUser_Failure()
        {
            // act
            var result = await _userService.Delete(8);

            // assert
            result.Should().BeFalse();

        }

        [Fact]
        public async Task DeleteExistingUser_Success()
        {
            // arrange
            _repository.Setup(repo => repo.Get(8, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Id = 8 });
            _repository.Setup(repo => repo.Delete(It.Is<User>(usr => usr.Id == 8), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // act
            var result = await _userService.Delete(8);

            // assert
            result.Should().BeTrue();
            _repository.Verify(r => r.Delete(It.Is<User>(usr => usr.Id == 8), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task UpdateNonExistingUser_Failure()
        {
            // arrange
            _repository.Setup(repo => repo.Get(8, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            // act
            var result = await _userService.Update(new User { Id = 8 });

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateExistingUser_Success()
        {
            // arrange
            _repository.Setup(repo => repo.Get(8, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Id = 8 });

            // act
            var result = await _userService.Update(new User { Id = 8 });

            // assert
            result.Should().BeTrue();
            _repository.Verify(r => r.Update(It.Is<User>(usr => usr.Id == 8), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task GetUsers_NoUserExist()
        {
            // act
            var users = await _userService.GetAll();

            // assert
            users.Should().BeEmpty();
        }

        [Fact]
        public async Task GetUsers()
        {
            // arrange
            _repository.Setup(repo => repo.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync([new User(), new User()]);

            // act
            var users = await _userService.GetAll();

            // assert
            users.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetUser_NonExisting()
        {
            // act
            var user = await _userService.Get(10);

            // assert
            user.Should().BeNull();
        }

        [Fact]
        public async Task GetUser_Existing()
        {
            _repository.Setup(repo => repo.Get(10, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Id = 10, Name = "Test User" });
            // act
            var user = await _userService.Get(10);

            // assert
            user.Should().NotBeNull();
            user!.Name.Should().Be("Test User");
        }
    }
}
