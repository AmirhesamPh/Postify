using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Postify.Abstractions;
using Postify.Abstractions.Infrastructure;
using Postify.Abstractions.Persistence;
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
            .WithOpenApi(SetOpenApiOperationForSignUpEndpoint);
    }

    private static async Task<IResult> SignInAsync(
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

    private static async Task<IResult> SignUpAsync(
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
            signUpInfo.UserRole);

        return TypedResults.Ok<SuccessResult<Guid>>(addedUser.Id);
    }

    private static OpenApiOperation SetOpenApiOperationForSignUpEndpoint(OpenApiOperation operation)
    {
        operation.Description = "Please provide a username (no limit), a password (no limit) and a user role ('Admin' or 'Regular')";

        return operation;
    }
}
