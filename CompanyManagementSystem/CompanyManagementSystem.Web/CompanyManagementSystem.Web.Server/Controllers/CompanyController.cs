using CompanyManagementSystem.Core.Commands.Company;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Web.Server.ActionFilters;
using CompanyManagementSystem.Web.Server.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagementSystem.Web.Server.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
//[AuthorizationFilter()]
public class CompanyController(IMediator mediator) : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(List<CompanyDTO>), 200)]
    public async Task<ActionResult<List<CompanyDTO>>> Get(CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetCompanyListQuery(), cancellationToken));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CompanyDTO), 200)]
    public async Task<ActionResult<CompanyDTO>> GetById(int id, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetCompanyByIdQuery(id), cancellationToken));
    }

    [HttpPost]
    //[AdminFilter()]
    public async Task<ActionResult> Post(CompanyDTO companyDTO, CancellationToken cancellationToken)
    {
        return CreatedAtAction(nameof(Post), await mediator.Send(new CreateCompanyCommand(companyDTO), cancellationToken));
    }

    [HttpPut("{id}")]
    //[AdminFilter()]
    public async Task<ActionResult> Put(int id, CompanyDTO companyDTO, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateCompanyCommand(id, companyDTO), cancellationToken);

        return NoContent();
    }
}
