using FluentValidation.Results;
using UserManager.Contracts.Dtos;
using UserManager.Contracts.Requests;
using UserManager.Validators;

namespace UserManager.Test.Validators
{
    public class CreateUserDtoValidatorTest : UserDtoValidatorTest<CreateUserRequestValidator, CreateUserRequest>
    {
    }

    public class ReplaceUserDtoValidatorTest : UserDtoValidatorTest<ReplaceUserRequestValidator, ReplaceUserRequest>
    {
        [Fact]
        public void NoId_Failure()
        {
            var result = ShouldFail(new ReplaceUserRequest { UserName = "john.doe" });
            result.Errors.Should().Contain(failure => failure.PropertyName == nameof(UserDto.Id));
        }
    }

    public abstract class UserDtoValidatorTest<TUserValidator, TUser> where TUser : UserDtoBase, new()
        where TUserValidator: UserDtoValidatorBase<TUser>, new()
    {
        [Fact]
        public void NoName_Failure()
        {
            var result = ShouldFail(new TUser { UserName = "john.doe" });
            result.Errors.Should().Contain(failure => failure.PropertyName == nameof(UserDto.Name));
        }

        [Fact]
        public void NoUserName_Failure()
        {
            var result = ShouldFail(new TUser { Name = "John Doe" });
            result.Errors.Should().Contain(failure => failure.PropertyName == nameof(UserDto.UserName));
        }

        [Fact]
        public void NoEmail_Failure()
        {
            var result = ShouldFail(new TUser { Name = "John Doe", UserName = "john.doe" });
            result.Errors.Should().Contain(failure => failure.PropertyName == nameof(UserDto.Email));
        }

        protected ValidationResult ShouldFail(TUser user)
        {
            var result = Validate(user);
            result.IsValid.Should().BeFalse();
            return result;
        }

        private ValidationResult ShouldSucceed(TUser user)
        {
            var result = Validate(user);
            result.IsValid.Should().BeTrue();
            return result;
        }

        private ValidationResult Validate(TUser user)
        {
            var validator = new TUserValidator();
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
