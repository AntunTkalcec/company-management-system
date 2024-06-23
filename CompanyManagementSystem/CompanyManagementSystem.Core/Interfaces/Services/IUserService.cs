using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Entities;
using CompanyManagementSystem.Core.Interfaces.Services.Base;
using ErrorOr;

namespace CompanyManagementSystem.Core.Interfaces.Services;

public interface IUserService : IBaseService<UserDTO>
{
    ErrorOr<UserDTO> Login(User user);

    Task<ErrorOr<User>> UserValid(string emailOrUserName, string password);
}
