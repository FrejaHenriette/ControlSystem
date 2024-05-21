namespace ControlSystem.Domain.Entities;

public class Location
{
    public int Id { get; init; }
    public virtual ICollection<TransportOrder> TransportOrderOrigin { get; set; }
    public virtual ICollection<TransportOrder> TransportOrderDestination { get; set; }
}