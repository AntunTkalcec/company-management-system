using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.DTOs.Input;
using CompanyManagementSystem.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.User;

public class CreateUserCommand(RegisterInputModel registerInputModel) : IRequest<int>
{
    public RegisterInputModel RegisterInputModel { get; set; } = registerInputModel;
}

public class CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IUserService userService) : IRequestHandler<CreateUserCommand, int>
{
    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await userService.CreateAsync(new()
            {
                FirstName = request.RegisterInputModel.FirstName,
                LastName = request.RegisterInputModel.LastName,
                Email = request.RegisterInputModel.Email,
                UserName = request.RegisterInputModel.Username,
                Password = request.RegisterInputModel.Password
            });
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong when adding a user: {ex}", ex.Message);

            throw;
        }
    }
}
