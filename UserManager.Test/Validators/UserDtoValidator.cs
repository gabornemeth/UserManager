using FluentValidation.Results;
using UserManager.Contracts.Dtos;
using UserManager.Contracts.Requests;
using UserManager.Validators;

namespace UserManager.Test.Validators
{
    public class CreateUserDtoValidatorTest
    {
        [Fact]
        public void NoName_Failure()
        {
            var result = ShouldFail(new CreateUserRequest { UserName = "john.doe" });
            result.Errors.Should().Contain(failure => failure.PropertyName == nameof(UserDto.Name));
        }

        [Fact]
        public void NoUserName_Failure()
        {
            var result = ShouldFail(new CreateUserRequest { Name = "John Doe" });
            result.Errors.Should().Contain(failure => failure.PropertyName == nameof(UserDto.UserName));
        }

        [Fact]
        public void NoEmail_Failure()
        {
            var result = ShouldFail(new CreateUserRequest { Name = "John Doe", UserName = "john.doe" });
            result.Errors.Should().Contain(failure => failure.PropertyName == nameof(UserDto.Email));
        }

        private ValidationResult ShouldFail(CreateUserRequest user)
        {
            var result = Validate(user);
            result.IsValid.Should().BeFalse();
            return result;
        }

        private ValidationResult ShouldSucceed(CreateUserRequest user)
        {
            var result = Validate(user);
            result.IsValid.Should().BeTrue();
            return result;
        }

        private ValidationResult Validate(CreateUserRequest user)
        {
            var validator = new CreateUserRequestValidator();
            return validator.Validate(user);
        }

        [Fact]
        public void AllRequiredFieldsFilled_Success()
        {
            var validator = new CreateUserRequestValidator();
            var result = validator.Validate(new CreateUserRequest
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
