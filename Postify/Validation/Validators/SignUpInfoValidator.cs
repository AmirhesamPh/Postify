using FluentValidation;
using Postify.Domain;
using Postify.Requests;

namespace Postify.Validation.Validators;

public class SignUpInfoValidator : AbstractValidator<SignUpInfo>
{
    public SignUpInfoValidator()
    {
        RuleFor(x => x.Username).NotEmpty();

        RuleFor(x => x.Password).NotEmpty();

        RuleFor(x => x.UserRole).IsEnumName(typeof(UserRole), false);
    }
}
