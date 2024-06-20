using CompanyManagementSystem.Core.Entities.Base;
using ErrorOr;
using System.Linq.Expressions;

namespace CompanyManagementSystem.Core.Interfaces.Repositories.Base;

public interface IBaseRepository<T> where T : BaseEntity
{
    IQueryable<T> Fetch();

    Task<T> AddAsync(T entity);

    Task AddRangeAsync(ICollection<T> entities);

    Task<ErrorOr<Deleted>> DeleteAsync(T entity);

    Task<ErrorOr<Deleted>> DeleteAsync(int id);

    Task DeleteAllAsync(List<T> entities);

    Task<ErrorOr<Updated>> UpdateAsync(T entity);

    Task UpdateRangeAsync(ICollection<T> entities);

    Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

    Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
}
