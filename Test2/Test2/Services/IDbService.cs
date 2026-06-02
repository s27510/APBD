using Test2.Dtos;
using Test2.Models;

namespace Test2.Services;

public interface IDbService
{
    Task<IEnumerable<OrderDto>> GetOrdersAsync();
    Task<OrderDto> GetOrderByIdAsync(int id);
    Task UpdateOrderAsync(int id, UpdateOrderDto order);
    Task<bool> DeleteAsync(int id);
    Task<Order> CreateAsync(CreateOrderDto dto);
}