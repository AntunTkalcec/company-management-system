using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CompanyManagementSystem.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(_ => _.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        return services;
    }
}
