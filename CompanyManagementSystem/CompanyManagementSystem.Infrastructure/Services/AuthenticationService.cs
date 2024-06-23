using AutoMapper;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Entities;
using CompanyManagementSystem.Core.Interfaces.Services;
using ErrorOr;
using System.Security.Claims;

namespace CompanyManagementSystem.Infrastructure.Services;

public class AuthenticationService(IUserService userService, IMapper mapper) : IAuthenticationService
{
    public async Task<ErrorOr<UserDTO>> RefreshTokenAsync(List<Claim> claims)
    {
        if (claims is not null)
        {
            Claim? userClaim = claims.SingleOrDefault(c => c.Type == "UserId");
            
            if (userClaim is not null)
            {
                if (int.TryParse(userClaim.Value, out int userId))
                {
                    ErrorOr<UserDTO> userDtoResult = await userService.GetByIdAsync(userId);

                    if (userDtoResult.IsError)
                    {
                        return ErrorPartials.User.UserNotFound($"Could not find user with id {userId}.");
                    }

                    User user = mapper.Map<User>(userDtoResult.Value);

                    return userService.Login(user).Value;
                }

                return ErrorPartials.Auth.RefreshTokenFailure("Something went wrong refreshing the access token.");
            }
        }

        return ErrorPartials.Auth.RefreshTokenFailure("Something went wrong refreshing the access token.");
    }
}
