namespace ControlSystem.Domain.Entities;

public class Item
{
    public int Id { get; init; }
    public int TransportOrderId { get; set; } 
    public virtual ICollection<TransportOrderItem> TransportOrderItem { get; set; }
}