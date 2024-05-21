namespace ControlSystem.Domain.Entities;

public enum TransportOrderStatus
{
    Created, 
    InTransit,
    Delivered
}
    

public class TransportOrder
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string Destination { get; set; } = default!;
    public string Origin { get; set; } = default!;
    public TransportOrderStatus TransportOrderStatus { get; set; }
    public List<string> Items { get; set; } = default!;
}