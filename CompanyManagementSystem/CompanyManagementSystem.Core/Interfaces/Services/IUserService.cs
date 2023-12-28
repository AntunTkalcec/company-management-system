using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Entities;
using CompanyManagementSystem.Core.Interfaces.Services.Base;

namespace CompanyManagementSystem.Core.Interfaces.Services;

public interface IUserService : IBaseService<UserDTO>
{
    UserDTO Login(User user);

    Task<User?> UserValid(string emailOrUserName, string password);
}
