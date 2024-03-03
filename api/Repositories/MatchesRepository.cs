using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Models.Entities;


namespace api.Repositories
{
//  https://stackoverflow.com/questions/26676563/entity-framework-queryable-async
	public class MatchesRepository
	{
		private readonly ApplicationDBContext _context;
		private readonly DbSet<Match> _matches;


        public MatchesRepository(ApplicationDBContext dbContext)
        {
			_context = dbContext;
			_matches = _context.Set<Match>();
		}


		public virtual async Task<Match?> GetById(int id)
			=> await _matches.FindAsync(id);


		public virtual async Task<IEnumerable<Match>> GetById(ISet<int> ids)
			=> await GetAll().Where(m => ids.Contains(m.Id)).ToListAsync();

		public IQueryable<Match> GetAll()
			=> _matches.AsQueryable();


		public virtual async Task<Match> Create(Match match)
		{
			_context.Matches.Add(match);
			await Persist();
			return match;
		}

		public virtual async Task Update(Match match)
		{
			_context.Entry(match).State = EntityState.Modified;
			await Persist();
		}

		public virtual async Task Delete(Match match)
		{
			_context.Matches.Remove(match);
			await Persist();
		}
		

		protected virtual async Task Persist()
			=> await _context.SaveChangesAsync();

		protected virtual void Detach(Match match)
			=> _context.Entry(match).State = EntityState.Detached;
	}
}
