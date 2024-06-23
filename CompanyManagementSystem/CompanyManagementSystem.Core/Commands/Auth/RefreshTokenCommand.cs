using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Services;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace CompanyManagementSystem.Core.Commands.Auth;

public class RefreshTokenCommand(string refreshToken) : IRequest<ErrorOr<UserDTO>>
{
    public string RefreshToken { get; set; } = refreshToken;
}

public class RefreshTokenCommandHandler(ILogger<RefreshTokenCommandHandler> logger, IAuthenticationService authenticationService, ITokenService tokenService) 
    : IRequestHandler<RefreshTokenCommand, ErrorOr<UserDTO>>
{
    public async Task<ErrorOr<UserDTO>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
		try
		{
            if (!tokenService.IsRefreshTokenValid(request.RefreshToken))
            {
                return ErrorPartials.Auth.RefreshTokenInvalid("The given refresh token is expired or otherwise invalid.");
            }

            List<Claim>? claims = tokenService.GetClaimsFromJwt(request.RefreshToken);

            if (claims is null)
            {
                return ErrorPartials.Auth.ClaimsFailure("Something went wrong.");
            }

            ErrorOr<UserDTO> userDtoResult = await authenticationService.RefreshTokenAsync(claims);

            if (userDtoResult.IsError)
            {
                return userDtoResult.Errors;
            }

            return userDtoResult.Value;
        }
        catch (Exception ex)
		{
			logger.LogError("Something went wrong when refreshing a user's token: {ex}", ex.Message);

			return ErrorPartials.Unexpected.InternalServerError();
		}
    }
}
