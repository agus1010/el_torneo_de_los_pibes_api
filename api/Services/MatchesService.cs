using AutoMapper;

using api.Models.Dtos.Match;
using api.Models.Entities;
using api.Repositories;
using api.Services.Errors;
using api.Services.Interfaces;
using api.Models.Dtos.Team;


namespace api.Services
{
	public class MatchesService
	{
		private readonly MatchesRepository _repo;
		private readonly IMapper _mapper;
		private readonly TeamsService _teamsService;
		private readonly ITournamentsService _turnamentsService;

		public MatchesService(MatchesRepository repo, IMapper mapper, TeamsService teamsService, ITournamentsService tournamentsService)
		{
			_repo = repo;
			_mapper = mapper;
			_teamsService = teamsService;
			_turnamentsService = tournamentsService;
		}


		public async Task<MatchDto> GetById(int id)
		{
			var match = await _repo.GetById(id);
			if (match == null)
				throw new EntityNotFoundException();
			return _mapper.Map<MatchDto>(match);
		}


		public async Task<MatchDto> Create(PlayersOnlyTournamentMatchCreationDto matchCreationDto)
		{
			var match = await populateMatchEntity(matchCreationDto.Team1Dto, matchCreationDto.Team2Dto, matchCreationDto.TournamentId);
			await _repo.Create(match);
			return _mapper.Map<MatchDto>(match);
		}


		public async Task<MatchDto> Create(PlayersOnlyFriendlyMatchCreationDto matchCreationDto)
		{
			var match = await populateMatchEntity(matchCreationDto.Team1Dto, matchCreationDto.Team2Dto);
			await _repo.Create(match);
			return _mapper.Map<MatchDto>(match);
		}



		private async Task<Match> populateMatchEntity(TeamCreationDto team1CreationDto, TeamCreationDto team2CreationDto, int tournamentId = -1)
		{
			var team1 = _teamsService.Create(team1CreationDto);
			var team2 = _teamsService.Create(team2CreationDto);
			var tournament = _turnamentsService.GetById(tournamentId);

			await Task.WhenAll(team1, team2, tournament);

			var match = new Match();
			match.Team1 = _mapper.Map<Team>(team1.Result);
			match.Team2 = _mapper.Map<Team>(team2.Result);
			match.Tournament = tournament.Result != null? _mapper.Map<Tournament>(tournament.Result) : null;

			return match;
		}
	}
}