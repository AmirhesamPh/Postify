using FluentValidation;
using Postify.Requests;

namespace Postify.Validation.Validators;

public class SignUpInfoValidator : AbstractValidator<SignUpInfo>
{
    public SignUpInfoValidator()
    {
        RuleFor(x => x.Username).NotEmpty();

        RuleFor(x => x.Password).NotEmpty();

        RuleFor(x => x.UserRole).IsInEnum();
    }
}
