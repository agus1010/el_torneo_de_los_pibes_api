using api.Models.Entities;

namespace api.Repositories.Interfaces
{
    public interface ITeamsRepository
    {
        Task<Team> CreateAsync(Team team);
        Task DeleteAsync(int id);
        Task EditTeamPlayers(Team team, IEnumerable<Player> playersRemoved, IEnumerable<Player> playersAdded);
        Task<Team?> GetAsync(int id);
        Task UpdateAsync(Team updatedTeam);
    }
}