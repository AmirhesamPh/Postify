using Postify.Domain;

namespace Postify.Abstractions.Infrastructure;

public interface IJwtProvider
{
    string Generate(User user);
}
