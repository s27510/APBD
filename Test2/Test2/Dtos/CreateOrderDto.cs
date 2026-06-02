namespace Test2.Dtos;

public class CreateOrderDto
{
    public int ClientId { get; set; }

    public int StatusId { get; set; }

    public List<CreateProductOrderDto> Products { get; set; } = [];
}

public class CreateProductOrderDto
{
    public int ProductId { get; set; }

    public int Amount { get; set; }
}