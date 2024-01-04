using UserManager.Models;
using UserManager.Services;
using UserManager.Validators;

namespace UserManager.Test.Services
{
    public class UserServiceTestsWithValidator : UserServiceTestsBase
    {
        public UserServiceTestsWithValidator() : base(new UserValidator())
        {
        }

        [Fact]
        public async Task CreateInvalidUser_Failure()
        {
            // arrange
            var invalidUser = TestHelper.GetValidUser().Invalidate();
            Repository.Setup(repo => repo.Get(invalidUser.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            // act
            var result = await UserService.Create(invalidUser);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task CreateValidUser_Succeed()
        {
            // arrange
            var validUser = TestHelper.GetValidUser();
            Repository.Setup(repo => repo.Get(validUser.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            // act
            var result = await UserService.Create(validUser);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateInvalidUser_Failure()
        {
            // arrange
            var invalidUser = TestHelper.GetValidUser().Invalidate();
            Repository.Setup(repo => repo.Get(invalidUser.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(invalidUser);

            // act
            var result = await UserService.Update(invalidUser);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateWithoutId_Failure()
        {
            // arrange
            var invalidUser = TestHelper.GetValidUser(u => u.Id = null);

            // act
            var result = await UserService.Update(invalidUser);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateValidUser_Success()
        {
            // arrange
            var validUser = TestHelper.GetValidUser();
            Repository.Setup(repo => repo.Get(validUser.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(validUser);
            validUser.Name = $"Changed {validUser.Name}";

            // act
            var result = await UserService.Update(validUser);

            // assert
            result.Should().BeTrue();
        }
    }
}
