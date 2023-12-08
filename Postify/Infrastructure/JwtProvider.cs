using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Postify.Abstractions.Infrastructure;
using Postify.Domain;
using Postify.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Postify.Infrastructure;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _jwtOptions = options.Value;
    }

    public string Generate(User user)
    {
        var claims = new Claim[]
        {
            new(CustomClaims.UserId, user.Id.ToString()),
            new(CustomClaims.UserRole, user.UserRole.ToString())
        };

        var signInCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            notBefore: null,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.TokenLifetime),
            signInCredentials);

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}
