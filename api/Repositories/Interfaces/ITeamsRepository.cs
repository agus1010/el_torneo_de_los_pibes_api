using api.Models.Entities;

namespace api.Repositories.Interfaces
{
    public interface ITeamsRepository
    {
        Task<Team> CreateAsync(Team team);
        Task DeleteAsync(int id);
        Task<Team?> GetAsync(int id, bool includePlayers, bool track = false);
        Task UpdateAsync(Team updatedTeamScalars, ISet<Player> removedPlayers, ISet<Player> addedPlayers);
    }
}