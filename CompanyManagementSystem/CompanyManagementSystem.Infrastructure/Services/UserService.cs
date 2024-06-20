using AutoMapper;
using CompanyManagementSystem.Core.Authentication;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Entities;
using CompanyManagementSystem.Core.Exceptions;
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

    public async Task<int> CreateAsync(UserDTO entity)
    {
        if (!ValidateUser(entity))
        {
            throw new BadRequestException("Required fields cannot remain empty!");
        }

        entity.Password = HashHelper.Hash(entity.Email, entity.Password);
        await userRepository.AddAsync(mapper.Map<User>(entity));

        return entity.Id;
    }

    public async Task<ErrorOr<Deleted>> DeleteAsync(int id)
    {
        return await userRepository.DeleteAsync(id);
    }

    public async Task<List<UserDTO>> GetAllAsync()
    {
        List<User> users = await userRepository.GetAllAsync(_ => _.Company);

        return mapper.Map<List<UserDTO>>(users);
    }

    public async Task<ErrorOr<UserDTO>> GetByIdAsync(int id)
    {
        User? user = await userRepository.GetByIdAsync(id, _ => _.Company);

        if (user is null)
        {
            return ErrorPartials.User.UserNotFound($"User with id '{id}' not found!");
        }

        return mapper.Map<UserDTO>(user);
    }

    public UserDTO Login(User user)
    {
        UserDTO userDto = mapper.Map<UserDTO>(user);

        List<Claim> claims =
        [
            new Claim("UserId", user.Id.ToString()),
            new Claim("IsAdmin", user.IsAdmin.ToString())
        ];

        AuthenticationInfo authInfo = new()
        {
            AccessToken = tokenService.GenerateJwt(claims, _tokenDataConfiguration.AccessTokenExpirationInMinutes),
            RefreshToken = tokenService.GenerateJwt(claims, _tokenDataConfiguration.RefreshTokenExpirationInMinutes)
        };
        userDto.AuthenticationInfo = authInfo;

        return userDto;
    }

    public async Task<ErrorOr<Updated>> UpdateAsync(int id, UserDTO entity)
    {
        if (!ValidateUser(entity))
        {
            throw new BadRequestException("Required fields cannot remain empty!");
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

    public async Task<User?> UserValid(string emailOrUserName, string password)
    {
        User? user = await userRepository
            .Fetch()
            .AsNoTracking()
            .SingleOrDefaultAsync(user =>
                string.Equals(user.Email, emailOrUserName, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(user.UserName, emailOrUserName, StringComparison.OrdinalIgnoreCase));

        if (user is not null && user.Password == HashHelper.Hash(user.Email, password))
            return user;

        return null;
    }

    #region Private methods
    private static bool ValidateUser(UserDTO entity)
    {
        return !string.IsNullOrEmpty(entity.FirstName) &&
        !string.IsNullOrEmpty(entity.LastName) &&
        !string.IsNullOrEmpty(entity.UserName) &&
        !string.IsNullOrEmpty(entity.Email);
    }
    #endregion
}
