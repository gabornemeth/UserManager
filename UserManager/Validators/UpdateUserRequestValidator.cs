using FluentValidation;
using UserManager.Contracts.Requests;

namespace UserManager.Validators
{
    public class UpdateUserRequestValidator : UserDtoValidatorBase<ReplaceUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(user => user.Id).NotEmpty();
        }
    }
}
