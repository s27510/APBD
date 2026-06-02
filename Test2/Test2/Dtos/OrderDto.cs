namespace Test2.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? FullfilledAt { get; set; }
    public string Status { get; set; } = null!;
    public ClientInfoDto Client { get; set; } = null!;
    public List<OrderLineItemDto> Products { get; set; } = null!;
}

public class ClientInfoDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}

public class OrderLineItemDto
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Amount { get; set; }
}