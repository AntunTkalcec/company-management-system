using CompanyManagementSystem.Core.DTOs.Base;
using ErrorOr;

namespace CompanyManagementSystem.Core.Interfaces.Services.Base;

public interface IBaseService<T> where T : BaseDTO
{
    Task<List<T>> GetAllAsync();

    Task<ErrorOr<T>> GetByIdAsync(int id);

    Task<int> CreateAsync(T entity);

    Task<ErrorOr<Updated>> UpdateAsync(int id, T entity);

    Task<ErrorOr<Deleted>> DeleteAsync(int id);
}
