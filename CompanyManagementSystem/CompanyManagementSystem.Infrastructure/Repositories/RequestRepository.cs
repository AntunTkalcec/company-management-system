using CompanyManagementSystem.Core.Entities;
using CompanyManagementSystem.Infrastructure.Database;
using CompanyManagementSystem.Infrastructure.Repositories.Base;

namespace CompanyManagementSystem.Infrastructure.Repositories;

public class RequestRepository(CompanyManagementSystemDBContext context) : BaseRepository<Request>(context)
{
}
