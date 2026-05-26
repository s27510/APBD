namespace Tutorial8.DTOs;

public class PcComponentResponseDto
{
    public int ComponentId { get; set; }
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}