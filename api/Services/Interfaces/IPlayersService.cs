using api.Models.Dtos.Player;

namespace api.Services.Interfaces
{
    public interface IPlayersService
    {
        Task<PlayerDto> Create(PlayerCreationDto playerCreationDto);
        Task DeleteWithId(int id);
        Task<IEnumerable<PlayerDto>> GetAll();
        Task<PlayerDto?> GetById(int id);
        Task UpdateWith(PlayerDto playerDto);
    }
}