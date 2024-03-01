using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Models.Entities;


namespace api.Repositories
{
	public class PlayersRepository : BaseCRUDRepository<Player>
	{
		public PlayersRepository(ApplicationDBContext db) : base(db)
		{ }


		public ISet<Player> ReadMany(ISet<int> playerIds, bool track = false)
		{
			IQueryable<Player> query = dbSet;
			if (!track)
				query = dbSet.AsNoTracking();
			return query.Where(p => playerIds.Contains(p.Id)).ToHashSet();
		}
	}
}
