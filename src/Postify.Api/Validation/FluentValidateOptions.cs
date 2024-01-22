using FluentValidation;
using Microsoft.Extensions.Options;

namespace Postify.Validation;

public class FluentValidateOptions<TOptions>(
    IServiceProvider serviceProvider,
    string? _name)
    : IValidateOptions<TOptions>
    where TOptions : class
{
    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        if (_name is not null && _name != name)
            return ValidateOptionsResult.Skip;

        ArgumentNullException.ThrowIfNull(options);

        using var scope = serviceProvider.CreateScope();

        var validator = scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>();

        var result = validator.Validate(options);

        if (result.IsValid)
            return ValidateOptionsResult.Success;

        var errors = result.Errors
            .Select(x => $"Validation failed for {x.PropertyName} with the error: {x.ErrorMessage}")
            .ToList();

        return ValidateOptionsResult.Fail(errors);
    }
}
