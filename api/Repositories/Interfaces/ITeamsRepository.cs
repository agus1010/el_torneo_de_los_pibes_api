using api.Models.Entities;

namespace api.Repositories.Interfaces
{
    public interface ITeamsRepository
    {
        Task AddPlayers(Team team, ISet<Player> addedPlayers);
        Task<Team> CreateAsync(Team team, bool track = false);
        Task DeleteAsync(Team team);
        Task EditPlayers(Team team, ISet<Player> addedPlayers, ISet<Player> removedPlayers);
        Task<Team?> GetAsync(int id, bool includePlayers, bool track = false);
        IEnumerable<Player> playersSetDifference(IEnumerable<Player> set1, IEnumerable<Player> set2);
        IEnumerable<Player> playersSetIntersection(IEnumerable<Player> set1, IEnumerable<Player> set2);
        Task RemovePlayers(Team team, ISet<Player> removedPlayers);
        Task UpdateAsync(Team updatedTeam);
    }
}