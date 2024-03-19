using api.Models.Entities;


namespace api.Repositories.Interfaces
{
    public interface ITeamsRepository
    {
        Task<Team> CreateAsync(Team team);
        Task DeleteAsync(Team team);
        Task EditPlayers(Team team, ISet<Player> addedPlayers, ISet<Player> removedPlayers);
        Task<Team?> GetAsync(int id);
        Task<Team?> GetAsync(int id, bool includePlayers = true);
        Task UpdateAsync(Team updatedTeam);
    }
}