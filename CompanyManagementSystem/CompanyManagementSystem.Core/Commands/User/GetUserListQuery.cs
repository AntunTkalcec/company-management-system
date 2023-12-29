using AutoMapper;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompanyManagementSystem.Core.Commands.User;

public class GetUserListQuery : IRequest<List<UserDTO>>
{
}

public class GetUserListQueryHandler(ILogger<GetUserListQueryHandler> logger, IUserService userService, IMapper mapper) : IRequestHandler<GetUserListQuery, List<UserDTO>>
{
    public async Task<List<UserDTO>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
		try
		{
            List<UserDTO> users = await userService.GetAllAsync();

            return mapper.Map<List<UserDTO>>(users);
        }
		catch (Exception ex)
		{
            logger.LogError("Something went wrong when retrieving users: {ex}", ex.Message);

            throw;
		}
    }
}
