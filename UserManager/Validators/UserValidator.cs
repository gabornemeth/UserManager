using FluentValidation;
using UserManager.Models;

namespace UserManager.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name).NotNull().NotEmpty();
            RuleFor(user => user.UserName).NotNull().NotEmpty();
            RuleFor(user => user.Email).NotNull().NotEmpty();
            RuleFor(user => user.Company!.Name)
                .NotNull().NotEmpty()
                .When(user => user.Company != null);

            RuleFor(user => user.Address!.City)
                .NotNull().NotEmpty()
                .When(user => user.Address != null);
            RuleFor(user => user.Address!.ZipCode)
                .NotNull().NotEmpty()
                .When(user => user.Address != null);
            RuleFor(user => user.Address!.Street)
                .NotNull().NotEmpty()
                .When(user => user.Address != null);

            RuleFor(user => user.Address!.Geolocation!.Latitude)
                .NotEmpty()
                .When(user => user.Address?.Geolocation != null);
            RuleFor(user => user.Address!.Geolocation!.Longitude)
                .NotEmpty()
                .When(user => user.Address?.Geolocation != null);
        }
    }
}
