using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.Request;

public class UpdateRequestCommand(int id, RequestDTO requestDto) : IRequest
{
    public int Id { get; set; } = id;

    public RequestDTO RequestDto { get; set; } = requestDto;
}

public class UpdateRequestCommandHandler(ILogger<UpdateRequestCommandHandler> logger, IRequestService requestService) : IRequestHandler<UpdateRequestCommand>
{
    public async Task Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
    {
		try
		{
			await requestService.UpdateAsync(request.Id, request.RequestDto);
		}
		catch (Exception ex)
		{
			logger.LogError("Something went wrong updating a request: {ex}", ex.Message);

			throw;
		}
    }
}
