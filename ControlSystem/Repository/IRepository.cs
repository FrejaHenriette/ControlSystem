using ControlSystem.Models;

namespace ControlSystem.Repository;

public interface IRepository<T>
{
    public Task<List<T>> GetAllAsync();
    public Task<T> GetByIdAsync(int id);
    public Task<List<T>> GetByStatusAsync(string status);
    public Task<int> CreateAsync(T entity);
    public Task<int> UpdateAsync(T entity);
    public Task DeleteAsync(int id);
}