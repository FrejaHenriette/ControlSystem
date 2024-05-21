using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ControlSystem.Repository;

public class Repository<T>(DbContext context) : IRepository<T>
    where T : class
{
    private readonly DbContext _context = context;
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public Task<int> CreateAsync(T entity)
    {
        _dbSet.Add(entity);
        return _context.SaveChangesAsync();
    }

    public Task DeleteAsync(int id)
    {
        var entity = _dbSet.Find(id);
        
        if (entity == null)
        {
            throw new ArgumentException("Entity not found");
        }
        
        _dbSet.Remove(entity);
        return _context.SaveChangesAsync();
    }

    public Task<int> UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        return _context.SaveChangesAsync();
    }

    public Task<List<T>> GetAllAsync()
    {
        var entities = _dbSet.ToListAsync(); 
        return entities;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            throw new ArgumentException("Entity not found");
        }
        return entity;
    }

    public Task<List<T>> GetByStatusAsync(string status)
    {
        var statusProperty = typeof(T).GetProperty("Status");
        if (statusProperty == null)
        {
            throw new InvalidOperationException($"The type {typeof(T).Name} does not have a Status property.");
        }
        
        var entityParameter = Expression.Parameter(typeof(T), "e");
        var statusPropertyAccess = Expression.Property(entityParameter, statusProperty);
        var statusValue = Expression.Constant(status, typeof(string));
        var equalityCheck = Expression.Equal(statusPropertyAccess, statusValue);
        var lambda = Expression.Lambda<Func<T, bool>>(equalityCheck, entityParameter);

        var entities = _dbSet.Where(lambda).ToListAsync();
        return entities;
    }
}