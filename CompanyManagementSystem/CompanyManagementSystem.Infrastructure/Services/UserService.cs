using AutoMapper;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Entities;
using CompanyManagementSystem.Core.Interfaces.Repositories.Base;
using CompanyManagementSystem.Core.Interfaces.Services;
using CompanyManagementSystem.Infrastructure.Authentication;
using CompanyManagementSystem.Infrastructure.Helpers;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace CompanyManagementSystem.Infrastructure.Services;

public class UserService(IBaseRepository<User> userRepository, IBaseRepository<Company> companyRepository, IMapper mapper, ITokenService tokenService,
            IOptions<TokenDataConfiguration> tokenDataConfiguration) : IUserService
{
    private readonly TokenDataConfiguration _tokenDataConfiguration = tokenDataConfiguration.Value ?? throw new Exception("TokenDataConfiguration is null.");

    public async Task<ErrorOr<int>> CreateAsync(UserDTO entity)
    {
        if (!ValidateUser(entity))
        {
            return ErrorPartials.User.UserValidationFailed("User data is incorrect.");
        }

        entity.Password = HashHelper.Hash(entity.Email, entity.Password);
        await userRepository.AddAsync(mapper.Map<User>(entity));

        return entity.Id;
    }

    public async Task<ErrorOr<Deleted>> DeleteAsync(int id) => await userRepository.DeleteAsync(id);

    public async Task<List<UserDTO>> GetAllAsync()
    {
        List<User> users = await userRepository.GetAllAsync(_ => _.Company);

        return mapper.Map<List<UserDTO>>(users);
    }

    public async Task<ErrorOr<UserDTO>> GetByIdAsync(int id)
    {
        User? user = await userRepository.GetByIdAsync(id, _ => _.Company);

        return user is null ? (ErrorOr<UserDTO>)ErrorPartials.User.UserNotFound($"User with id '{id}' not found!") : (ErrorOr<UserDTO>)mapper.Map<UserDTO>(user);
    }

    public ErrorOr<UserDTO> Login(User user)
    {
        try
        {
            UserDTO userDto = mapper.Map<UserDTO>(user);

            List<Claim> claims =
            [
                new("UserId", user.Id.ToString()),
                new("IsAdmin", user.IsAdmin.ToString())
            ];

            userDto.AuthenticationInfo = new(
                AccessToken: tokenService.GenerateJwt(claims, _tokenDataConfiguration.AccessTokenExpirationInMinutes),
                RefreshToken: tokenService.GenerateJwt(claims, _tokenDataConfiguration.RefreshTokenExpirationInMinutes));

            return userDto;
        }
        catch (Exception)
        {
            return ErrorPartials.Unexpected.InternalServerError();
        }
    }

    public async Task<ErrorOr<Updated>> UpdateAsync(int id, UserDTO entity)
    {
        if (!ValidateUser(entity))
        {
            return ErrorPartials.User.UserValidationFailed("User data is incorrect.");
        }

        User? currentEntity = await userRepository.GetByIdAsync(id);

        if (currentEntity is null)
        {
            return ErrorPartials.User.UserNotFound($"User with id '{id}' not found!");
        }

        entity.Password = currentEntity.Password;

        User user = mapper.Map<User>(entity);
        user.UpdatedAt = DateTime.UtcNow;

        return await userRepository.UpdateAsync(user);
    }

    public async Task<ErrorOr<User>> UserValid(string emailOrUserName, string password)
    {
        User? user = await userRepository
            .Fetch()
            .AsNoTracking()
            .SingleOrDefaultAsync(user => user.Email == emailOrUserName || user.UserName == emailOrUserName);

        return user is not null && user.Password == HashHelper.Hash(user.Email, password)
            ? user
            : ErrorPartials.User.UserNotFound($"User with that information does not exist.");
    }

    #region Private methods
    private static bool ValidateUser(UserDTO entity) => !string.IsNullOrEmpty(entity.FirstName) &&
        !string.IsNullOrEmpty(entity.LastName) &&
        !string.IsNullOrEmpty(entity.UserName) &&
        !string.IsNullOrEmpty(entity.Email);
    #endregion
}
