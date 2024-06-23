using CompanyManagementSystem.Core.Authentication;
using CompanyManagementSystem.Core.Commands.Auth;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Web.Server.Controllers.Base;
using CompanyManagementSystem.Web.Server.Routes;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagementSystem.Web.Server.Controllers;

[ApiController]
public class AuthenticationController(IMediator mediator) : BaseController
{
    [HttpPost(ApiRoutes.Auth.General.Login)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(UserLogin userLogin)
    {
        ErrorOr<UserDTO> userResult = await mediator.Send(new LoginCommand(userLogin));

        return userResult.Match(
            Ok,
            Problem);
    }

    [HttpPost(ApiRoutes.Auth.General.RefreshToken)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        ErrorOr<UserDTO> userDtoResult = await mediator.Send(new RefreshTokenCommand(refreshToken));

        return userDtoResult.Match(
            Ok,
            Problem);
    }
}
