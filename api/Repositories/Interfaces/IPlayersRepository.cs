using api.Models.Entities;

namespace api.Repositories.Interfaces
{
    public interface IPlayersRepository
    {
        Task<IEnumerable<Player>> CreateAsync(IEnumerable<Player> players);
        Task<Player> CreateAsync(Player player);
        Task DeleteAsync(Player player);
        Task<IEnumerable<Player>> GetAsync(IEnumerable<int> ids);
        Task<Player?> GetAsync(int id);
        Task<IEnumerable<Player>> UpdateAsync(IEnumerable<Player> players);
        Task<Player> UpdateAsync(Player player);
    }
}