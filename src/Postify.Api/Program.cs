using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Postify.Configuration;
using Postify.Configuration.Setups;
using Postify.Endpoints;
using Postify.Infrastructure;
using Postify.Middlewares;
using Postify.Persistence;
using Postify.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<JwtOptions>()
    .BindConfiguration(JwtOptions.ConfigurationSectionName)
    .ValidateFluentValidation()
    .ValidateOnStart();

builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
builder.Services.ConfigureOptions<SwaggerGenOptionsSetup>();

builder.Services.AddInfrasrtucture();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.Services.EnsureDatabaseCreated<ApplicationDbContext>();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseGlobalExceptionHandling();

app.MapAccountEndpoints();

app.MapPostEndpoints();

app.Run();
