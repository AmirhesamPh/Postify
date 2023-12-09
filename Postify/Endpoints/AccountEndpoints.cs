using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Postify.Abstractions;
using Postify.Abstractions.Infrastructure;
using Postify.Abstractions.Persistence;
using Postify.Domain;
using Postify.Requests;
using Postify.Responses;
using Postify.Validation;

namespace Postify.Endpoints;

public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(Apis.Accounts.Route);

        group
            .MapPost($"/{Apis.Accounts.Endpoints.SignIn}", SignInAsync)
            .WithName(Apis.Accounts.Endpoints.SignIn)
            .WithOpenApi();

        group
            .MapPost($"/{Apis.Accounts.Endpoints.SignUp}", SignUpAsync)
            .WithName(Apis.Accounts.Endpoints.SignUp)
            .WithOpenApi();
    }

    public static async Task<IResult> SignInAsync(
        [FromBody] SignInInfo signInInfo,
        IValidator<SignInInfo> validator,
        IUserRepository userRepository,
        IHasher hasher,
        IJwtProvider jwtProvider)
    {
        var validationErrors = validator
            .Validate(signInInfo)
            .SelectErrorMessages();

        if (validationErrors.Any())
            return TypedResults.BadRequest<FailureResult>(validationErrors);

        var user = await userRepository.GetByUsernameAsync(signInInfo.Username);

        if (user is null || user.Password != hasher.HashData(signInInfo.Password))
            return TypedResults.NotFound<FailureResult>(ResponseMessages.InvalidUsernameOrPassword);

        var token = jwtProvider.Generate(user);

        return TypedResults.Ok<SuccessResult<string>>(token);
    }

    public static async Task<IResult> SignUpAsync(
        [FromBody] SignUpInfo signUpInfo,
        IValidator<SignUpInfo> validator,
        IUserRepository userRepository,
        IHasher hasher)
    {
        var validationErrors = validator
            .Validate(signUpInfo)
            .SelectErrorMessages();

        if (validationErrors.Any())
            return TypedResults.BadRequest<FailureResult>(validationErrors);

        var isUsernameTaken = await userRepository.IsUsernameRegisteredAsync(signUpInfo.Username);

        if (isUsernameTaken)
            return TypedResults.BadRequest<FailureResult>(ResponseMessages.UsernameIsAlreadyTaken);

        var passwordHash = hasher.HashData(signUpInfo.Password);

        var addedUser = await userRepository.AddAsync(
            signUpInfo.Username,
            passwordHash,
            Enum.Parse<UserRole>(signUpInfo.UserRole, true));

        return TypedResults.Ok<SuccessResult<Guid>>(addedUser.Id);
    }
}
