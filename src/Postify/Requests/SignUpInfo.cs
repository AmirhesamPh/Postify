using Postify.Domain;

namespace Postify.Requests;

public record SignUpInfo(string Username, string Password, UserRole UserRole);
