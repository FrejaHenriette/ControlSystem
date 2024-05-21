namespace ControlSystem.Models;

public class TransportOrder
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string Destination { get; set; } = default!;
    public string Origin { get; set; } = default!;
    public string Status { get; set; } = default!;
    public List<string> Items { get; set; } = default!;
}