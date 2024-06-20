using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Services;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.Company;

public class UpdateCompanyCommand(int id, CompanyDTO companyDto) : IRequest<ErrorOr<Updated>>
{
    public int Id { get; set; } = id;

    public CompanyDTO CompanyDto { get; set; } = companyDto;
}

public class UpdateCompanyCommandHandler(ILogger<UpdateCompanyCommandHandler> logger, ICompanyService companyService) : IRequestHandler<UpdateCompanyCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ErrorOr<Updated> companyResult = await companyService.UpdateAsync(request.Id, request.CompanyDto);

            if (companyResult.IsError)
            {
                logger.LogError("Company with id '{request.Id}' was not found!", request.Id);

                return companyResult.Errors;
            }

            return companyResult.Value;
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong updating a company: {ex}", ex.Message);

            return ErrorPartials.Unexpected.InternalServerError();
        }
    }
}
