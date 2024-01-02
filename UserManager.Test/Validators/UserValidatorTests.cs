using FluentValidation.Results;
using UserManager.Models;
using UserManager.Validators;

namespace UserManager.Test.Validators
{
    public class UserValidatorTests
    {
        [Fact]
        public void ValidUser_Success()
        {
            var result = Validate(ContentHelper.GetValidUser());
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void NullOrEmptyName_Error()
        {
            ShouldFail(ContentHelper.GetValidUser(u => u.Name = ""));
            ShouldFail(ContentHelper.GetValidUser(u => u.Name = null!));
        }

        [Fact]
        public void NullOrEmptyUserName_Error()
        {
            ShouldFail(ContentHelper.GetValidUser(u => u.UserName = ""));
            ShouldFail(ContentHelper.GetValidUser(u => u.UserName = null!));
        }

        [Fact]
        public void NullOrEmptyEmail_Error()
        {
            ShouldFail(ContentHelper.GetValidUser(u => u.Email = ""));
            ShouldFail(ContentHelper.GetValidUser(u => u.Email = null!));
        }

        [Fact]
        public void EmptyCompanyName_Error()
        {
            ShouldFail(ContentHelper.GetValidUser(u => u.Company!.Name = ""));
            ShouldFail(ContentHelper.GetValidUser(u => u.Company!.Name = null!));
        }

        [Fact]
        public void NullCompany_Success()
        {
            ShouldSucceed(ContentHelper.GetValidUser(u => u.Company = null));
        }

        [Fact]
        public void NullOrEmptyCity_Error()
        {
            ShouldFail(ContentHelper.GetValidUser(u => u.Address!.City = null!));
            ShouldFail(ContentHelper.GetValidUser(u => u.Address!.City = ""));
        }

        [Fact]
        public void NullOrEmptyZipCode_Error()
        {
            ShouldFail(ContentHelper.GetValidUser(u => u.Address!.ZipCode = null!));
            ShouldFail(ContentHelper.GetValidUser(u => u.Address!.ZipCode = ""));
        }

        [Fact]
        public void NullOrEmptyStreet_Error()
        {
            ShouldFail(ContentHelper.GetValidUser(u => u.Address!.Street = null!));
            ShouldFail(ContentHelper.GetValidUser(u => u.Address!.Street = ""));
        }

        [Fact]
        public void NullAddress_Success()
        {
            ShouldSucceed(ContentHelper.GetValidUser(u => u.Address = null));
        }

        [Fact]
        public void EmptyAddressGeoLocation_Error()
        {
            ShouldFail(ContentHelper.GetValidUser(u => u.Address!.Geolocation = new Location(0, 16.86f)));
            ShouldFail(ContentHelper.GetValidUser(u => u.Address!.Geolocation = new Location(46.81f, 0)));
        }

        private ValidationResult ShouldFail(User user)
        {
            var result = Validate(user);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();

            return result;
        }

        private ValidationResult ShouldSucceed(User user)
        {
            var result = Validate(user);
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();

            return result;
        }

        private ValidationResult Validate(User user)
        {
            var validator = new UserValidator();
            return validator.Validate(user);
        }
    }
}
