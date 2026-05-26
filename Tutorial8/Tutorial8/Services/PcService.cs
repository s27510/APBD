using Microsoft.EntityFrameworkCore;
using Tutorial8.Data;
using Tutorial8.DTOs;
using Tutorial8.Models;

namespace Tutorial8.Services;

public class PcService : IPcService
{
    private readonly AppDbContext _context;

    public PcService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PcDTOs.PcResponseDto>> GetAllAsync()
    {
        return await _context.Pcs
            .Select(p => new PcDTOs.PcResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Weight = p.Weight,
                Warranty = p.Warranty,
                CreatedAt = p.CreatedAt,
                Stock = p.Stock
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<PcComponentResponseDto>?> GetComponentsAsync(int id)
    {
        var exists = await _context.Pcs.AnyAsync(p => p.Id == id);

        if (!exists)
            return null;

        return await _context.PcComponents
            .Where(pc => pc.PcId == id)
            .Select(pc => new PcComponentResponseDto
            {
                ComponentId = pc.ComponentId,
                Name = pc.Component.Name,
                Type = pc.Component.Type,
                Price = pc.Component.Price,
                Quantity = pc.Quantity
            })
            .ToListAsync();
    }

    public async Task<PcDTOs.PcResponseDto> CreateAsync(PcDTOs.CreatePcDto dto)
    {
        var pc = new Pc
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt,
            Stock = dto.Stock
        };

        _context.Pcs.Add(pc);
        await _context.SaveChangesAsync();

        return new PcDTOs.PcResponseDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<bool> UpdateAsync(int id, PcDTOs.UpdatePcDto dto)
    {
        var pc = await _context.Pcs.FindAsync(id);

        if (pc == null)
            return false;

        pc.Name = dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pc = await _context.Pcs
            .Include(p => p.PcComponents)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pc == null)
            return false;

        _context.PcComponents.RemoveRange(pc.PcComponents);
        _context.Pcs.Remove(pc);

        await _context.SaveChangesAsync();

        return true;
    }
}
