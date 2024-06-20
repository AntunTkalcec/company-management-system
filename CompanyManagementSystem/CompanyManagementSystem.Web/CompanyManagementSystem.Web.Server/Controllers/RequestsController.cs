using CompanyManagementSystem.Core.Commands.Request;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.DTOs.Input;
using CompanyManagementSystem.Web.Server.ActionFilters;
using CompanyManagementSystem.Web.Server.Controllers.Base;
using CompanyManagementSystem.Web.Server.Routes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagementSystem.Web.Server.Controllers;

[ApiController]
//[AuthorizationFilter()]
public class RequestsController(IMediator mediator) : BaseController
{
    [HttpGet(ApiRoutes.Requests.General.GetAll)]
    [ProducesResponseType(typeof(List<RequestDTO>), 200)]
    public async Task<ActionResult<List<RequestDTO>>> Get(CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetRequestListQuery(UserId), cancellationToken));
    }

    [HttpGet(ApiRoutes.Requests.General.GetById)]
    [ProducesResponseType(typeof(RequestDTO), 200)]
    public async Task<ActionResult<RequestDTO>> GetById(int id, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetRequestByIdQuery(id), cancellationToken));
    }

    [HttpPost(ApiRoutes.Requests.General.Create)]
    //[AdminFilter()]
    public async Task<ActionResult> Post(RequestInputModel input, CancellationToken cancellationToken)
    {
        return CreatedAtAction(nameof(Post), await mediator.Send(new CreateRequestCommand(input), cancellationToken));
    }

    [HttpPut(ApiRoutes.Requests.General.Update)]
    //[AdminFilter()]
    public async Task<ActionResult> Put(int id, RequestDTO requestDTO, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateRequestCommand(id, requestDTO), cancellationToken);

        return NoContent();
    }
}
