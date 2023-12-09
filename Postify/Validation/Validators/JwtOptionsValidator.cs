using FluentValidation;
using Postify.OptionsConfiguration;

namespace Postify.Validation.Validators;

public class JwtOptionsValidator : AbstractValidator<JwtOptions>
{
    public JwtOptionsValidator()
    {
        RuleFor(x => x.Issuer).NotEmpty();

        RuleFor(x => x.Audience).NotEmpty();

        RuleFor(x => x.TokenLifetime).GreaterThanOrEqualTo(1);
    }
}
