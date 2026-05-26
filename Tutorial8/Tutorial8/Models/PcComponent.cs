namespace Tutorial8.Models;

public class PcComponent
{
    public int PcId { get; set; }
    public Pc Pc { get; set; } = null!;

    public int ComponentId { get; set; }
    public Component Component { get; set; } = null!;

    public int Quantity { get; set; }
}