using CompanyManagementSystem.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.User;

public class DeleteUserCommand(int id) : IRequest
{
    public int Id { get; set; } = id;
}

public class DeleteUserCommandHandler(ILogger<DeleteUserCommandHandler> logger, IUserService userService) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
		try
		{
			await userService.DeleteAsync(request.Id);
		}
		catch (Exception ex)
		{
			logger.LogError("Something went wrong deleting a user: {ex}", ex.Message);

			throw;
		}
    }
}
