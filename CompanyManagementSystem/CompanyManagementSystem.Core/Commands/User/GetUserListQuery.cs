using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Services;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.User;

public class GetUserListQuery : IRequest<ErrorOr<List<UserDTO>>>
{
}

public class GetUserListQueryHandler(ILogger<GetUserListQueryHandler> logger, IUserService userService) : IRequestHandler<GetUserListQuery, ErrorOr<List<UserDTO>>>
{
    public async Task<ErrorOr<List<UserDTO>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await userService.GetAllAsync();
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong when retrieving users: {ex}", ex.Message);

            return ErrorPartials.Unexpected.InternalServerError();
        }
    }
}
