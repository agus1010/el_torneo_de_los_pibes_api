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
		protected readonly IPlayersService _playersService;

		public TeamsService(IBaseCRUDRepository<Team> teamsRepo, IPlayersService playersRepo, IMapper mapper) : base(teamsRepo, mapper)
		{
			_playersService = playersRepo;
		}


		public async Task<TeamDto> Create(TeamCreationDto teamCreationDto)
		{
			var players = await _playersService.GetById(teamCreationDto.PlayerIds);
			
			if (players.Count() < teamCreationDto.PlayerIds.Count)
				throw new Exception();

			var newTeam = _mapper.Map<Team>(teamCreationDto);
			newTeam.Players = _mapper.Map<ICollection<Player>>(players).ToList();

			await _repo.Create(newTeam);
			await _repo.Persist();

			return _mapper.Map<TeamDto>(newTeam);
		}


		public async Task<TeamDto?> GetById(int id)
			=> (await Get(filter: t => t.Id == id, trackEntities: false, includeField: "Players")).FirstOrDefault();


		public async Task<IEnumerable<TeamDto>> GetById(IEnumerable<int> ids)
		{
			var teams = new List<TeamDto>();
			foreach (var id in ids)
			{
				var team = await _repo.ReadSingle(t => t.Id == id, tracked:false, includeField: "Players");
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