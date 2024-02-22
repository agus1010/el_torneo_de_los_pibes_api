using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Models.Entities;
using api.General;


namespace api.Repositories
{
	public class PlayersRepository : IAsyncCRUD<Player>
	{
		protected readonly ApplicationDBContext _context;


		public PlayersRepository(ApplicationDBContext dbContext)
		{
			_context = dbContext;
		}


		public async Task<Player> Create(Player player)
		{
			_context.Players.Add(player);
			await Persist();
			return player;
		}


		public async Task Delete(Player player)
		{
			_context.Players.Remove(player);
			await Persist();
		}


		public async Task<IEnumerable<Player>> Get()
		{
			return await _context.Players.ToListAsync();
		}
		
		public async Task<Player?> Get(int id)
		{
			return await _context.Players.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
		}


		public async Task Update(Player player)
		{
			_context.Entry(player).State = EntityState.Modified;

			try
			{
				await Persist();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw;
			}
		}


		public async Task Persist()
		{
			await _context.SaveChangesAsync();
		}
	}
}
