using api.Data.Comamnds;
using api.Data.Queries;
using api.Models.Entities;
using api.Repositories.Interfaces;


namespace api.Repositories
{
	// https://learn.microsoft.com/en-us/ef/core/change-tracking/relationship-changes
	// https://learn.microsoft.com/en-us/ef/core/change-tracking/
	// https://learn.microsoft.com/en-us/ef/core/change-tracking/identity-resolution
	// https://stackoverflow.com/questions/48962213/updating-many-to-many-in-entity-framework-core
	public class TeamsRepository : ITeamsRepository
	{
		protected readonly TeamCommandsRunner teamCommands;
		protected readonly TeamQueriesRunner teamQueries;

		
		public TeamsRepository(TeamCommandsRunner teamCommands, TeamQueriesRunner teamQueries)
        {
			this.teamCommands = teamCommands;
			this.teamQueries = teamQueries;
		}


		// Queries
		public async Task<Team?> GetAsync(int id)
			=> await GetAsync(id, false);

		public async Task<Team?> GetAsync(int id, bool includePlayers = true)
			=> await teamQueries.GetAsync(id, includePlayers);

		

		// Commands
		public async Task<Team> CreateAsync(Team team)
			=> await teamCommands.CreateAsync(team);

		public async Task DeleteAsync(Team team)
			=> await teamCommands.DeleteAsync(team);

		public async Task UpdateAsync(Team updatedTeam)
			=> await teamCommands.UpdateAsync(updatedTeam);

		public async Task EditPlayers(Team team, ISet<Player> addedPlayers, ISet<Player> removedPlayers)
			=> await teamCommands.EditPlayers(team, addedPlayers, removedPlayers);
	}
}