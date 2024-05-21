using ControlSystem.Domain.Entities;

namespace ControlSystem.Domain.Repositories;

public interface ITransportOrderRepository
{
    public Task<List<TransportOrder>> GetAllAsync();
    public Task<TransportOrder> GetByIdAsync(int id);
    public Task<List<TransportOrder>> GetByStatusAsync(TransportOrderStatus transportOrderStatus);
    public Task CreateAsync(TransportOrder transportOrder);
    public Task UpdateAsync(TransportOrder transportOrder);
    public Task DeleteAsync(int id);
}