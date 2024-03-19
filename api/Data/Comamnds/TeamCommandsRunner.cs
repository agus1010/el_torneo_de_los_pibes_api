using Microsoft.EntityFrameworkCore;

using api.Data.Queries;
using api.Models.Entities;


namespace api.Data.Comamnds
{
	public class TeamCommandsRunner : DBCommandRunner<Team>
	{
		protected TeamQueriesRunner teamsQueries;

		public TeamCommandsRunner(IDbContextFactory<ApplicationDbContext> contextFactory, TeamQueriesRunner teamsQueries) : base(contextFactory)
		{
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
			var originalTeam = await findInContext(context, team.Id);
			context.Entry(originalTeam).CurrentValues.SetValues(team);
			setPlayers(
				team: originalTeam,
				addedPlayers: playersDifference(team, originalTeam),
				removedPlayers: playersDifference(originalTeam, team)
			);
		}


		public override async Task UpdateAsync(ApplicationDbContext context, IEnumerable<Team> teams)
		{
			foreach (var team in teams)
				await UpdateAsync(context, team);
		}



		public async Task SetPlayers(Team team, IEnumerable<Player> finalPlayers)
			=>  await EditPlayers(
					team: team,
					playersToAdd: playersDifference(finalPlayers, team.Players),
					playersToRemove: playersDifference(team.Players, finalPlayers)
				);

		public async Task EditPlayers(Team team, IEnumerable<Player> playersToAdd, IEnumerable<Player> playersToRemove)
		{
			using (var context = await contextFactory.CreateDbContextAsync())
				await EditPlayers(context, team, playersToAdd, playersToRemove);
		}


		public async Task EditPlayers(ApplicationDbContext context, Team team, IEnumerable<Player> finalPlayers)
			=> await EditPlayers(
					context: context, 
					team: team, 
					playersToAdd: playersDifference(finalPlayers, team.Players), 
					playersToRemove: playersDifference(team.Players, finalPlayers)
				);

		public async Task EditPlayers(ApplicationDbContext context, Team team, IEnumerable<Player> playersToAdd, IEnumerable<Player> playersToRemove)
		{
			var trackedTeam = await findInContext(context, team.Id);
			setPlayers(trackedTeam, playersToAdd, playersToRemove);
			team.Players = trackedTeam.Players;
		}



		
		protected void setPlayers(Team team, IEnumerable<Player>? addedPlayers = null, IEnumerable<Player>? removedPlayers = null)
		{
			if (removedPlayers != null)
				removePlayersFromTeam(team, removedPlayers);

			if (addedPlayers != null)
				addPlayersToTeam(team, addedPlayers);
		}

		protected void addPlayersToTeam(Team trackedTeam, IEnumerable<Player> playersToAdd)
		{
			foreach (var player in playersToAdd)
				trackedTeam.Players.Add(player);
		}

		protected void removePlayersFromTeam(Team trackedTeam, IEnumerable<Player> playersToRemove)
		{
			var removedIds = playersToRemove.Select(player => player.Id);
			var removedInstances = trackedTeam.Players.Where(p => removedIds.Contains(p.Id));
			foreach (var player in removedInstances)
				trackedTeam.Players.Remove(player);
		}



		private async Task<Team> findInContext(ApplicationDbContext context, int id)
			=> (await teamsQueries.GetAsync(context, id, track: true, fetchPlayers: true, trackPlayers: true))!;

		private IEnumerable<Player> playersDifference(Team from, Team with)
			=> playersDifference(from.Players, with.Players);

		private IEnumerable<Player> playersDifference(IEnumerable<Player> from, IEnumerable<Player> with)
			=> from.ExceptBy(with.Select(p => p.Id), p => p.Id);
	}
}
