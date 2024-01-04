using FluentValidation;
using UserManager.Contracts.Requests;

namespace UserManager.Validators
{
    public class ReplaceUserRequestValidator : UserDtoValidatorBase<ReplaceUserRequest>
    {
        public ReplaceUserRequestValidator()
        {
            RuleFor(user => user.Id).NotEmpty();
        }
    }
}
