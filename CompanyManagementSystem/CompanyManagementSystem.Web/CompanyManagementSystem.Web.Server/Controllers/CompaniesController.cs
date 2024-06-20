using CompanyManagementSystem.Core.Commands.Company;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Web.Server.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using CompanyManagementSystem.Web.Server.Routes;

namespace CompanyManagementSystem.Web.Server.Controllers;

[ApiController]
//[AuthorizationFilter()]
public class CompaniesController(IMediator mediator) : BaseController
{
    [HttpGet(ApiRoutes.Companies.General.GetAll)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        ErrorOr<List<CompanyDTO>> companiesResult = await mediator.Send(new GetCompanyListQuery(), cancellationToken);

        return companiesResult.Match(
            Ok,
            Problem);
    }

    [HttpGet(ApiRoutes.Companies.General.GetById)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        ErrorOr<CompanyDTO> companyResult = await mediator.Send(new GetCompanyByIdQuery(id), cancellationToken);

        return companyResult.Match(
            Ok,
            Problem);
    }

    [HttpPost(ApiRoutes.Companies.General.Create)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    //[AdminFilter()]
    public async Task<IActionResult> Post(CompanyDTO companyDTO, CancellationToken cancellationToken)
    {
        ErrorOr<int> result = await mediator.Send(new CreateCompanyCommand(companyDTO), cancellationToken);

        return result.Match(
            id => CreatedAtAction(nameof(GetById), id, null),
            Problem);
    }

    [HttpPut(ApiRoutes.Companies.General.Update)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //[AdminFilter()]
    public async Task<IActionResult> Put(int id, CompanyDTO companyDTO, CancellationToken cancellationToken)
    {
        ErrorOr<Updated> result = await mediator.Send(new UpdateCompanyCommand(id, companyDTO), cancellationToken);

        return result.Match(
            updated => NoContent(),
            Problem);
    }
}
