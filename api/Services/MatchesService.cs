using api.Models.Dtos.Match;
using api.Repositories;
using AutoMapper;

namespace api.Services
{
	public class MatchesService
	{
		protected readonly MatchesRepository matchesRepository;
		protected readonly PlayersService playersService;
		protected readonly IMapper mapper;

		public MatchesService(MatchesRepository matchesRepository, PlayersService playersService, IMapper mapper)
		{
			this.matchesRepository = matchesRepository;
			this.playersService = playersService;
			this.mapper = mapper;
		}

		public async Task<MatchDto> CreateAsync(PlayersOnlyFriendlyMatchCreationDto matchCreationDto)
		{
			throw new NotImplementedException();
		}

		
		public async Task<MatchDto?> GetAsync(int id)
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
