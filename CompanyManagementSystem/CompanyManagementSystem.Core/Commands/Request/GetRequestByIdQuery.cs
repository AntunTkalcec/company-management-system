using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Services;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.Request;

public class GetRequestByIdQuery(int id) : IRequest<ErrorOr<RequestDTO>>
{
    public int Id { get; set; } = id;
}

public class GetRequestByIdQueryHandler(ILogger<GetRequestByIdQueryHandler> logger, IRequestService requestService) : IRequestHandler<GetRequestByIdQuery, ErrorOr<RequestDTO>>
{
    public async Task<ErrorOr<RequestDTO>> Handle(GetRequestByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            ErrorOr<RequestDTO> requestResult = await requestService.GetByIdAsync(request.Id);

            if (requestResult.IsError)
            {
                logger.LogError("Could not find request with id '{id}'.", request.Id);

                return requestResult.Errors;
            }

            return requestResult.Value;
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong retrieving a request by id: {ex}", ex.Message);

            return ErrorPartials.Unexpected.InternalServerError();
        }
    }
}
