using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.User;

public class CreateUserCommand(UserDTO userDto) : IRequest<int>
{
    public UserDTO UserDto { get; set; } = userDto;
}

public class CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IUserService userService) : IRequestHandler<CreateUserCommand, int>
{
    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
		try
		{
			return await userService.CreateAsync(request.UserDto);
		}
		catch (Exception ex)
		{
			logger.LogError("Something went wrong when adding a user: {ex}", ex.Message);

			throw;
		}
    }
}
