using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ControlSystem.Application.Repositories;

public class TransportOrderRepository(DbContext context) : ITransportOrderRepository
{
    private readonly DbContext _context = context;
    private readonly DbSet<TransportOrder> _dbSet = context.Set<TransportOrder>();

    public async Task CreateAsync(TransportOrder transportOrder)
    {
        _dbSet.Add(transportOrder);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = _dbSet.Find(id);
        
        if (entity == null)
        {
            throw new ArgumentException("TransportOrder not found");
        }
        
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TransportOrder transportOrder)
    {
        _dbSet.Update(transportOrder);
        await _context.SaveChangesAsync();
    }

    public Task<List<TransportOrder>> GetAllAsync()
    {
        var transportOrders = _dbSet.ToListAsync(); 
        return transportOrders;
    }

    public async Task<TransportOrder> GetByIdAsync(int id)
    {
        var transportOrder = await _dbSet.FindAsync(id);
        if (transportOrder == null)
        {
            throw new ArgumentException("TransportOrder not found");
        }
        return transportOrder;
    }

    public Task<List<TransportOrder>> GetByStatusAsync(TransportOrderStatus transportOrderStatus)
    {
        var transportOrders = _dbSet.Where(e => e.TransportOrderStatus == transportOrderStatus).ToListAsync();
        return transportOrders;
    }
}