using FluentValidation.Results;

namespace Postify.Validation;

public static class ValidationResultExtensions
{
    public static string[] SelectErrorMessages(this ValidationResult result)
    {
        if (result.IsValid)
            return Array.Empty<string>();

        return result.Errors
            .Select(e => $"{e.PropertyName}: {e.ErrorMessage}")
            .ToArray();
    }
}
