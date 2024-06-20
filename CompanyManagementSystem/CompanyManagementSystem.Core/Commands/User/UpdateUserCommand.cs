using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Services;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.User;

public class UpdateUserCommand(int id, UserDTO userDto) : IRequest<ErrorOr<Updated>>
{
    public int Id { get; set; } = id;

    public UserDTO UserDto { get; set; } = userDto;
}

public class UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> logger, IUserService userService) : IRequestHandler<UpdateUserCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ErrorOr<Updated> userResult = await userService.UpdateAsync(request.Id, request.UserDto);

            if (userResult.IsError)
            {
                logger.LogError("User with id '{request.Id}' was not found!", request.Id);

                return userResult.Errors;
            }

            return userResult.Value;
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong updating a user: {ex}", ex.Message);

            return ErrorPartials.Unexpected.InternalServerError();
        }
    }
}
