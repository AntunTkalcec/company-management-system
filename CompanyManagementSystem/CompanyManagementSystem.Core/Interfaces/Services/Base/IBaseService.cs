using CompanyManagementSystem.Core.DTOs.Base;

namespace CompanyManagementSystem.Core.Interfaces.Services.Base;

public interface IBaseService<T> where T : BaseDTO
{
    Task<List<T>> GetAllAsync();

    Task<T> GetByIdAsync(int id);

    Task<int> CreateAsync(T entity);

    Task UpdateAsync(int id, T entity);

    Task DeleteAsync(int id);
}
