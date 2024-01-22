using FluentValidation.Results;

namespace Postify.Validation;

public static class ValidationResultExtensions
{
    public static string[] ToErrorMessages(this ValidationResult result)
    {
        if (result.IsValid)
            return [];

        return result.Errors
            .Select(e => $"{e.PropertyName}: {e.ErrorMessage}")
            .ToArray();
    }
}
