using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Services;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.Company;

public class CreateCompanyCommand(CompanyDTO companyDto) : IRequest<ErrorOr<int>>
{
    public CompanyDTO CompanyDto { get; set; } = companyDto;
}

public class CreateCompanyCommandHandler(ILogger<CreateCompanyCommandHandler> logger, ICompanyService companyService) : IRequestHandler<CreateCompanyCommand, ErrorOr<int>>
{
    public async Task<ErrorOr<int>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await companyService.CreateAsync(request.CompanyDto);
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong when adding a company: {ex}", ex.Message);

            return ErrorPartials.Unexpected.InternalServerError();
        }
    }
}
