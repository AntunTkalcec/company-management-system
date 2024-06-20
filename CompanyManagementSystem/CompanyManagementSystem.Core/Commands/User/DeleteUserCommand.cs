using CompanyManagementSystem.Core.Interfaces.Services;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.User;

public class DeleteUserCommand(int id) : IRequest<ErrorOr<Deleted>>
{
    public int Id { get; set; } = id;
}

public class DeleteUserCommandHandler(ILogger<DeleteUserCommandHandler> logger, IUserService userService) : IRequestHandler<DeleteUserCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ErrorOr<Deleted> userResult = await userService.DeleteAsync(request.Id);

            if (userResult.IsError)
            {
                logger.LogError("User with id '{request.Id}' was not found!", request.Id);

                return userResult.Errors;
            }

            return userResult.Value;
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong deleting a user: {ex}", ex.Message);

            return ErrorPartials.Unexpected.InternalServerError();
        }
    }
}
