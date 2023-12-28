using System.Security.Claims;

namespace CompanyManagementSystem.Core.Interfaces.Services;

public interface ITokenService
{
    string GenerateJwt(List<Claim> claims, int expirationInMinutes);

    List<Claim> GetClaimsFromJwt(string jwt);
}
