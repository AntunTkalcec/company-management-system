using CompanyManagementSystem.Core.DTOs;
using System.Security.Claims;

namespace CompanyManagementSystem.Core.Interfaces.Services;

public interface IAuthenticationService
{
    Task<UserDTO?> RefreshTokenAsync(List<Claim> claims);
}
