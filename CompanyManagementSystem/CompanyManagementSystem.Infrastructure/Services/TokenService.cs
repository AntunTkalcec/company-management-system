﻿using CompanyManagementSystem.Core.Interfaces.Services;
using CompanyManagementSystem.Infrastructure.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CompanyManagementSystem.Infrastructure.Services;

public class TokenService(ILogger<TokenService> logger, IOptions<TokenDataConfiguration> tokenDataConfiguration) : ITokenService
{
    private readonly TokenDataConfiguration _tokenDataConfiguration = tokenDataConfiguration.Value ?? throw new Exception("TokenDataConfiguration is null.");
    public string GenerateJwt(List<Claim> claims, int expirationInMinutes)
    {
        DateTime now = DateTime.UtcNow;

        SymmetricSecurityKey secretKey = new(Encoding.UTF8.GetBytes(_tokenDataConfiguration.SecretKey));
        SigningCredentials signInCredentials = new(secretKey, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken tokenOptions = new(
            issuer: _tokenDataConfiguration.Issuer,
            audience: _tokenDataConfiguration.Audience,
            claims: claims,
            notBefore: now,
            expires: now.AddMinutes(expirationInMinutes),
            signingCredentials: signInCredentials
        );
        string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return tokenString;
    }

    public List<Claim>? GetClaimsFromJwt(string jwt)
    {
        try
        {
            JwtSecurityTokenHandler handler = new();

            handler.ValidateToken(jwt, TokenValidationConfiguration.GetTokenValidationParameters(_tokenDataConfiguration.Issuer,
                _tokenDataConfiguration.Audience, _tokenDataConfiguration.SecretKey), out SecurityToken securityToken);

            if (securityToken is JwtSecurityToken jwtSecurityToken)
            {
                return jwtSecurityToken?.Claims?.ToList();
            }

            return null;
        }
        catch (Exception ex)
        {
            logger.LogError("An error occurred in TokenService - {method}, Exception: {ex}", nameof(GetClaimsFromJwt), ex);

            return null;
        }
    }
}
