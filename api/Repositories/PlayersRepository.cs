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
			await _context.SaveChangesAsync();
			return player;
		}


		public async Task Delete(Player player)
		{
			_context.Players.Remove(player);
			await _context.SaveChangesAsync();
		}


		public async Task<IEnumerable<Player>> Get()
		{
			return await _context.Players.ToListAsync();
		}
		
		public async Task<Player?> Get(int id)
		{
			return await _context.Players.FindAsync(id);
		}


		public async Task Update(int id, Player player)
		{
			_context.Entry(player).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw;
			}
		}
	}
}
