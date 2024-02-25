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
		protected readonly IPlayersService _playersRepo;

		public TeamsService(IBaseCRUDRepository<Team> teamsRepo, IPlayersService playersRepo, IMapper mapper) : base(teamsRepo, mapper)
		{
			_playersRepo = playersRepo;
		}


		public async Task<TeamDto> Create(TeamCreationDto teamCreationDto)
		{
			var teamDto = _mapper.Map<TeamDto>(teamCreationDto);
			teamDto.Players = await _playersRepo.GetById(teamCreationDto.PlayerIds);
			return await Create(teamDto);
		}


		public async Task<TeamDto?> GetById(int id)
			=> (await Get(filter: t => t.Id == id, includeField: "Players")).FirstOrDefault();


		public async Task<IEnumerable<TeamDto>> GetById(IEnumerable<int> ids)
		{
			var teams = new List<TeamDto>();
			foreach (var id in ids)
			{
				var team = await _repo.ReadSingle(t => t.Id == id, includeField: "Players");
				if (team != null)
					teams.Add(_mapper.Map<TeamDto>(team));
			}
			return teams;
		}


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