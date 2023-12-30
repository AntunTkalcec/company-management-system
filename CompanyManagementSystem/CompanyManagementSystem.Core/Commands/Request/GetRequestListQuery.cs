using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Exceptions;
using CompanyManagementSystem.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.Request;

public class GetRequestListQuery(int userId) : IRequest<List<RequestDTO>>
{
	public int UserId { get; set; } = userId;
}

public class GetRequestListQueryHandler(ILogger<GetRequestListQueryHandler> logger, IRequestService requestService, IUserService userService) 
	: IRequestHandler<GetRequestListQuery, List<RequestDTO>>
{
    public async Task<List<RequestDTO>> Handle(GetRequestListQuery request, CancellationToken cancellationToken)
    {
		try
		{
			UserDTO user = await userService.GetByIdAsync(request.UserId);

            return user.CompanyId is null
                ? throw new NotPartOfCompanyException("Cannot retrieve any requests as you are not part of a company.")
                : await requestService.GetUnacceptedForCompany((int)user.CompanyId);
        }
        catch (Exception ex)
		{
			logger.LogError("Something went wrong retrieving requests: {ex}", ex.Message);

			throw;
		}
    }
}