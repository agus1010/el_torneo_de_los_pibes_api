using api.Models.Entities;

namespace api.Repositories.Interfaces
{
    public interface ITeamsRepository
    {
        Task AddPlayers(Team team, ISet<Player> addedPlayers);
        Task<Team> CreateAsync(Team team);
        Task DeleteAsync(Team team);
        Task EditPlayers(Team team, ISet<Player> addedPlayers, ISet<Player> removedPlayers);
        Task<Team?> GetAsync(int id);
        Task<Team?> GetAsync(int id, bool includePlayers = true);
        Task RemovePlayers(Team team, ISet<Player> removedPlayers);
        Task UpdateAsync(Team updatedTeam);
    }
}