using CompanyManagementSystem.Core.Authentication;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Entities;
using CompanyManagementSystem.Core.Interfaces.Services;
using CompanyManagementSystem.Infrastructure.Helpers;
using CompanyManagementSystem.Web.Server.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CompanyManagementSystem.Web.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IUserService userService, ITokenService tokenService, IAuthenticationService authenticationService) : BaseController
{
    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(UserLogin userLogin)
    {
        try
        {
            User? user = await userService.UserValid(userLogin.EmailOrUserName, userLogin.Password);

            if (user is not null)
            {
                UserDTO userDto = userService.Login(user);

                return Ok(userDto);
            }
            return NotFound(new ApiResponseHelper(404, "A user with that information does not exist!"));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponseHelper(400, ex.Message));
        }
    }

    [HttpPost("token/refresh")]
    public async Task<ActionResult<UserDTO>> RefreshToken([FromBody] string refreshToken)
    {
        List<Claim> claims = tokenService.GetClaimsFromJwt(refreshToken);
        UserDTO? userDto = await authenticationService.RefreshTokenAsync(claims);

        if (userDto is not null)
        {
            return Ok(userDto);
        }

        return Unauthorized();
    }
}
