using System.Text.Json;
using ControlSystem.Application.DTOs;
using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Repositories;

namespace ControlSystem.Application.Services;

public class TransportOrderService(ITransportOrderRepository transportOrderRepository)
{
    public async Task GetByIdAsync(int id)
    {
        await transportOrderRepository.GetByIdAsync(id);
    }
    
    public async Task CreateAsync(TransportOrder transportOrder)
    {
        // TODO: Implement message broker for async communication
        await transportOrderRepository.CreateAsync(transportOrder);
    }
    
    public async Task UpdateAsync(int id, TransportOrderDto transportOrderDto)
    {
        // TODO: Implement message broker for async communication
        var transportOrder = transportOrderDto.ToTransportOrder(id);
        await transportOrderRepository.UpdateAsync(transportOrder);
    }
    
    public async Task DeleteAsync(int id)
    {
        // TODO: Implement message broker for async communication
        await transportOrderRepository.DeleteAsync(id);
    }
    
    public async Task SetState(int id, string state)
    {
        // TODO: Implement message broker for async communication
        var transportOrderState = ToTransportOrderState(state);
        
        if (transportOrderState is null)
        {
            throw new ArgumentException($"TransportOrderState {state} not found");
        }
        
        var transportOrder = new TransportOrder { Id = id, TransportOrderState = transportOrderState.Value };
        await transportOrderRepository.UpdateAsync(transportOrder);
    }

    private static TransportOrderState? ToTransportOrderState(string state)
    {
        return state switch
        {
            "Created" => TransportOrderState.Created,
            "InTransit" => TransportOrderState.InTransit,
            "OnHold" => TransportOrderState.OnHold,
            "Delivered" => TransportOrderState.Delivered,
            _ => null
        };
    }

    public async Task<List<TransportOrder>> GetByStateAsync(string state)
    {
        var transportOrderState = ToTransportOrderState(state);
        
        if (transportOrderState is null)
        {
            throw new ArgumentException($"TransportOrderState {state} not found");
        }
        
        return await transportOrderRepository.GetByStatusAsync(transportOrderState.Value);
    }
}