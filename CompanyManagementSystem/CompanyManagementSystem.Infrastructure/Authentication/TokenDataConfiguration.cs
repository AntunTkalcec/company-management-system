namespace CompanyManagementSystem.Infrastructure.Authentication;

public class TokenDataConfiguration
{
    public string? Issuer { get; set; }

    public string? Audience { get; set; }

    public string? SecretKey { get; set; }

    public int AccessTokenExpirationInMinutes { get; set; }

    public int RefreshTokenExpirationInMinutes { get; set; }
}
