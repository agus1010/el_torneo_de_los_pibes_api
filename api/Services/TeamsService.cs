using AutoMapper;

using api.Models.Dtos.Team;
using api.Models.Entities;
using api.Repositories.Interfaces;
using api.Services.Interfaces;


namespace api.Services
{
	public class TeamsService : BaseService<Team, TeamDto, TeamCreationDto>, ITeamsService
	{
		public TeamsService(IBaseCRUDRepository<Team> repository, IMapper mapper) : base(repository, mapper)
		{ }


		public async Task<TeamDto?> GetById(int id)
			=> (await Get(t => t.Id == id)).FirstOrDefault();


		public async Task<IEnumerable<TeamDto>> GetAll()
			=> await Get();


		public async Task DeleteWithId(int id)
		{
			TeamDto? target = await GetById(id);
			if (target == null)
				throw new Exception();
			await Delete(target);
		}
	}
}
