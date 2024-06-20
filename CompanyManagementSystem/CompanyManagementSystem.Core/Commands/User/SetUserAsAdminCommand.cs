using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Services;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.User;

public class SetUserAsAdminCommand(int id) : IRequest<ErrorOr<Updated>>
{
    public int Id { get; set; } = id;
}

public class SetUserAsAdminCommandHandler(ILogger<SetUserAsAdminCommandHandler> logger, IUserService userService) : IRequestHandler<SetUserAsAdminCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(SetUserAsAdminCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ErrorOr<UserDTO> userResult = await userService.GetByIdAsync(request.Id);

            if (userResult.IsError)
            {
                logger.LogError("User with id '{id}' was not found!", request.Id);

                return userResult.Errors;
            }

            userResult.Value.IsAdmin = true;

            return await userService.UpdateAsync(userResult.Value.Id, userResult.Value);
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong setting a user as admin: {ex}", ex.Message);

            return ErrorPartials.Unexpected.InternalServerError();
        }
    }
}
