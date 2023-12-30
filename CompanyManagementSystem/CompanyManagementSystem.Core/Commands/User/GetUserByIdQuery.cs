using AutoMapper;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Exceptions;
using CompanyManagementSystem.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.User;

public class GetUserByIdQuery(int id) : IRequest<UserDTO>
{
    public int Id { get; set; } = id;
}

public class GetUserByIdQueryHandler(ILogger<GetUserByIdQueryHandler> logger, IUserService userService) : IRequestHandler<GetUserByIdQuery, UserDTO>
{
    public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
		try
		{
			return await userService.GetByIdAsync(request.Id) ?? throw new NotFoundException("That user does not exist.");
		}
		catch (Exception ex)
		{
			logger.LogError("Something went wrong retrieving a user by id: {ex}", ex.Message);

			throw;
		}
    }
}
