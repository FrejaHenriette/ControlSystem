using ControlSystem.Domain.Entities;

namespace ControlSystem.Application.DTOs;

// TODO: Setup AutoMapper to be able to partially update the transportOrder
public class TransportOrderDto
{
    public DateTime TransportOrderDate { get; init; }
    public int DestinationId { get; set; }
    public int OriginId { get; set; }
    public string TransportOrderState { get; set; } = default!;
    public List<int> Items { get; set; }
    public int EmployeeId { get; init; }

    public TransportOrder ToTransportOrder(int id)
    {
        var transportOrder = new TransportOrder
        {
            Id = id,
            TransportOrderItem = Items.Select(item => new TransportOrderItem{TransportOrderId = item}).ToList(),
            Employee = new Employee{Id = EmployeeId},
            Destination = new Location{Id = DestinationId},
            Origin = new Location{Id = OriginId},
            TransportOrderDate = TransportOrderDate,
        };
        
        switch (TransportOrderState)
        {
            case "Created":
                transportOrder.TransportOrderState = Domain.Entities.TransportOrderState.Created;
                break;
            case "InTransit":
                transportOrder.TransportOrderState = Domain.Entities.TransportOrderState.InTransit;
                break;
            case "OnHold":
                transportOrder.TransportOrderState = Domain.Entities.TransportOrderState.OnHold;
                break;
            case "Delivered":
                transportOrder.TransportOrderState = Domain.Entities.TransportOrderState.Delivered;
                break;
        }
        
        return transportOrder;
    }
}
