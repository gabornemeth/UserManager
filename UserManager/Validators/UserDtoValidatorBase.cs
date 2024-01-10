using FastEndpoints;
using FluentValidation;
using UserManager.Contracts.Dtos;

namespace UserManager.Validators
{
    public abstract class UserDtoValidatorBase<TUserDto> : Validator<TUserDto> where TUserDto : UserDtoBase
    {
        public UserDtoValidatorBase()
        {
            RuleFor(user => user.Name).NotNull().NotEmpty().PersonName()
                .WithMessage($"{nameof(UserDtoBase.Name)} is required");
            RuleFor(user => user.UserName).NotNull().NotEmpty()
                .WithMessage($"{nameof(UserDtoBase.UserName)} is required"); ;
            RuleFor(user => user.Email).NotNull().NotEmpty().EmailAddress()
                .WithMessage($"{nameof(UserDtoBase.Email)} has to be a valid e-mail address");
            RuleFor(user => user.Company!.Name)
                .NotNull().NotEmpty()
                .When(user => user.Company != null)
                .WithMessage($"{nameof(CompanyDto.Name)} is required, if {nameof(UserDtoBase.Company)} is specified");

            RuleFor(user => user.Website).Uri().When(u => u.Website != null)
                .WithMessage($"{nameof(UserDtoBase.Website)} is supplied, but invalid"); ;

            RuleFor(user => user.Address!.City)
                .NotNull().NotEmpty()
                .When(user => user.Address != null)
                .WithMessage($"{nameof(AddressDto.City)} is required, if {nameof(UserDtoBase.Address)} is specified");
            RuleFor(user => user.Address!.ZipCode)
                .NotNull().NotEmpty()
                .When(user => user.Address != null)
                .WithMessage($"{nameof(AddressDto.ZipCode)} is required, if {nameof(UserDtoBase.Address)} is specified");
            RuleFor(user => user.Address!.Street)
                .NotNull().NotEmpty()
                .When(user => user.Address != null)
                .WithMessage($"{nameof(AddressDto.Street)} is required, if {nameof(UserDtoBase.Address)} is specified");

            RuleFor(user => user.Address!.Geolocation!.Latitude)
                .NotEmpty()
                .When(user => user.Address?.Geolocation != null)
                .WithMessage($"{nameof(LocationDto.Latitude)} is required, if {nameof(AddressDto.Geolocation)} is specified");
            RuleFor(user => user.Address!.Geolocation!.Longitude)
                .NotEmpty()
                .When(user => user.Address?.Geolocation != null)
                .WithMessage($"{nameof(LocationDto.Longitude)} is required, if {nameof(AddressDto.Geolocation)} is specified");
        }
    }
}
