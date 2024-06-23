using CompanyManagementSystem.Core.Commands.Request;
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
public class RequestsController(IMediator mediator) : BaseController
{
    [HttpGet(ApiRoutes.Requests.General.GetAll)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        ErrorOr<List<RequestDTO>> requestsResult = await mediator.Send(new GetRequestListQuery(UserId), cancellationToken);

        return requestsResult.Match(
            Ok,
            Problem);
    }

    [HttpGet(ApiRoutes.Requests.General.GetById)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        ErrorOr<RequestDTO> requestResult = await mediator.Send(new GetRequestByIdQuery(id), cancellationToken);

        return requestResult.Match(
            Ok,
            Problem);
    }

    [HttpPost(ApiRoutes.Requests.General.Create)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post(RequestInputModel input, CancellationToken cancellationToken)
    {
        ErrorOr<int> result = await mediator.Send(new CreateRequestCommand(input), cancellationToken);

        return result.Match(
            id => CreatedAtAction(nameof(GetById), id, null),
            Problem);
    }

    [HttpPut(ApiRoutes.Requests.General.Update)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Put(int id, RequestDTO requestDTO, CancellationToken cancellationToken)
    {
        ErrorOr<Updated> result = await mediator.Send(new UpdateRequestCommand(id, requestDTO), cancellationToken);

        return result.Match(
            updated => NoContent(),
            Problem);
    }

    [HttpDelete(ApiRoutes.Requests.General.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        ErrorOr<Deleted> result = await mediator.Send(new DeleteRequestCommand(id), cancellationToken);

        return result.Match(
            deleted => NoContent(),
            Problem);
    }
}
