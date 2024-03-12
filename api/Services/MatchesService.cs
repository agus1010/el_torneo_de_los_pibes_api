using AutoMapper;

using api.Models.Dtos.Match;
using api.Repositories;
using api.Services.Interfaces;
using api.Models.Entities;


// https://stackoverflow.com/questions/33041113/c-sharp-entity-framework-correct-use-of-dbcontext-class-inside-your-repository
namespace api.Services
{
	public class MatchesService
	{
		protected readonly MatchesRepository matchesRepository;
		protected readonly ITeamsService teamsService;
		protected readonly IMapper mapper;

		public MatchesService(MatchesRepository matchesRepository, ITeamsService teamsService, IMapper mapper)
		{
			this.matchesRepository = matchesRepository;
			this.teamsService = teamsService;
			this.mapper = mapper;
		}

		public async Task<MatchDto> CreateMatch(PlayersOnlyFriendlyMatchCreationDto matchCreationDto)
		{
			var team1 = await teamsService.CreateTeam(matchCreationDto.Team1Dto);
			var team2 = await teamsService.CreateTeam(matchCreationDto.Team2Dto);
			var match = new Match()
			{
				Team1 = mapper.Map<Team>(team1),
				Team2 = mapper.Map<Team>(team2),
				Tournament = null
			};
			match = await matchesRepository.CreateAsync(match);
			return mapper.Map<MatchDto>(match);
		}

		
		public async Task<MatchDto?> GetAsync(int id)
		{
			var match = await matchesRepository.GetAsync(id);
			MatchDto? matchDto = null;
			if (match != null)
				matchDto = mapper.Map<MatchDto>(match);
			return matchDto;
		}


		public async Task<IEnumerable<MatchDto>> GetAsync(Tournament tournament)
		{
			throw new NotImplementedException();
		}


		public async Task UpdateAsync(int id, MatchUpdateDto matchUpdateDto)
		{
			throw new NotImplementedException();
		}


		public async Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
