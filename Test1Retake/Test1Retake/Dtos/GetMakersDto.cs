namespace Test1Retake.Dtos;

public class GetMakersDto
{
    public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public List<GetProductDto> products { get; set; } = [];
}

public class GetProductDto
{
    public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public string? description { get; set; } = string.Empty;
    public decimal strickerPrice { get; set; }
    public GetProductTypeDto productType { get; set; }
    public List<GetVendorDto> vendors { get; set; } = [];
}

public class GetProductTypeDto
{
    public int id { get; set; }
    public string name { get; set; } = string.Empty;
}

public class GetVendorDto
{
    public string code { get; set; }
    public string name { get; set; } = string.Empty;
    public int amount { get; set; }
    public decimal pricePerUnit { get; set; }
}