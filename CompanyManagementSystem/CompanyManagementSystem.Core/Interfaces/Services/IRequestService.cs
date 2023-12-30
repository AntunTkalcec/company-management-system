using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Services.Base;

namespace CompanyManagementSystem.Core.Interfaces.Services;

public interface IRequestService : IBaseService<RequestDTO>
{
    Task<List<RequestDTO>> GetUnacceptedForCompany(int userId);
}