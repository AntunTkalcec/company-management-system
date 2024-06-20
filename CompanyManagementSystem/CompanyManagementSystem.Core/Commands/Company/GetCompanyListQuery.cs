using AutoMapper;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Services;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.Company;

public class GetCompanyListQuery : IRequest<ErrorOr<List<CompanyDTO>>>
{
}

public class GetCompanyListQueryHandler(ILogger<GetCompanyListQueryHandler> logger, ICompanyService companyService, IMapper mapper)
    : IRequestHandler<GetCompanyListQuery, ErrorOr<List<CompanyDTO>>>
{
    public async Task<ErrorOr<List<CompanyDTO>>> Handle(GetCompanyListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await companyService.GetAllAsync();
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong retrieving companies: {ex}", ex.Message);

            return ErrorPartials.Unexpected.InternalServerError();
        }
    }
}
