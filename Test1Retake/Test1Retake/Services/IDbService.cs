using Test1Retake.Dtos;

namespace Test1Retake.Services;

public interface IDbService
{
    Task<GetMakersDto> GetMakerById(int id);
    Task<List<GetMakersDto>> GetMakers(string? name);
    Task CreateMaker(CreateMakerDto dto);
}