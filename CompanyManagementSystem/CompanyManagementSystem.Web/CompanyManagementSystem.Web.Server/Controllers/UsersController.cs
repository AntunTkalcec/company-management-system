using CompanyManagementSystem.Core.Commands.User;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.DTOs.Input;
using CompanyManagementSystem.Web.Server.ActionFilters;
using CompanyManagementSystem.Web.Server.Controllers.Base;
using CompanyManagementSystem.Web.Server.Routes;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagementSystem.Web.Server.Controllers;

[ApiController]
[AuthorizationFilter()]
public class UsersController(IMediator mediator) : BaseController
{
    [HttpGet(ApiRoutes.Users.General.GetAll)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        ErrorOr<List<UserDTO>> usersResult = await mediator.Send(new GetUserListQuery(), cancellationToken);

        return usersResult.Match(
            Ok,
            Problem);
    }

    [HttpGet(ApiRoutes.Users.General.GetById)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        ErrorOr<UserDTO> userResult = await mediator.Send(new GetUserByIdQuery(id), cancellationToken);

        return userResult.Match(
            Ok,
            Problem);
    }

    [HttpPost(ApiRoutes.Users.General.Create)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post(RegisterInputModel registerInputModel, CancellationToken cancellationToken)
    {
        ErrorOr<int> result = await mediator.Send(new CreateUserCommand(registerInputModel), cancellationToken);

        return result.Match(
            id => CreatedAtAction(nameof(GetById), new { id }, null),
            Problem);
    }

    [HttpPut(ApiRoutes.Users.General.Update)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Put(int id, UserDTO userDTO, CancellationToken cancellationToken)
    {
        ErrorOr<Updated> result = await mediator.Send(new UpdateUserCommand(id, userDTO), cancellationToken);

        return result.Match(
            updated => NoContent(),
            Problem);
    }

    [HttpDelete(ApiRoutes.Users.General.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [AdminFilter()]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        ErrorOr<Deleted> result = await mediator.Send(new DeleteUserCommand(id), cancellationToken);

        return result.Match(
            deleted => NoContent(),
            Problem);
    }

    [HttpPatch(ApiRoutes.Users.General.SetAdmin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [AdminFilter()]
    public async Task<IActionResult> SetAdmin(int id, CancellationToken cancellationToken)
    {
        ErrorOr<Updated> result = await mediator.Send(new SetUserAsAdminCommand(id), cancellationToken);

        return result.Match(
            updated => NoContent(),
            Problem);
    }
}