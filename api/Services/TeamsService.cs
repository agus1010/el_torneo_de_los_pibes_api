using AutoMapper;

using api.Models.Dtos.Player;
using api.Models.Dtos.Team;
using api.Repositories.Interfaces;
using api.Services.Interfaces;
using api.Models.Entities;
using api.Repositories;


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
			throw new NotImplementedException();
		}


		public async Task DeleteTeam(int id)
		{
			throw new NotImplementedException();
		}



		public async Task<ISet<PlayerDto>> GetPlayers(int teamId)
		{
			throw new NotImplementedException();
		}


		public async Task EditPlayers(int teamId, TeamPlayersEditDto teamPlayersEditDto)
		{
			throw new NotImplementedException();
		}
	}
}
