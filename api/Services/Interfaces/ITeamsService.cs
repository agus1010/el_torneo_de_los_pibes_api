using api.Models.Dtos.Team;
using api.Services.Interfaces.Base;


namespace api.Services.Interfaces
{
	public interface ITeamsService : IIdDependantService<TeamDto, TeamCreationDto>
	{
		Task AddPlayers(int teamId, ISet<int> playerIds);
		Task RemovePlayers(int teamId, ISet<int> playerIds);
		Task<TeamDto> Create(TeamCreationDto teamCreationDto);
		Task<IEnumerable<TeamDto>> GetAll();
		Task UpdateWith(TeamUpdateDto teamUpdateDto);
	}
}
