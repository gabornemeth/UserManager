using FastEndpoints;
using FluentValidation;
using UserManager.Contracts.Dtos;

namespace UserManager.Validators
{
    public class CreateUserDtoValidator : Validator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .WithMessage($"{nameof(CreateUserDto.Name)} is required");
            RuleFor(u => u.UserName)
                .NotEmpty()
                .WithMessage($"{nameof(CreateUserDto.UserName)} is required");
            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage($"{nameof(CreateUserDto.Email)} is required");
        }
    }
}
