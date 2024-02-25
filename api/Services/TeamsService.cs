using AutoMapper;

using api.Models.Dtos.Team;
using api.Models.Entities;
using api.Repositories.Interfaces;
using api.Services.Interfaces;
using api.Models.Dtos.Player;


namespace api.Services
{
	public class TeamsService : BaseService<Team, TeamDto>, ITeamsService
	{
		protected readonly IBaseCRUDRepository<Player> _playersRepo;
		public TeamsService(IBaseCRUDRepository<Team> teamsRepo, IPlayersService playersRepo, IMapper mapper) : base(teamsRepo, mapper)
		{
			_playersRepo = playersRepo;
		}


		public async Task<TeamDto> Create(TeamCreationDto teamCreationDto)
		{
			var teamDto = _mapper.Map<TeamDto>(teamCreationDto);
			teamDto.Players = teamCreationDto.PlayerIds
				.Select(id => _playersRepo.ReadSingle(p => p.Id == id))
				.Where(p => p != null)
				.Select(p => _mapper.Map<PlayerDto>(p));
			return await Create(teamDto);
		}


		public async Task<TeamDto?> GetById(int id)
			=> (await Get(filter: t => t.Id == id, includeField: "Players")).FirstOrDefault();


		public async Task<IEnumerable<TeamDto>> GetAll()
			=> await Get(includeField: "Players");


		public async Task DeleteWithId(int id)
		{
			TeamDto? target = await GetById(id);
			if (target == null)
				throw new Exception();
			await Delete(target);
		}
	}
}
