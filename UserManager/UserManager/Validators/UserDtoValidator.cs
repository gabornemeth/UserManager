using FastEndpoints;
using FluentValidation;
using UserManager.Contracts.Dtos;

namespace UserManager.Validators
{
    public class UserDtoValidator : Validator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .WithMessage($"{nameof(UserDto.Name)} is required");
            RuleFor(u => u.UserName)
                .NotEmpty()
                .WithMessage($"{nameof(UserDto.UserName)} is required");
            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage($"{nameof(UserDto.Email)} is required");
        }
    }
}
