using CompanyManagementSystem.Core.Commands.Request;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.DTOs.Input;
using CompanyManagementSystem.Web.Server.ActionFilters;
using CompanyManagementSystem.Web.Server.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagementSystem.Web.Server.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
//[AuthorizationFilter()]
public class RequestController(IMediator mediator) : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(List<RequestDTO>), 200)]
    public async Task<ActionResult<List<RequestDTO>>> Get(CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetRequestListQuery(UserId), cancellationToken));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RequestDTO), 200)]
    public async Task<ActionResult<RequestDTO>> GetById(int id, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetRequestByIdQuery(id), cancellationToken));
    }

    [HttpPost]
    //[AdminFilter()]
    public async Task<ActionResult> Post(RequestInputModel input, CancellationToken cancellationToken)
    {
        return CreatedAtAction(nameof(Post), await mediator.Send(new CreateRequestCommand(input), cancellationToken));
    }

    [HttpPut("{id}")]
    //[AdminFilter()]
    public async Task<ActionResult> Put(int id, RequestDTO requestDTO, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateRequestCommand(id, requestDTO), cancellationToken);

        return NoContent();
    }
}
