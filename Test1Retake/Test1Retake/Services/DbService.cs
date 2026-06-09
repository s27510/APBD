using Microsoft.Data.SqlClient;
using Test1Retake.Dtos;
using Test1Retake.Exceptions;

namespace Test1Retake.Services;

public class DbService : IDbService
{
    private readonly string _connectionString;

    public DbService(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection") ?? string.Empty;
    }
    
    public async Task<GetMakersDto> GetMakerById(int id)
    {
        var query = """
                    SELECT 
                        m.Name as MakerName, 
                        p.Id as ProductId, 
                        p.Name as ProductName, 
                        p.Description as ProductDescription, 
                        p.StickerPrice as StickerPrice, 
                        pt.Id as TypeId, 
                        pt.Name as TypeName, 
                        v.Code as VendorCode, 
                        v.Name as VendorName, 
                        vp.Amount as Amount, 
                        vp.PricePerUnit as PricePerUnit
                    FROM Makers m
                        JOIN Products p ON p.MakerId = m.Id
                        JOIN ProductTypes pt ON pt.Id = p.ProductTypeId
                        JOIN VendorProducts vp ON vp.ProductId = p.Id
                        JOIN Vendors v ON v.Code = vp.VendorCode
                        WHERE m.Id = @id;
                    """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        
        await using var command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@id", id);
        
        await using var reader = await command.ExecuteReaderAsync();
        
        GetMakersDto? result = null;
        
        var ordMakerName = reader.GetOrdinal("MakerName");
        var ordProductId = reader.GetOrdinal("ProductId");
        var ordProductName = reader.GetOrdinal("ProductName");
        var ordProductDescription = reader.GetOrdinal("ProductDescription");
        var ordStickerPrice = reader.GetOrdinal("StickerPrice");
        var ordTypeId = reader.GetOrdinal("TypeId");
        var ordTypeName = reader.GetOrdinal("TypeName");
        var ordVendorCode = reader.GetOrdinal("VendorCode");
        var ordVendorName = reader.GetOrdinal("VendorName");
        var ordAmount = reader.GetOrdinal("Amount");
        var ordPricePerUnit = reader.GetOrdinal("PricePerUnit");
        
        while (await reader.ReadAsync())
        {
            if (result is null)
            {
                result = new GetMakersDto()
                {
                    id = id,
                    name = reader.GetString(ordMakerName),
                    products = new List<GetProductDto>()
                };
            }

            var productId = reader.GetInt32(ordProductId);
            var product = result.products.FirstOrDefault(p => p.id.Equals(productId));

            if (product is null)
            {
                product = new GetProductDto()
                {
                    id = productId,
                    name = reader.GetString(ordProductName),
                    description = reader.GetString(ordProductDescription),
                    strickerPrice = reader.GetDecimal(ordStickerPrice),
                    productType = new GetProductTypeDto
                    {
                        id = reader.GetInt32(ordTypeId),
                        name = reader.GetString(ordTypeName)
                    },
                    vendors = new List<GetVendorDto>()
                };
                result.products.Add(product);
            }
            
            var vendorId = reader.GetString(ordVendorCode);
            var vendor = product.vendors.FirstOrDefault(v => v.code.Equals(vendorId));

            if (vendor is null)
            {
                vendor = new GetVendorDto()
                {
                    code = vendorId,
                    name = reader.GetString(ordVendorName),
                    amount = reader.GetInt32(ordAmount),
                    pricePerUnit = reader.GetDecimal(ordPricePerUnit)
                };
                product.vendors.Add(vendor);
            }
        }

        return result ?? throw new NotFoundException("No maker found for the specified id.");
    }
    
    public async Task<List<GetMakersDto>> GetMakers(string? name)
    {
        var query = """
                    SELECT 
                        m.Id as MakerId,
                        m.Name as MakerName, 
                        p.Id as ProductId, 
                        p.Name as ProductName, 
                        p.Description as ProductDescription, 
                        p.StickerPrice as StickerPrice, 
                        pt.Id as TypeId, 
                        pt.Name as TypeName, 
                        v.Code as VendorCode, 
                        v.Name as VendorName, 
                        vp.Amount as Amount, 
                        vp.PricePerUnit as PricePerUnit
                    FROM Makers m
                        JOIN Products p ON p.MakerId = m.Id
                        JOIN ProductTypes pt ON pt.Id = p.ProductTypeId
                        JOIN VendorProducts vp ON vp.ProductId = p.Id
                        JOIN Vendors v ON v.Code = vp.VendorCode
                    WHERE (@name IS NULL OR m.Name LIKE '%' + @name + '%');
                    """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        
        await using var command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@name", (object?)name ?? DBNull.Value);
        
        await using var reader = await command.ExecuteReaderAsync();
        
        List<GetMakersDto> result = [];

        var ordMakerId = reader.GetOrdinal("MakerId");
        var ordMakerName = reader.GetOrdinal("MakerName");
        var ordProductId = reader.GetOrdinal("ProductId");
        var ordProductName = reader.GetOrdinal("ProductName");
        var ordProductDescription = reader.GetOrdinal("ProductDescription");
        var ordStickerPrice = reader.GetOrdinal("StickerPrice");
        var ordTypeId = reader.GetOrdinal("TypeId");
        var ordTypeName = reader.GetOrdinal("TypeName");
        var ordVendorCode = reader.GetOrdinal("VendorCode");
        var ordVendorName = reader.GetOrdinal("VendorName");
        var ordAmount = reader.GetOrdinal("Amount");
        var ordPricePerUnit = reader.GetOrdinal("PricePerUnit");
        
        while (await reader.ReadAsync())
        {
            var makerId = reader.GetInt32(ordMakerId);
            var maker = result.FirstOrDefault(m => m.id == makerId);

            if (maker is null)
            {
                maker = new GetMakersDto
                {
                    id = makerId,
                    name = reader.GetString(ordMakerName),
                    products = new List<GetProductDto>()
                };
                result.Add(maker);
            }

            var productId = reader.GetInt32(ordProductId);
            var product = maker.products.FirstOrDefault(p => p.id == productId);

            if (product is null)
            {
                product = new GetProductDto
                {
                    id = productId,
                    name = reader.GetString(ordProductName),
                    description = reader.GetString(ordProductDescription),
                    strickerPrice = reader.GetDecimal(ordStickerPrice),
                    productType = new GetProductTypeDto
                    {
                        id = reader.GetInt32(ordTypeId),
                        name = reader.GetString(ordTypeName)
                    },
                    vendors = new List<GetVendorDto>()
                };

                maker.products.Add(product);
            }

            var vendorCode = reader.GetString(ordVendorCode);
            var vendor = product.vendors.FirstOrDefault(v => v.code == vendorCode);

            if (vendor is null)
            {
                vendor = new GetVendorDto
                {
                    code = vendorCode,
                    name = reader.GetString(ordVendorName),
                    amount = reader.GetInt32(ordAmount),
                    pricePerUnit = reader.GetDecimal(ordPricePerUnit)
                };
                product.vendors.Add(vendor);
            }
        }

        return result;
    }

    public async Task CreateMaker(CreateMakerDto dto)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var transaction = await connection.BeginTransactionAsync();

        try
        {
            var insertMakerQuery = """
                INSERT INTO Makers (Name)
                OUTPUT INSERTED.Id
                VALUES (@name);
            """;

            await using var makerCmd = new SqlCommand(insertMakerQuery, connection, (SqlTransaction)transaction);
            makerCmd.Parameters.AddWithValue("@name", dto.name);

            var makerId = (int)await makerCmd.ExecuteScalarAsync();
            
            if (dto.products is null || dto.products.Count == 0)
            {
                await transaction.CommitAsync();
                return;
            }

            foreach (var p in dto.products)
            {
                var typeIdQuery = """
                    SELECT Id FROM ProductTypes WHERE Name = @name;
                """;

                await using var typeCmd = new SqlCommand(typeIdQuery, connection, (SqlTransaction)transaction);
                typeCmd.Parameters.AddWithValue("@name", p.type);

                var typeIdObj = await typeCmd.ExecuteScalarAsync();

                int typeId;

                if (typeIdObj is null)
                {
                    var insertTypeQuery = """
                        INSERT INTO ProductTypes (Name)
                        OUTPUT INSERTED.Id
                        VALUES (@name);
                    """;

                    await using var insertTypeCmd = new SqlCommand(insertTypeQuery, connection, (SqlTransaction)transaction);
                    insertTypeCmd.Parameters.AddWithValue("@name", p.type);

                    typeId = (int)await insertTypeCmd.ExecuteScalarAsync();
                }
                else
                {
                    typeId = (int)typeIdObj;
                }
                
                var insertProductQuery = """
                    INSERT INTO Products (Name, Description, StickerPrice, MakerId, ProductTypeId)
                    VALUES (@name, @desc, @price, @makerId, @typeId);
                """;

                await using var productCmd = new SqlCommand(insertProductQuery, connection, (SqlTransaction)transaction);
                productCmd.Parameters.AddWithValue("@name", p.name);
                productCmd.Parameters.AddWithValue("@desc", p.description);
                productCmd.Parameters.AddWithValue("@price", p.strickerPrice);
                productCmd.Parameters.AddWithValue("@makerId", makerId);
                productCmd.Parameters.AddWithValue("@typeId", typeId);

                await productCmd.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}