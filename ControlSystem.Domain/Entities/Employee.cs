namespace ControlSystem.Domain.Entities;

public class Employee
{
    public int Id { get; init; }
    public virtual ICollection<TransportOrder> TransportOrders { get; set; }
}