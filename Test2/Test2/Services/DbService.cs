using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Test2.Data;
using Test2.Dtos;
using Test2.Exceptions;
using Test2.Models;

namespace Test2.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<OrderDto>> GetOrdersAsync()
    {
        var orders = await _context.Orders
            .Select(e => new OrderDto
            {
                Id = e.ID,
                CreatedAt = e.CreatedAt,
                FullfilledAt = e.FullfieldAt,
                Status = e.Status.Name,
                Client = new ClientInfoDto()
                {
                    FirstName = e.Client.FirstName,
                    LastName = e.Client.LastName,
                },
                Products = e.ProductOrders.Select(e => new OrderLineItemDto()
                {
                    Name = e.Product.Name,
                    Price = e.Product.Price,
                    Amount = e.Amount
                }).ToList()
            })
            .ToListAsync();

        return orders;
    }

    public async Task<OrderDto> GetOrderByIdAsync(int id)
    {
        var order = await _context.Orders
            .Select(e => new OrderDto
            {
                Id = e.ID,
                CreatedAt = e.CreatedAt,
                FullfilledAt = e.FullfieldAt,
                Status = e.Status.Name,
                Client = new ClientInfoDto()
                {
                    FirstName = e.Client.FirstName,
                    LastName = e.Client.LastName,
                },
                Products = e.ProductOrders.Select(e => new OrderLineItemDto()
                {
                    Name = e.Product.Name,
                    Price = e.Product.Price,
                    Amount = e.Amount
                }).ToList()
            })
            .FirstOrDefaultAsync(e => e.Id == id);

        if (order is null)
        {
            throw new NotFoundException();
        }
        
        return order;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null)
            return false;

        _context.Orders.Remove(order);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task UpdateOrderAsync(int id, UpdateOrderDto dto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.ID == id);

            if (order is null)
                throw new NotFoundException("Order not found.");

            var status = await _context.Statuses.FirstOrDefaultAsync(s => s.Name.Equals(dto.StatusName));
            if (status is null)
                throw new NotFoundException("Status not found.");

            if (order.FullfieldAt != null)
                throw new ConflictException("Order already fulfilled.");
            
            order.StatusID = status.ID;
            order.FullfieldAt = DateTime.Now;
            
            var relatedProducts = _context.ProductOrders.Where(po => po.OrderID == id);
            _context.ProductOrders.RemoveRange(relatedProducts);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<Order> CreateAsync(CreateOrderDto dto)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.ID == dto.ClientId);

        if (client == null)
            throw new Exception("Client not found");

        var status = await _context.Statuses
            .FirstOrDefaultAsync(s => s.ID == dto.StatusId);

        if (status == null)
            throw new Exception("Status not found");

        var order = new Order
        {
            ClientID = dto.ClientId,
            StatusID = dto.StatusId,
            CreatedAt = DateTime.UtcNow,
            ProductOrders = new List<ProductOrder>()
        };

        foreach (var productDto in dto.Products)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ID == productDto.ProductId);

            if (product == null)
                throw new Exception($"Product {productDto.ProductId} not found");

            order.ProductOrders.Add(new ProductOrder
            {
                ProductID = productDto.ProductId,
                Amount = productDto.Amount
            });
        }

        _context.Orders.Add(order);

        await _context.SaveChangesAsync();

        return order;
    }
}