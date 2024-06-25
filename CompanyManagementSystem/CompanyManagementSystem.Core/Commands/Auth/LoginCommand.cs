using CompanyManagementSystem.Core.Authentication;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Services;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.Auth;

public class LoginCommand(UserLogin userLogin) : IRequest<ErrorOr<UserDTO>>
{
    public UserLogin UserLogin { get; set; } = userLogin;
}

public class LoginCommandHandler(ILogger<LoginCommandHandler> logger, IUserService userService) : IRequestHandler<LoginCommand, ErrorOr<UserDTO>>
{
    public async Task<ErrorOr<UserDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ErrorOr<Entities.User> userResult = await userService.UserValid(request.UserLogin.EmailOrUserName, request.UserLogin.Password);

            if (userResult.IsError)
            {
                logger.LogError("User with username - password combination '{username}' - '{password}' was not found!",
                    request.UserLogin.EmailOrUserName, request.UserLogin.Password);

                return userResult.Errors;
            }

            ErrorOr<UserDTO> userDtoResult = userService.Login(userResult.Value);

            if (userDtoResult.IsError)
            {
                logger.LogError("An error occurred when trying to log in a user.");

                return userDtoResult.Errors;
            }

            return userDtoResult.Value;
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong when trying to log in a user: {ex}", ex.Message);

            return ErrorPartials.Unexpected.InternalServerError();
        }
    }
}
