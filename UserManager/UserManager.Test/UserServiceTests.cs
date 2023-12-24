using AutoMapper;
using UserManager.Mappings;
using UserManager.Models;
using UserManager.Services;

namespace UserManager.Test
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _repository;
        private readonly IMapper _mapper;

        public UserServiceTests()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile<UserProfile>()).CreateMapper();
            _repository = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task AddAlreadyExistingUser_NotSucceed()
        {
            _repository.Setup(repo => repo.Get(8, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Id = 8 });
            var userService = new UserService(_repository.Object, _mapper);

            // act
            var result = await userService.Create(new User { Id = 8 });

            result.Should().BeFalse();
        }

        [Fact]
        public async Task AddNewUser_Succeess()
        {
            _repository.Setup(repo => repo.Get(8, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Id = 8 });
            var userService = new UserService(_repository.Object, _mapper);

            // act
            var result = await userService.Create(new User { Id = 9 });

            result.Should().BeTrue();
            _repository.Verify(r => r.Create(It.Is<User>(usr => usr.Id == 9), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
