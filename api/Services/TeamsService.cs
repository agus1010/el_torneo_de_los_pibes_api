using AutoMapper;

using api.Models.Dtos.Player;
using api.Models.Dtos.Team;
using api.Services.Interfaces;
using api.Models.Entities;
using api.Repositories;
using api.Services.Errors;


namespace api.Services
{
    public class TeamsService : ITeamsService
	{
		protected readonly TeamsRepository teamsRepo;
		protected readonly IPlayersService playersService;
		protected readonly IMapper mapper;

        public TeamsService(TeamsRepository teamsRepository, IPlayersService playersService, IMapper mapper)
        {
            teamsRepo = teamsRepository;
			this.playersService = playersService;
			this.mapper = mapper;
        }



        public async Task<TeamDto> CreateTeam(TeamCreationDto teamCreationDto)
		{
			var team = mapper.Map<Team>(teamCreationDto);
			var playerDtos = await playersService.GetAsync(teamCreationDto.PlayerIds);
			team.Players = mapper.Map<ISet<Player>>(playerDtos);
			team = await teamsRepo.CreateAsync(team);
			return mapper.Map<TeamDto>(team);
		}


		public async Task<TeamDto?> GetTeam(int id, bool includedPlayers)
		{
			var team = await teamsRepo.GetAsync(id, includedPlayers);
			TeamDto? teamDto = null;
			if (team != null)
				teamDto = mapper.Map<TeamDto>(team);
			return teamDto;
		}


		public async Task EditTeam(TeamUpdateDto teamUpdateDto)
		{
			var team = mapper.Map<Team>(teamUpdateDto);
			team.Players = mapper.Map<ISet<Player>>(await playersService.GetAsync(teamUpdateDto.PlayerIds));
			await teamsRepo.UpdateAsync(team);
		}


		public async Task DeleteTeam(int id)
		{
			var team = await teamsRepo.GetAsync(id, false, true);
			if (team == null)
				throw new EntityNotFoundException();
			await teamsRepo.DeleteAsync(team);
		}



		public async Task<ISet<PlayerDto>> GetPlayers(int teamId)
		{
			var team = await teamsRepo.GetAsync(teamId, true, false);
			if (team == null)
				throw new EntityNotFoundException();
			return mapper.Map<ISet<PlayerDto>>(team.Players);
		}


		public async Task EditPlayers(int teamId, TeamPlayersEditDto teamPlayersEditDto)
		{
			var team = await teamsRepo.GetAsync(teamId, true, false);
			if (team == null)
				throw new EntityNotFoundException();
			
		}
	}
}
