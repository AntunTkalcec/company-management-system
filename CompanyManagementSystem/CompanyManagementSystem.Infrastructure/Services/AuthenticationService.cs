using AutoMapper;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Entities;
using CompanyManagementSystem.Core.Interfaces.Services;
using System.Security.Claims;

namespace CompanyManagementSystem.Infrastructure.Services;

public class AuthenticationService(IUserService userService, IMapper mapper) : IAuthenticationService
{
    public async Task<UserDTO?> RefreshTokenAsync(List<Claim> claims)
    {
        if (claims is not null)
        {
            Claim? userClaim = claims.SingleOrDefault(c => c.Type == "UserId");
            
            if (userClaim is not null)
            {
                User user = mapper.Map<User>(await userService.GetByIdAsync(int.Parse(userClaim.Value)));

                if (user is not null)
                {
                    UserDTO userDto = userService.Login(user);
                    return userDto;
                }

                return null;
            }
        }

        return null;
    }
}
