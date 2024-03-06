using api.Models.Dtos.Player;
using api.Models.Dtos.Team;

namespace api.Services.Interfaces
{
    public interface ITeamsService
    {
        Task<TeamDto> CreateTeam(TeamCreationDto teamCreationDto);
        Task DeleteTeam(int id);
        Task EditPlayers(int teamId, TeamPlayersEditDto teamPlayersEditDto);
        Task EditTeam(TeamUpdateDto teamUpdateDto);
        Task<ISet<PlayerDto>> GetPlayers(int teamId);
        Task<TeamDto?> GetTeam(int id, bool includedPlayers);
    }
}