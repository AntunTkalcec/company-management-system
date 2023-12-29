using CompanyManagementSystem.Core.Entities;
using CompanyManagementSystem.Infrastructure.Database;
using CompanyManagementSystem.Infrastructure.Repositories.Base;

namespace CompanyManagementSystem.Infrastructure.Repositories;

public class UserRepository(CompanyManagementSystemDBContext context) : BaseRepository<User>(context)
{
}
