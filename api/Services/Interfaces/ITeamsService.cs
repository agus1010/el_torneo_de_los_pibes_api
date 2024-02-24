using api.Models.Dtos.Team;
using api.Services.Interfaces.Base;


namespace api.Services.Interfaces
{
	public interface ITeamsService : IIdDependantService<TeamDto, TeamCreationDto>
	{
		Task<IEnumerable<TeamDto>> GetAll();
	}
}
