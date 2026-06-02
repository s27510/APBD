using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using Test2.Dtos;
using Test2.Exceptions;
using Test2.Models;
using Test2.Services;

namespace Test2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{

    private readonly IDbService _dbService;

    public OrderController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpGet]
    public async Task<IEnumerable<OrderDto>> GetOrders()
    {
        return await _dbService.GetOrdersAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<OrderDto> GetOrderById(int id)
    {
        return await _dbService.GetOrderByIdAsync(id);
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteOrder(int id)
    {
        return await _dbService.DeleteAsync(id);
    }

    
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
    {
        try
        {
            var order = await _dbService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetOrderById),
                new { id = order.ID },
                order);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}/fulfill")]
    public async Task<IActionResult> UpdateOrder(int id, UpdateOrderDto dto)
    {
        try
        {
            await _dbService.UpdateOrderAsync(id, dto);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
    }
}