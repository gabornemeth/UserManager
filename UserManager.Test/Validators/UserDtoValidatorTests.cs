﻿using FluentValidation.Results;
using UserManager.Contracts.Dtos;
using UserManager.Contracts.Requests;
using UserManager.Validators;

namespace UserManager.Test.Validators
{
    public class CreateUserDtoValidatorTest : UserDtoValidatorTest<CreateUserRequestValidator, CreateUserRequest>
    {
        [Fact]
        public void AllRequiredFieldsFilled_Success()
        {
            ShouldSucceed(new CreateUserRequest
            {
                Name = "John Doe",
                UserName = "john.doe",
                Email = "john.doe@mycompany.net"
            });
        }
    }

    public class ReplaceUserDtoValidatorTest : UserDtoValidatorTest<ReplaceUserRequestValidator, ReplaceUserRequest>
    {
        [Fact]
        public void NoId_Failure()
        {
            var result = ShouldFail(new ReplaceUserRequest { UserName = "john.doe" });
            result.Errors.Should().Contain(failure => failure.PropertyName == nameof(UserDto.Id));
        }

        [Fact]
        public void MissingId_Failure()
        {
            ShouldFail(new ReplaceUserRequest
            {
                Name = "John Doe",
                UserName = "john.doe",
                Email = "john.doe@mycompany.net"
            });
        }

        [Fact]
        public void AllRequiredFieldsFilled_Success()
        {
            ShouldSucceed(new ReplaceUserRequest
            {
                Id = "247834792398fdsfa",
                Name = "John Doe",
                UserName = "john.doe",
                Email = "john.doe@mycompany.net"
            });
        }

    }

    public abstract class UserDtoValidatorTest<TUserValidator, TUser> where TUser : UserDtoBase, new()
        where TUserValidator : UserDtoValidatorBase<TUser>, new()
    {
        [Fact]
        public void NoName_Failure()
        {
            var result = ShouldFail(new TUser { UserName = "john.doe" });
            result.Errors.Should().Contain(failure => failure.PropertyName == nameof(UserDto.Name));
        }

        [Theory]
        [InlineData("John $mith")]
        [InlineData("John Sm!th")]
        public void InvalidName_Failure(string name)
        {
            var result = ShouldFail(TestHelper.GetValidUserDto<TUser>(user => user.Name = name));
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

        [Fact]
        public void InvalidEmail_Failure()
        {
            var result = ShouldFail(new TUser { Name = "John Doe", UserName = "john.doe", Email = "notmail.address" });
            result.Errors.Should().Contain(failure => failure.PropertyName == nameof(UserDto.Email));
        }

        [Fact(Skip = "Has to decide about required e-mail validation rules")]
        public void InvalidEmail2_Failure()
        {
            var result = ShouldFail(new TUser { Name = "John Doe", UserName = "john.doe", Email = "not @ mail.address" });
            result.Errors.Should().Contain(failure => failure.PropertyName == nameof(UserDto.Email));
        }

        [Theory]
        [InlineData("://invalid uri")]
        [InlineData("invalid")]
        [InlineData("")]
        [InlineData("https:///www.mycompany.net")]
        public void InvalidWebsite_Failure(string website)
        {
            var result = ShouldFail(TestHelper.GetValidUserDto<TUser>(user => user.Website = website));
            result.Errors.Should().Contain(failure => failure.PropertyName == nameof(UserDto.Website));
        }

        [Fact]
        public void CompletelyFilled_Success()
        {
            ShouldSucceed(TestHelper.GetValidUserDto<TUser>());
        }

        protected ValidationResult ShouldFail(TUser user)
        {
            var result = Validate(user);
            result.IsValid.Should().BeFalse();
            return result;
        }

        protected ValidationResult ShouldSucceed(TUser user)
        {
            var result = Validate(user);
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
            return result;
        }

        private ValidationResult Validate(TUser user)
        {
            var validator = new TUserValidator();
            return validator.Validate(user);
        }
    }
}
