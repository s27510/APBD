using Microsoft.AspNetCore.Mvc;
using Test1Retake.Dtos;
using Test1Retake.Exceptions;
using Test1Retake.Services;

namespace Test1Retake.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MakersController : ControllerBase
{
    private readonly IDbService _dbService;
    public MakersController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [Route("{id}")]
    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _dbService.GetMakerById(id);
            return Ok(result);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? name)
    {
        var result = await _dbService.GetMakers(name);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateMaker([FromBody] CreateMakerDto dto)
    {
        await _dbService.CreateMaker(dto);
        return Created("", null);
    }
}