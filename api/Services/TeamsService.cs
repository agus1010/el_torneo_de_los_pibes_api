using AutoMapper;

using api.Models.Dtos.Player;
using api.Models.Dtos.Team;
using api.Repositories.Interfaces;
using api.Services.Interfaces;


namespace api.Services
{
    public class TeamsService : ITeamsService
	{
		protected readonly ITeamsRepository teamsRepo;
		protected readonly IPlayersService playersService;
		protected readonly IMapper mapper;

        public TeamsService(ITeamsRepository teamsRepository, IPlayersService playersService, IMapper mapper)
        {
            teamsRepo = teamsRepository;
			this.playersService = playersService;
			this.mapper = mapper;
        }



        public async Task<TeamDto> CreateTeam(TeamCreationDto teamCreationDto)
		{
			throw new NotImplementedException();
		}


		public async Task<TeamDto?> GetTeam(int id, bool includedPlayers)
		{
			throw new NotImplementedException();
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
