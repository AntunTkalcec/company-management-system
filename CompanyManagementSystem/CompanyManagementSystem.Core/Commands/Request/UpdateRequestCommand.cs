using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Services;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.Request;

public class UpdateRequestCommand(int id, RequestDTO requestDto) : IRequest<ErrorOr<Updated>>
{
    public int Id { get; set; } = id;

    public RequestDTO RequestDto { get; set; } = requestDto;
}

public class UpdateRequestCommandHandler(ILogger<UpdateRequestCommandHandler> logger, IRequestService requestService) : IRequestHandler<UpdateRequestCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ErrorOr<Updated> requestResult = await requestService.UpdateAsync(request.Id, request.RequestDto);

            if (requestResult.IsError)
            {
                logger.LogError("Request with id '{request.Id}' was not found!", request.Id);

                return requestResult.Errors;
            }

            return requestResult.Value;
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong updating a request: {ex}", ex.Message);

            return ErrorPartials.Unexpected.InternalServerError();
        }
    }
}
