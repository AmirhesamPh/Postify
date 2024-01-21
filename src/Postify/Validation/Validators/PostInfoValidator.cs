using FluentValidation;
using Postify.Requests;

namespace Postify.Validation.Validators;

public class PostInfoValidator : AbstractValidator<PostInfo>
{
    public PostInfoValidator()
    {
        RuleFor(x => x.Subject).NotEmpty();

        RuleFor(x => x.Content).NotEmpty();
    }
}
