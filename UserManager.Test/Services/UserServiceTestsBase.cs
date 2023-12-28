using FluentValidation;
using UserManager.Models;
using UserManager.Services;

namespace UserManager.Test.Services
{
    public abstract class UserServiceTestsBase
    {
        protected Mock<IUserRepository> Repository { get; }
        protected UserService UserService { get; }

        public UserServiceTestsBase(AbstractValidator<User> validator)
        {
            Repository = new Mock<IUserRepository>();
            UserService = new UserService(Repository.Object, validator);
        }
    }
}
