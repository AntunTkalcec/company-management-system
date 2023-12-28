using CompanyManagementSystem.Core.Entities.Base;
using CompanyManagementSystem.Core.Interfaces.Repositories.Base;
using CompanyManagementSystem.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;
using System.Web.Http;

namespace CompanyManagementSystem.Infrastructure.Repositories.Base;

public abstract class BaseRepository<T>(CompanyManagementSystemDBContext context) : IBaseRepository<T> where T : BaseEntity, new()
{
    protected readonly CompanyManagementSystemDBContext _context = context;
    protected DbSet<T> _entities = context.Set<T>();

    protected async virtual Task SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new HttpResponseException(HttpStatusCode.InternalServerError);
        }
    }

    public async Task<T> AddAsync(T entity)
    {
        _ = entity ?? throw new ArgumentNullException(nameof(entity), "Entity cannot be null");

        T? result = (await _entities.AddAsync(entity)).Entity;
        await SaveChangesAsync();

        return result;
    }

    public async Task<T> AddOrUpdateAsync(T entity)
    {
        _ = entity ?? throw new ArgumentNullException(nameof(entity), "Entity cannot be null");

        if (entity.Id is 0)
        {
            return await AddAsync(entity);
        }
        else
        {
            return await UpdateAsync(entity);
        }
    }

    public async Task AddRangeAsync(ICollection<T> entities)
    {
        _ = entities ?? throw new ArgumentNullException(nameof(entities), "List of entities cannot be null");

        await _entities.AddRangeAsync(entities);
        await SaveChangesAsync();
    }

    public async Task DeleteAllAsync(List<T> entities)
    {
        _entities.RemoveRange(entities);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _ = entity ?? throw new ArgumentNullException(nameof(entity), "Entity cannot be null");

        await DeleteAsync(entity.Id);
    }

    public async Task DeleteAsync(int id)
    {
        T? entity = _entities.Where(i => i.Id == id).FirstOrDefault();

        if (entity is null)
        {
            throw new ArgumentNullException(nameof(id), "Couldn't find entity with given id.");
        }

        _entities.Remove(entity);

        await SaveChangesAsync();
    }

    public IQueryable<T> Fetch()
    {
        return _entities;
    }

    public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        return await SetIncludes(includes).AsSplitQuery().AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        return await FirstAsync(x => x.Id == id, includes);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _ = entity ?? throw new ArgumentNullException(nameof(entity), "Entity cannot be null");

        T? existing = await _context.Set<T>().FindAsync(entity.Id);

        if (existing is not null)
        {
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("Entity does not exist");
        }

        return existing;
    }

    public async Task UpdateRangeAsync(ICollection<T> entities)
    {
        _ = entities ?? throw new ArgumentNullException(nameof(entities), "List of entities cannot be null");

        _entities.UpdateRange(entities);
        await SaveChangesAsync();
    }

    public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        return await SetIncludes(includes).FirstOrDefaultAsync(predicate);
    }

    protected IQueryable<T> SetIncludes(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _entities.AsQueryable();

        foreach (Expression<Func<T, object>> inc in includes)
        {
            query = query.Include(inc);
        }

        return query.AsNoTracking();
    }
}
