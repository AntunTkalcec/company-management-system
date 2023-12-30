using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Exceptions;
using CompanyManagementSystem.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.User;

public class SetUserAsAdminCommand(int id) : IRequest
{
    public int Id { get; set; } = id;
}

public class SetUserAsAdminCommandHandler(ILogger<SetUserAsAdminCommandHandler> logger, IUserService userService) : IRequestHandler<SetUserAsAdminCommand>
{
    public async Task Handle(SetUserAsAdminCommand request, CancellationToken cancellationToken)
    {
		try
		{
			UserDTO user = await userService.GetByIdAsync(request.Id) ?? throw new NotFoundException("That user does not exist.");

			user.IsAdmin = true;

			await userService.UpdateAsync(user.Id, user);
        }
        catch (Exception ex)
		{
			logger.LogError("Something went wrong setting a user as admin: {ex}", ex.Message);

			throw;
		}
    }
}
