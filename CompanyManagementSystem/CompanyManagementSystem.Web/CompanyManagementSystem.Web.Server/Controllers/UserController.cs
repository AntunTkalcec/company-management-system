using CompanyManagementSystem.Core.Commands.User;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Web.Server.ActionFilters;
using CompanyManagementSystem.Web.Server.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagementSystem.Web.Server.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
//[AuthorizationFilter()]
public class UserController(IMediator mediator) : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(List<UserDTO>), 200)]
    public async Task<ActionResult<List<UserDTO>>> Get(CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetUserListQuery(), cancellationToken));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserDTO), 200)]
    public async Task<ActionResult<UserDTO>> GetById(int id, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetUserByIdQuery(id), cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult> Post(UserDTO userDTO, CancellationToken cancellationToken)
    {
        return CreatedAtAction(nameof(Post), await mediator.Send(new CreateUserCommand(userDTO), cancellationToken));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, UserDTO userDTO, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateUserCommand(id, userDTO), cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    //[AdminFilter()]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteUserCommand(id), cancellationToken);

        return NoContent();
    }

    [HttpPatch("{id}")]
    //[AdminFilter()]
    public async Task<ActionResult> SetAdmin(int id, CancellationToken cancellationToken)
    {
        await mediator.Send(new SetUserAsAdminCommand(id), cancellationToken);

        return NoContent();
    }
}