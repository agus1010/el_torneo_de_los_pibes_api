using AutoMapper;

using api.Models.Dtos.Player;
using api.Models.Dtos.Team;
using api.Models.Entities;
using api.Services.Interfaces;
using api.Repositories;
using api.Repositories.Interfaces;


namespace api.Services
{
	public class TeamsService : BaseService<Team, TeamDto>, ITeamsService
	{
		protected readonly IPlayersService _playersService;
		protected readonly IPlayersRepository playersRepo;

		public TeamsService(TeamsRepository teamsRepo, IPlayersService playersService, IPlayersRepository playersRepo, IMapper mapper) : base(teamsRepo, mapper)
		{
			_playersService = playersService;
			this.playersRepo = playersRepo;
		}


		public async Task AddPlayers(int teamId, ISet<int> playerIds)
		{
			await ((TeamsRepository)_repo).AddPlayers(teamId, playerIds);
		}


		public async Task RemovePlayers(int teamId, ISet<int> playerIds)
		{
			await ((TeamsRepository)_repo).RemovePlayers(teamId, playerIds);
		}


		public async Task<TeamDto> Create(TeamCreationDto teamCreationDto)
		{
			var team = _mapper.Map<Team>(teamCreationDto);
			team.Players = playersRepo.ReadMany(teamCreationDto.PlayerIds);
			await ((TeamsRepository)_repo).Create(team);
			
			var teamDto = _mapper.Map<TeamDto>(team);
			teamDto.Players = _mapper.Map<ISet<PlayerDto>>(team.Players);
			return teamDto;
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


		public async Task UpdateWith(TeamUpdateDto teamUpdateDto)
		{
			var team = _mapper.Map<Team>(teamUpdateDto);
			team.Players = (await ((TeamsRepository)_repo).GetPlayers(teamUpdateDto.Id));
			await _repo.Update(team);
		}
	}
}