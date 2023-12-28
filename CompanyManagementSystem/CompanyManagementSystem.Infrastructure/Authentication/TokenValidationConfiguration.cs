using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CompanyManagementSystem.Infrastructure.Authentication;

public class TokenValidationConfiguration
{
    public static TokenValidationParameters GetTokenValidationParameters(string issuer, string audience, string secretKey)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero
        };
    }
}
