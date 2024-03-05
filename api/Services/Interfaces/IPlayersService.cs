using api.Models.Dtos.Player;

namespace api.Services.Interfaces
{
    public interface IPlayersService
    {
        Task<PlayerDto> CreateAsync(PlayerCreationDto playerCreationDto);
        Task DeleteAsync(int id);
        Task<PlayerDto?> GetAsync(int id);
        Task<IEnumerable<PlayerDto?>> GetAsync(ISet<int> ids);
        Task UpdateAsync(PlayerDto playerDto);
    }
}