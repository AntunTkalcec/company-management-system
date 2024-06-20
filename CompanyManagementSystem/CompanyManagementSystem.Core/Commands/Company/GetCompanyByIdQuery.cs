using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Services;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.Company;

public class GetCompanyByIdQuery(int id) : IRequest<ErrorOr<CompanyDTO>>
{
    public int Id { get; set; } = id;
}

public class GetCompanyByIdQueryHandler(ILogger<GetCompanyByIdQueryHandler> logger, ICompanyService companyService) : IRequestHandler<GetCompanyByIdQuery, ErrorOr<CompanyDTO>>
{
    public async Task<ErrorOr<CompanyDTO>> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            ErrorOr<CompanyDTO> companyResult = await companyService.GetByIdAsync(request.Id);

            if (companyResult.IsError)
            {
                logger.LogError("Company with id '{id}' was not found!", request.Id);

                return companyResult.Errors;
            }

            return companyResult.Value;
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong retrieving a company by id: {ex}", ex.Message);

            return ErrorPartials.Unexpected.InternalServerError();
        }
    }
}
