using CompanyManagementSystem.Core.Interfaces.Services;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.Request;

public class DeleteRequestCommand(int id) : IRequest<ErrorOr<Deleted>>
{
    public int Id { get; set; } = id;
}

public class DeleteRequestCommandHandler(ILogger<DeleteRequestCommandHandler> logger, IRequestService requestService) : IRequestHandler<DeleteRequestCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ErrorOr<Deleted> requestResult = await requestService.DeleteAsync(request.Id);

            if (requestResult.IsError)
            {
                logger.LogError("Request with id '{request.Id}' was not found!", request.Id);

                return requestResult.Errors;
            }

            return requestResult.Value;
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong deleting a request: {ex}", ex.Message);

            return ErrorPartials.Unexpected.InternalServerError();
        }
    }
}
