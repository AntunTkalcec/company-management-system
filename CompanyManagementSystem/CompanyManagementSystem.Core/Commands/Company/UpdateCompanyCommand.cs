using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.Company;

public class UpdateCompanyCommand(int id, CompanyDTO companyDto) : IRequest
{
    public int Id { get; set; } = id;

    public CompanyDTO CompanyDto { get; set; } = companyDto;
}

public class UpdateCompanyCommandHandler(ILogger<UpdateCompanyCommandHandler> logger, ICompanyService companyService) : IRequestHandler<UpdateCompanyCommand>
{
    public async Task Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
		try
		{
			await companyService.UpdateAsync(request.Id, request.CompanyDto);
		}
		catch (Exception ex)
		{
			logger.LogError("Something went wrong updating a company: {ex}", ex.Message);

			throw;
		}
    }
}
