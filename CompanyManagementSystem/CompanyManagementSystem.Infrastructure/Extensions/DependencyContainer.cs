using CompanyManagementSystem.Core;
using CompanyManagementSystem.Core.Entities;
using CompanyManagementSystem.Core.Interfaces.Repositories.Base;
using CompanyManagementSystem.Core.Interfaces.Seed;
using CompanyManagementSystem.Core.Interfaces.Services;
using CompanyManagementSystem.Infrastructure.Database;
using CompanyManagementSystem.Infrastructure.Repositories;
using CompanyManagementSystem.Infrastructure.Services;
using CompanyManagementSystem.Infrastructure.Services.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManagementSystem.Infrastructure.Extensions;

public static class DependencyContainer
{
    public static void AddDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CompanyManagementSystemDBContext>(options => options.UseSqlServer(connectionString));
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddMediatR();
        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBaseRepository<User>, UserRepository>();
    }
}
