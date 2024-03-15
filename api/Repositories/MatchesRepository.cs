using Microsoft.EntityFrameworkCore;
using api.Models.Entities;
using api.Data;


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

		 
		public async Task<Match> CreateAsync(Match match)
		{
			using (var context = await contextFactory.CreateDbContextAsync())
			{
				/*var team1 = new Team()
				{
					Name = match.Team1.Name;
				}
				var team1 = (await context.Teams.AddAsync(match.Team1)).Entity;
				var team2 = (await context.Teams.AddAsync(match.Team2)).Entity;
				


				var players = team1.Players.Union(team2.Players);
				await context.Players.AddRangeAsync(players);
				context.Players.AttachRange(players);
				
				match.Team1 = team1;
				match.Team2 = team2;*/

				await context.Matches.AddAsync(match);
			}
			return match;
		}


		public async Task<Match?> GetAsync(int id)
			=> await allMatches
				.Include(m => m.Team1)
					.ThenInclude(t => t.Players)
				.Include(m => m.Team2)
					.ThenInclude(t => t.Players)
				.Include(m => m.Tournament)
				.FirstOrDefaultAsync(m => m.Id == id);


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