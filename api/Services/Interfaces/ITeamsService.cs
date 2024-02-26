using api.Models.Dtos.Team;
using api.Services.Interfaces.Base;


namespace api.Services.Interfaces
{
	public interface ITeamsService : IIdDependantService<TeamDto, TeamCreationDto>
	{
		Task AddPlayer(int teamId, int playerId);
		Task<TeamDto> Create(TeamCreationDto teamCreationDto);
		Task<IEnumerable<TeamDto>> GetAll();
	}
}
