using Tutorial8.DTOs;

namespace Tutorial8.Services;

public interface IPcService
{
    Task<IEnumerable<PcDTOs.PcResponseDto>> GetAllAsync();

    Task<IEnumerable<PcComponentResponseDto>?> GetComponentsAsync(int id);

    Task<PcDTOs.PcResponseDto> CreateAsync(PcDTOs.CreatePcDto dto);

    Task<bool> UpdateAsync(int id, PcDTOs.UpdatePcDto dto);

    Task<bool> DeleteAsync(int id);
}