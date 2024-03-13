using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Models.Entities;


// https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design
// https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/apply-simplified-microservice-cqrs-ddd-patterns
// https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/eshoponcontainers-cqrs-ddd-microservice
// https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/cqrs-microservice-reads
// https://deviq.com/domain-driven-design/aggregate-pattern
// https://medium.com/@philsarin/whats-the-point-of-the-aggregate-pattern-741a3132da5c
namespace api.Repositories
{
	public class MatchesRepository
	{
		protected IDbContextFactory<ApplicationDbContext> contextFactory;
		protected ApplicationDbContext qContext;

		protected IQueryable<Match> allMatches => qContext.Matches.AsNoTracking();


		public MatchesRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
		{
			this.contextFactory = contextFactory;
			qContext = contextFactory.CreateDbContext();
		}

		 
		public async Task<Match> CreateAsync(Match match, bool track = false)
		{
			using (var context = await contextFactory.CreateDbContextAsync())
				await context.Matches.AddAsync(match);
			//var newMatchEntity = await context.Matches.AddAsync(match);
			//await Persist();
			//if (!track)
			//	context.Entry(newMatchEntity).State = EntityState.Detached;
			return match;
		}


		public async Task<Match?> GetAsync(int id)
			=> await allMatches.FirstOrDefaultAsync(m => m.Id == id);


		public async Task<IEnumerable<Match>> GetTeamsMatchesAsync(int teamId)
		{
			throw new NotImplementedException();
		}


		public async Task UpdateAsync(Match match)
		{
			throw new NotImplementedException();
		}


		public async Task DeleteAsync(Match match)
		{
			throw new NotImplementedException();
		}
	}
}