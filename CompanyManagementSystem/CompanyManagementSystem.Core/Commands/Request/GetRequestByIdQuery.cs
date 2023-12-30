using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Exceptions;
using CompanyManagementSystem.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.Request;

public class GetRequestByIdQuery(int id) : IRequest<RequestDTO>
{
    public int Id { get; set; } = id;
}

public class GetRequestByIdQueryHandler(ILogger<GetRequestByIdQueryHandler> logger, IRequestService requestService) : IRequestHandler<GetRequestByIdQuery, RequestDTO>
{
    public async Task<RequestDTO> Handle(GetRequestByIdQuery request, CancellationToken cancellationToken)
    {
		try
		{
			return await requestService.GetByIdAsync(request.Id) ?? throw new NotFoundException("That request does not exist.");
		}
		catch (Exception ex)
		{
			logger.LogError("Something went wrong retrieving a request by id: {ex}", ex.Message);

			throw;
		}
    }
}
