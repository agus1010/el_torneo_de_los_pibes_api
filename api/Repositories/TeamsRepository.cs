using api.Data;
using api.Models.Entities;


namespace api.Repositories
{
	public class TeamsRepository : ITeamsRepository
	{
		protected readonly ApplicationDbContext context;

		public TeamsRepository(ApplicationDbContext context)
			=> this.context = context;


		public async Task<Team> CreateAsync(Team team)
		{
			throw new NotImplementedException();
		}


		public async Task<Team?> GetAsync(int id)
		{
			throw new NotImplementedException();
		}


		public async Task UpdateAsync(Team updatedTeam)
		{
			throw new NotImplementedException();
		}


		public async Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}


		public async Task EditTeamPlayers(Team team, IEnumerable<Player> playersRemoved, IEnumerable<Player> playersAdded)
		{
			throw new NotImplementedException()
		}
	}
}
