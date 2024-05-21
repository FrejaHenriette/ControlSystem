using System.Text.Json;
using ControlSystem.Models;
using ControlSystem.Repository;

namespace ControlSystem.Services;

public class TransportOrderService(IRepository<TransportOrder> repository, IMessageBrokerService messageBrokerService)
{ 
    public async Task<IEnumerable<TransportOrder>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }
    
    public async Task<TransportOrder> GetByIdAsync(int id)
    {
        return await repository.GetByIdAsync(id);
    }
    
    public async Task<int> CreateAsync(TransportOrder transportOrder)
    {
        messageBrokerService.SendMessage(JsonSerializer.Serialize(transportOrder));
        return await repository.CreateAsync(transportOrder);
    }
    
    public async Task<int> UpdateAsync(TransportOrder transportOrder)
    {
        messageBrokerService.SendMessage(JsonSerializer.Serialize(transportOrder));
        return await repository.UpdateAsync(transportOrder);
    }
    
    public async Task DeleteAsync(int id)
    {
        messageBrokerService.SendMessage($"Transport order with id {id} has been deleted.");
        await repository.DeleteAsync(id);
    }
    
    public async Task<IEnumerable<TransportOrder>> GetByStatusAsync(string status)
    {
        return await repository.GetByStatusAsync(status);
    }
}