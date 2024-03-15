using Microsoft.EntityFrameworkCore;

using api.Data.Queries;
using api.Models.Entities;


namespace api.Data.Comamnds
{
	public class TeamCommandsRunner : DBCommandRunner<Team>
	{
		protected TeamQueriesRunner teamsQueries;
		protected DBQueryRunner<Player> playersQueries;

		public TeamCommandsRunner(IDbContextFactory<ApplicationDbContext> contextFactory, TeamQueriesRunner teamsQueries, DBQueryRunner<Player> playersQueries) : base(contextFactory)
		{
			this.playersQueries = playersQueries;
			this.teamsQueries = teamsQueries;
		}



		public override async Task<Team> CreateAsync(ApplicationDbContext context, Team team)
		{
			await context.Teams.AddAsync(team);
			// agrega los nuevos players que no existan
			await context.Players.AddRangeAsync(team.Players);
			context.Players.AttachRange(team.Players);
			return team;
		}

		public override async Task<IEnumerable<Team>> CreateAsync(ApplicationDbContext context, IEnumerable<Team> teams)
		{
			foreach (var team in teams)
				await CreateAsync(context, team);
			return teams;
        }



		public override async Task UpdateAsync(ApplicationDbContext context, Team team)
		{
			var originalTeam = (await teamsQueries.GetAsyncWithNavs(team.Id))!;
			await UpdateAsync
				(
					context,
					team,
					removedPlayers: playersDifference(originalTeam, team),
					addedPlayers: playersDifference(team, originalTeam)
				);
		}


		public async Task UpdateAsync(
			ApplicationDbContext context, 
			Team team,
			IEnumerable<Player>? addedPlayers = null,
			IEnumerable<Player>? removedPlayers = null
		)
		{
			var trackedTeam = (await context.Teams.FindAsync(team.Id))!;
			context.Entry(trackedTeam).CurrentValues.SetValues(team);
			
			if (removedPlayers != null)
				removePlayersFromTeam(trackedTeam, removedPlayers);

			if (addedPlayers != null)
				addPlayersToTeam(trackedTeam, addedPlayers);
		}


		public override async Task UpdateAsync(ApplicationDbContext context, IEnumerable<Team> teams)
		{
			foreach (var team in teams)
				await UpdateAsync(context, team);
		}


		protected void addPlayersToTeam(Team trackedTeam, IEnumerable<Player> playersToAdd)
			=> trackedTeam.Players.AddRange(playersToAdd);

		protected void removePlayersFromTeam(Team trackedTeam, IEnumerable<Player> playersToRemove)
		{
			foreach (var player in playersToRemove)
				trackedTeam.Players.Remove(player);
		}


		
		private IEnumerable<Player> playersDifference(Team from, Team with)
			=> playersDifference(from.Players, with.Players);

		private IEnumerable<Player> playersDifference(IEnumerable<Player> from, IEnumerable<Player> with)
			=> from.ExceptBy(with.Select(p => p.Id), p => p.Id);
	}
}
