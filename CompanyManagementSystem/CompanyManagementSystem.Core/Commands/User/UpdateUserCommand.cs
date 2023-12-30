using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.User;

public class UpdateUserCommand(int id, UserDTO userDto) : IRequest
{
	public int Id { get; set; } = id;

    public UserDTO UserDto { get; set; } = userDto;
}

public class UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> logger, IUserService userService) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
		try
		{
			await userService.UpdateAsync(request.Id, request.UserDto);
		}
		catch (Exception ex)
		{
			logger.LogError("Something went wrong updating a user: {ex}", ex.Message);

			throw;
		}
    }
}
