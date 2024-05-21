using System.Text.Json;
using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Repositories;

namespace ControlSystem.Application.Services;

public class TransportOrderService(ITransportOrderRepository transportOrderRepository, IMessageBrokerService messageBrokerService)
{ 
    public async Task<IEnumerable<TransportOrder>> GetAllAsync()
    {
        return await transportOrderRepository.GetAllAsync();
    }
    
    public async Task<TransportOrder> GetByIdAsync(int id)
    {
        return await transportOrderRepository.GetByIdAsync(id);
    }
    
    public async Task CreateAsync(TransportOrder transportOrder)
    {
        messageBrokerService.SendMessage(JsonSerializer.Serialize(transportOrder));
        await transportOrderRepository.CreateAsync(transportOrder);
    }
    
    public async Task UpdateAsync(TransportOrder transportOrder)
    {
        messageBrokerService.SendMessage(JsonSerializer.Serialize(transportOrder));
        await transportOrderRepository.UpdateAsync(transportOrder);
    }
    
    public async Task DeleteAsync(int id)
    {
        messageBrokerService.SendMessage($"Transport order with id {id} has been deleted.");
        await transportOrderRepository.DeleteAsync(id);
    }
    
    public async Task<List<TransportOrder>> GetByStatusAsync(TransportOrderStatus status)
    {
        return await transportOrderRepository.GetByStatusAsync(status);
    }
}