using FluentValidation;
using Postify.Requests;

namespace Postify.Validation.Validators;

public class SignInInfoValidator : AbstractValidator<SignInInfo>
{
    public SignInInfoValidator()
    {
        RuleFor(x => x.Username).NotEmpty();

        RuleFor(x => x.Password).NotEmpty();
    }
}
