using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Services;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.User;

public class GetUserByIdQuery(int id) : IRequest<ErrorOr<UserDTO>>
{
    public int Id { get; set; } = id;
}

public class GetUserByIdQueryHandler(ILogger<GetUserByIdQueryHandler> logger, IUserService userService) : IRequestHandler<GetUserByIdQuery, ErrorOr<UserDTO>>
{
    public async Task<ErrorOr<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            ErrorOr<UserDTO> userResult = await userService.GetByIdAsync(request.Id);

            if (userResult.IsError)
            {
                logger.LogError("User with id '{id}' was not found!", request.Id);

                return userResult.Errors;
            }

            return userResult.Value;
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong retrieving a user by id: {ex}", ex.Message);

            return ErrorPartials.Unexpected.InternalServerError();
        }
    }
}
