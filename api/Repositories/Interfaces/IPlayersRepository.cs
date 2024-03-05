using api.Models.Entities;

namespace api.Repositories.Interfaces
{
    public interface IPlayersRepository
    {
        Task<IEnumerable<Player>> CreateAsync(IEnumerable<Player> players, bool keepAsTracked = false);
        Task<Player> CreateAsync(Player player, bool keepAsTracked = false);
        Task DeleteAsync(int id);
        Task DeleteAsync(Player player);
        Task<IEnumerable<Player?>> GetAsync(IEnumerable<int> ids, bool track = false);
        Task<Player?> GetAsync(int id, bool track = false);
        Task<IEnumerable<Player>> UpdateAsync(IEnumerable<Player> players, bool keepAsTracked = false);
        Task<Player> UpdateAsync(Player player, bool keepAsTracked = false);
    }
}