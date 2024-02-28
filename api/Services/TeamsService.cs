using AutoMapper;

using api.Models.Dtos.Player;
using api.Models.Dtos.Team;
using api.Models.Entities;
using api.Repositories.Interfaces;
using api.Services.Interfaces;


namespace api.Services
{
	public class TeamsService : BaseService<Team, TeamDto>, ITeamsService
	{
		protected readonly IPlayersService _playersService;

		public TeamsService(IBaseCRUDRepository<Team> teamsRepo, IPlayersService playersRepo, IMapper mapper) : base(teamsRepo, mapper)
		{
			_playersService = playersRepo;
		}


		public async Task AddPlayers(int teamId, ISet<int> playerIds)
		{
			var teamDto = await GetById(teamId);
			if (teamDto == null)
				throw new Exception();

			var teamPlayerIds = teamDto.Players.Select(p => p.Id).ToArray();
			var playerDtosToAdd = await _playersService.GetById(playerIds);
			foreach (var playerDto in playerDtosToAdd)
			{
				if (teamPlayerIds.Contains(playerDto.Id))
					continue;
				teamDto.Players.Add(playerDto);
			}

			await UpdateWith(teamDto);
		}


		public async Task RemovePlayers(int teamId, ISet<int> playerIds)
		{
			var teamDto = await GetById(teamId);
			if (teamDto == null)
				throw new Exception();

			int paramPlayerIdsCount = playerIds.Count;

			if (paramPlayerIdsCount > teamDto.Players.Count)
				throw new Exception();

			var teamPlayerIds = teamDto.Players.Select(p => p.Id).ToArray();
			foreach (var id in playerIds)
			{
				if (!teamPlayerIds.Contains(id))
					throw new Exception();
			}

			var givePlayerDtos = await _playersService.GetById(playerIds);
			if (givePlayerDtos.Count() != paramPlayerIdsCount)
				throw new Exception();

			foreach (var playerDto in givePlayerDtos)
				teamDto.Players.Remove(playerDto);

			await UpdateWith(teamDto);
		}


		public async Task<TeamDto> Create(TeamCreationDto teamCreationDto)
		{
			var players = await _playersService.GetById(teamCreationDto.PlayerIds);
			
			if (players.Count() < teamCreationDto.PlayerIds.Count)
				throw new Exception();

			var teamDto = _mapper.Map<TeamDto>(teamCreationDto);
			teamDto.Players = _mapper.Map<ISet<PlayerDto>>(players);

			return await Create(teamDto);
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
			await UpdateWith(_mapper.Map<TeamDto>(teamUpdateDto));
		}
	}
}