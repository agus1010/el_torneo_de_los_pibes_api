using api.Models.Dtos.Team;
using api.Services.Interfaces.Base;


namespace api.Services.Interfaces
{
	public interface ITeamsService : IIdDependantService<TeamDto, TeamCreationDto>
	{
		Task<TeamDto> Create(TeamCreationDto teamCreationDto);
		Task<IEnumerable<TeamDto>> GetAll();
	}
}
