using FluentValidation;
using UserManager.Models;
using UserManager.Services;
using UserManager.Validators;

namespace UserManager.Test.Services
{
    public abstract class UserServiceTestsBase
    {
        private readonly AbstractValidator<User>? _validator;

        protected Mock<IUserRepository> Repository { get; }
        protected UserService UserService { get; }

        public UserServiceTestsBase(AbstractValidator<User>? validator = null)
        {
            Repository = new Mock<IUserRepository>();
            UserService = new UserService(Repository.Object, validator);
            _validator = validator;
        }

        [Fact]
        public void CannotInstantiateWithoutRepository()
        {
            Assert.Throws<ArgumentNullException>(() => new UserService(null, _validator));
        }
    }
}
