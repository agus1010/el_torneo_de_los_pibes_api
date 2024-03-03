using AutoMapper;

using api.Models.Dtos.Match;
using api.Repositories;
using api.Services.Errors;
using api.Models.Entities;


namespace api.Services
{
	public class MatchesService
	{
		private readonly MatchesRepository _repo;
		private readonly IMapper _mapper;
		private readonly TeamsService _teamsService;

		public MatchesService(MatchesRepository repo, IMapper mapper, TeamsService teamsService)
		{
			_repo = repo;
			_mapper = mapper;
			_teamsService = teamsService;
		}


		public async Task<MatchDto> GetById(int id)
		{
			var match = await _repo.GetById(id);
			if (match == null)
				throw new EntityNotFoundException();
			return _mapper.Map<MatchDto>(match);
		}

		public async Task<MatchDto> Create(MatchCreationDto matchCreationDto)
		{
			
		}

		public async Task<MatchDto> Create(FriendlyMatchCreationDto friendlyMatchCreationDto)
		{
			var team1 = _teamsService.Create(friendlyMatchCreationDto.Team1Dto);
			var team2 = _teamsService.Create(friendlyMatchCreationDto.Team2Dto);
			var match = new Match();
			await Task.WhenAll(team1, team2);
			match.Team1 = _mapper.Map<Team>(team1.Result);
			match.Team2 = _mapper.Map<Team>(team2.Result);
			await _repo.Create(match);
			return _mapper.Map<MatchDto>(match);
		}
	}
}