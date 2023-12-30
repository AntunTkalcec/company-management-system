using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.DTOs.Input;
using CompanyManagementSystem.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.Request;

public class CreateRequestCommand(RequestInputModel input) : IRequest<int>
{
    public RequestInputModel Request { get; set; } = input;
}

public class CreateRequestCommandHandler(ILogger<CreateRequestCommandHandler> logger, IRequestService requestService) : IRequestHandler<CreateRequestCommand, int>
{
    public async Task<int> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
		try
		{
			RequestDTO requestDto = new()
			{
				Accepted = request.Request.Accepted,
				RequestType = request.Request.RequestType,
				TimeOffStartDate = request.Request.TimeOffStartDate,
				TimeOffEndDate = request.Request.TimeOffEndDate,
				CreatorId = request.Request.CreatorId,
			};

			return await requestService.CreateAsync(requestDto);
		}
		catch (Exception ex)
		{
			logger.LogError("Something went wrong creating a request: {ex}", ex.Message);

			throw;
		}
    }
}
