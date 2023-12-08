namespace Postify.Options;

public class JwtOptions
{
    internal const string ConfigurationSectionName = "JwtOptions";

    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string SecretKey { get; set; } = string.Empty;

    public int TokenLifetime { get; set; }
}
