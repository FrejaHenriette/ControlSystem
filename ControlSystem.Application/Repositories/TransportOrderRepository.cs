using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Repositories;
using ControlSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ControlSystem.Application.Repositories;

public class TransportOrderRepository(ControlSystemDbContext dbContext) : ITransportOrderRepository
{
    private readonly ControlSystemDbContext _dbContext = dbContext;
    private readonly DbSet<TransportOrder> _dbSet = dbContext.Set<TransportOrder>();

    public async Task CreateAsync(TransportOrder transportOrder)
    {
        _dbSet.Add(transportOrder);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        
        if (entity is null)
        {
            throw new ArgumentException("TransportOrder not found");
        }
        
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TransportOrder transportOrder)
    {
        _dbSet.Update(transportOrder);
        await _dbContext.SaveChangesAsync();
    }

    public Task<List<TransportOrder>> GetAllAsync()
    {
        var transportOrders = _dbSet.ToListAsync(); 
        return transportOrders;
    }

    public async Task<TransportOrder?> GetByIdAsync(int id)
    {
        var transportOrder = await _dbSet.FindAsync(id);
        if (transportOrder is null)
        {
            throw new ArgumentException("TransportOrder not found");
        }
        return transportOrder;
    }

    public Task<List<TransportOrder>> GetByStatusAsync(TransportOrderState transportOrderState)
    {
        var transportOrders = _dbSet.Where(e => e.TransportOrderState == transportOrderState).ToListAsync();
        return transportOrders;
    }
}