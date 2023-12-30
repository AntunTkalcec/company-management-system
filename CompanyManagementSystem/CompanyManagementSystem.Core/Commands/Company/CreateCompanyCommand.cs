using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.Company;

public class CreateCompanyCommand(CompanyDTO companyDto) : IRequest<int>
{
    public CompanyDTO CompanyDto { get; set; } = companyDto;
}

public class CreateCompanyCommandHandler(ILogger<CreateCompanyCommandHandler> logger, ICompanyService companyService) : IRequestHandler<CreateCompanyCommand, int>
{
    public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
		try
		{
			return await companyService.CreateAsync(request.CompanyDto);
		}
		catch (Exception ex)
		{
			logger.LogError("Something went wrong when adding a company: {ex}", ex.Message);

			throw;
		}
    }
}
