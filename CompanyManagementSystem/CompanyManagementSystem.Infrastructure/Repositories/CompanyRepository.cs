using CompanyManagementSystem.Core.Entities;
using CompanyManagementSystem.Infrastructure.Database;
using CompanyManagementSystem.Infrastructure.Repositories.Base;

namespace CompanyManagementSystem.Infrastructure.Repositories;

public class CompanyRepository(CompanyManagementSystemDBContext context) : BaseRepository<Company>(context)
{
}
