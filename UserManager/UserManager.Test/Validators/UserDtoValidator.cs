using FluentValidation.Results;
using UserManager.Contracts.Dtos;
using UserManager.Validators;

namespace UserManager.Test.Validators
{
    public class UserDtoValidatorTest
    {
        [Fact]
        public void NoName_Failure()
        {
            var result = ShouldFail(new UserDto { UserName = "john.doe" });
            result.Errors.Should().Contain(failure => failure.PropertyName == nameof(UserDto.Name));
        }

        [Fact]
        public void NoUserName_Failure()
        {
            var result = ShouldFail(new UserDto { Name = "John Doe" });
            result.Errors.Should().Contain(failure => failure.PropertyName == nameof(UserDto.UserName));
        }

        [Fact]
        public void NoEmail_Failure()
        {
            var result = ShouldFail(new UserDto { Name = "John Doe", UserName = "john.doe" });
            result.Errors.Should().Contain(failure => failure.PropertyName == nameof(UserDto.Email));
        }

        private ValidationResult ShouldFail(UserDto user)
        {
            var validator = new UserDtoValidator();
            var result = validator.Validate(user);
            result.IsValid.Should().BeFalse();
            return result;
        }

        [Fact]
        public void AllRequiredFieldsFilled_Success()
        {
            var validator = new UserDtoValidator();
            var result = validator.Validate(new UserDto
            {
                Name = "John Doe",
                UserName = "john.doe",
                Email = "john.doe@mycompany.net"
            });
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }
    }
}
