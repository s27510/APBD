namespace Test1Retake.Dtos;

public class CreateMakerDto
{
    public string name { get; set; } = string.Empty;
    public List<CreateProductDto> products { get; set; } = [];
}

public class CreateProductDto
{
    public string name { get; set; } = string.Empty;
    public string? description { get; set; } = string.Empty;
    public decimal strickerPrice { get; set; }
    public string type { get; set; } = string.Empty;
}