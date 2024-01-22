namespace Postify.Configuration;

public class JwtOptions
{
    internal const string ConfigurationSectionName = "JwtOptions";

    public string Issuer { get; set; } = null!;

    public string Audience { get; set; } = null!;

    public string SecretKey { get; set; } = null!;

    public int TokenLifetime { get; set; }
}
