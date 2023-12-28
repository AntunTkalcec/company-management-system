using CompanyManagementSystem.Core.Entities.Base;
using System.Linq.Expressions;

namespace CompanyManagementSystem.Core.Interfaces.Repositories.Base;

public interface IBaseRepository<T> where T : BaseEntity
{
    IQueryable<T> Fetch();

    Task<T> AddAsync(T entity);

    Task AddRangeAsync(ICollection<T> entities);

    Task<T> AddOrUpdateAsync(T entity);

    Task DeleteAsync(T entity);

    Task DeleteAsync(int id);

    Task DeleteAllAsync(List<T> entities);

    Task<T> UpdateAsync(T entity);

    Task UpdateRangeAsync(ICollection<T> entities);

    Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

    Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
}
