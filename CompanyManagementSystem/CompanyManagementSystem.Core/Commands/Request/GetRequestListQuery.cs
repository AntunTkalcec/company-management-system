using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Services;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.Request;

public class GetRequestListQuery(int userId) : IRequest<ErrorOr<List<RequestDTO>>>
{
    public int UserId { get; set; } = userId;
}

public class GetRequestListQueryHandler(ILogger<GetRequestListQueryHandler> logger, IRequestService requestService, IUserService userService)
    : IRequestHandler<GetRequestListQuery, ErrorOr<List<RequestDTO>>>
{
    public async Task<ErrorOr<List<RequestDTO>>> Handle(GetRequestListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            ErrorOr<UserDTO> userResult = await userService.GetByIdAsync(request.UserId);

            if (userResult.IsError)
            {
                logger.LogError("User with id '{id}' was not found!", request.UserId);

                return userResult.Errors;
            }

            return userResult.Value.CompanyId is null
                ? ErrorPartials.User.UserNotPartOfCompany("Cannot get requests because you are not part of a company.")
                : await requestService.GetUnacceptedForCompany((int)userResult.Value.CompanyId);
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong retrieving requests: {ex}", ex.Message);

            return ErrorPartials.Unexpected.InternalServerError();
        }
    }
}