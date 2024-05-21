namespace ControlSystem.Domain.Entities;

public class TransportOrderItem
{
    public int TransportOrderId { get; set; }
    public int ItemId { get; set; }
    public virtual Item Item { get; set; }
    public virtual TransportOrder TransportOrder { get; set; }

}