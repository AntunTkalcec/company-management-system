using CompanyManagementSystem.Core.Interfaces.Seed;
using CompanyManagementSystem.Infrastructure.Data.SeedData;
using CompanyManagementSystem.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManagementSystem.Infrastructure.Services.Seed;

public class DbInitializer(IServiceScopeFactory scopeFactory) : IDbInitializer
{
    public void Initialize()
    {
        using IServiceScope serviceScope = scopeFactory.CreateScope();
        using CompanyManagementSystemDBContext? context = serviceScope.ServiceProvider.GetService<CompanyManagementSystemDBContext>();
        context?.Database.Migrate();
    }

    public void SeedData()
    {
        using IServiceScope serviceScope = scopeFactory.CreateScope();
        using CompanyManagementSystemDBContext? context = serviceScope.ServiceProvider.GetService<CompanyManagementSystemDBContext>();

        AddIfEmpty(context.Companies, DbSeeder.Companies);

        context.SaveChanges();

        AddIfEmpty(context.Users, DbSeeder.Users);

        context?.SaveChanges();
    }

    private static void AddIfEmpty<T>(DbSet<T> set, IEnumerable<T> entities) where T : class
    {
        if (!set.Any())
        {
            set.AddRange(entities);
        }
    }
}
