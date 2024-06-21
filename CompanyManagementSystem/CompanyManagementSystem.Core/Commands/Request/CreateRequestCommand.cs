using CompanyManagementSystem.Core.DTOs.Input;
using CompanyManagementSystem.Core.Interfaces.Services;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.Request;

public class CreateRequestCommand(RequestInputModel input) : IRequest<ErrorOr<int>>
{
    public RequestInputModel Request { get; set; } = input;
}

public class CreateRequestCommandHandler(ILogger<CreateRequestCommandHandler> logger, IRequestService requestService) : IRequestHandler<CreateRequestCommand, ErrorOr<int>>
{
    public async Task<ErrorOr<int>> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {

            return await requestService.CreateAsync(new()
            {
                Accepted = request.Request.Accepted,
                RequestType = request.Request.RequestType,
                TimeOffStartDate = request.Request.TimeOffStartDate,
                TimeOffEndDate = request.Request.TimeOffEndDate,
                CreatorId = request.Request.CreatorId,
            });
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong creating a request: {ex}", ex.Message);

            return ErrorPartials.Unexpected.InternalServerError();
        }
    }
}
