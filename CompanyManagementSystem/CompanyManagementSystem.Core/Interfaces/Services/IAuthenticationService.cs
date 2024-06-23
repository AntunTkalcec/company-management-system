using CompanyManagementSystem.Core.DTOs;
using ErrorOr;
using System.Security.Claims;

namespace CompanyManagementSystem.Core.Interfaces.Services;

public interface IAuthenticationService
{
    Task<ErrorOr<UserDTO>> RefreshTokenAsync(List<Claim> claims);
}
