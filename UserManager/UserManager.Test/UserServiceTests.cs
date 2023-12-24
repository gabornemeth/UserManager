using UserManager.Models;
using UserManager.Services;

namespace UserManager.Test
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _repository;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _repository = new Mock<IUserRepository>();
            _userService = new UserService(_repository.Object);
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
    }
}
