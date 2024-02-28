using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Models.Entities;


namespace api.Repositories
{
	public class TeamsRepository : BaseCRUDRepository<Team>
	{
		public TeamsRepository(ApplicationDBContext db) : base(db)
		{ }


		public async override Task<Team> Create(Team team)
		{
			foreach (var p in team.Players)
				_context.Set<Player>().Entry(p).State = EntityState.Detached;
			await base.Create(team);
			return team;
		}


		public async Task AddPlayers(int teamId, ISet<int> playerIds)
		{
			var team = await _context.Teams
				.Include(t => t.Players)
				.FirstOrDefaultAsync(t => t.Id == teamId);
			
			if (team == null)
				throw new Exception();
			
			if (team.Players == null)
				team.Players = new HashSet<Player>();

			var players = new HashSet<Player>();
			foreach (var pId in playerIds)
			{
				var targetPlayer = await _context.Players.FirstOrDefaultAsync(p => p.Id == pId);
				if (targetPlayer == null)
					throw new Exception();
				players.Add(targetPlayer);
			}

			foreach (var p in players.ToList())
			{
				_context.Players.Add(p);
				_context.Players.Attach(p);
				team.Players.Add(p);
			}
			await Persist();
		}

		public async Task AddPlayers(Team team, ISet<Player> players)
		{
			foreach (var p in players.ToList())
			{
				_context.Players.Add(p);
				_context.Players.Attach(p);
				team.Players.Add(p);
			}
			await Persist();
		}


		public async Task RemovePlayers(Team team, ISet<Player> players)
		{
			var targetTeam = await _context.Teams.Include(t => t.Players)
				.FirstOrDefaultAsync(t => t.Id == team.Id);

			var selectedIds = players.Select(p => p.Id);

			var playersToDelete = targetTeam!.Players
				.Where(p => selectedIds.Contains(p.Id)).ToList();

			foreach (var player in playersToDelete)
				team.Players.Remove(player);
			await Persist();
		}
	}
}
