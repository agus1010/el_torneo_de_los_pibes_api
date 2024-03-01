using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Models.Entities;


namespace api.Repositories
{
	public class TeamsRepository : BaseCRUDRepository<Team>
	{
		public TeamsRepository(ApplicationDBContext db) : base(db)
		{ }


		public async Task<Team?> GetById(int id)
			=> await _context.Teams.AsNoTracking().Include(t => t.Players).FirstOrDefaultAsync(t => t.Id == id);

		public async Task<ISet<Player>> GetPlayers(int teamId)
			=> (await GetById(teamId))?.Players!;


		public async override Task<Team> Create(Team team)
		{
			foreach (var p in team.Players)
			{
				_context.Players.Add(p);
				_context.Players.Attach(p);
			}
			await dbSet.AddAsync(team);
			await Persist();
			return team;
		}


		public async override Task Update(Team team)
		{
			_context.Teams.Entry(team).Collection(t => t.Players).IsModified = true;
			_context.Teams.Entry(team).State = EntityState.Modified;
			_context.Teams.Entry(team).DetectChanges();
			await Persist();
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

			foreach (var pId in playerIds)
			{
				var targetPlayer = await _context.Players.FirstOrDefaultAsync(p => p.Id == pId);
				if (targetPlayer == null)
					throw new Exception();
				_context.Players.Add(targetPlayer);
				_context.Players.Attach(targetPlayer);
				team.Players.Add(targetPlayer);
			}
			await Persist();
		}



		public async Task RemovePlayers(int teamId, ISet<int> playerIds)
		{
			var team = await _context.Teams
				.Include(t => t.Players)
				.FirstOrDefaultAsync(t => t.Id == teamId);

			if (team == null)
				throw new Exception();
			if (team.Players == null)
				team.Players = new HashSet<Player>();
			if (team.Players.Count == 0)
				throw new Exception();

			var playersToRemove = team.Players
				.Select(p => p.Id)
				.Intersect(playerIds)
				.Zip(
					team.Players,
					(id, p) => p.Id == id ? p : throw new Exception()
				)
				.Where(p => p != null)
				.ToList();

			foreach (var player in playersToRemove)
			{
				_context.Players.Add(player!);
				_context.Players.Attach(player!);
				team.Players.Remove(player!);
			}
			
			await Persist();
		}
	}
}
