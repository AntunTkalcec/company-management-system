using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Exceptions;
using CompanyManagementSystem.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.Company;

public class GetCompanyByIdQuery(int id) : IRequest<CompanyDTO>
{
    public int Id { get; set; } = id;
}

public class GetCompanyByIdQueryHandler(ILogger<GetCompanyByIdQueryHandler> logger, ICompanyService companyService) : IRequestHandler<GetCompanyByIdQuery, CompanyDTO>
{
    public async Task<CompanyDTO> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
		try
		{
			return await companyService.GetByIdAsync(request.Id) ?? throw new NotFoundException("That company does not exist.");
		}
		catch (Exception ex)
		{
			logger.LogError("Something went wrong retrieving a company by id: {ex}", ex.Message);

			throw;
		}
    }
}
