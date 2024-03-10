using api.Data;
using api.Models.Entities;


namespace api.Repositories
{
	public class MatchesRepository
	{
		protected readonly ApplicationDbContext context;
		public MatchesRepository(ApplicationDbContext context)
		{
			this.context = context;
		}


		public async Task<Match> CreateAsync(Match match)
		{
			throw new NotImplementedException();
		}


		public async Task<Match?> GetAsync(int id)
		{
			throw new NotImplementedException();
		}


		public async Task<IEnumerable<Match>> GetTeamsMatchesAsync(int teamId)
		{
			throw new NotImplementedException();
		}


		public async Task UpdateAsync(Match match)
		{
			throw new NotImplementedException();
		}


		public async Task DeleteAsync(Match match)
		{
			throw new NotImplementedException();
		}
	}
}