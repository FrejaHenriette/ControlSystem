namespace ControlSystem.Domain.Entities;

public enum TransportOrderState
{
    Created, 
    InTransit,
    OnHold,
    Delivered
}
    

public class TransportOrder
{
    public int Id { get; init; }
    public DateTime TransportOrderDate { get; init; }
    public int DestinationId { get; set; } 
    public virtual Location? Destination { get; set; }
    public int OriginId { get; set; }
    public virtual Location? Origin { get; set; }
    public TransportOrderState TransportOrderState { get; set; }
    public virtual ICollection<TransportOrderItem> TransportOrderItem { get; set; }
    public virtual Employee Employee { get; set; }
    public int EmployeeId { get; init; }
}
