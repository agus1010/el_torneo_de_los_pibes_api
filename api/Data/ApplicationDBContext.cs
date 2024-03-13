using Microsoft.EntityFrameworkCore;

using api.Models.Entities;


// https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many
namespace api.Data
{
	public class ApplicationDbContext : DbContext, IDisposable
	{
		public DbSet<Player> Players { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<Match> Matches { get; set; }
		public DbSet<Tournament> Tournaments { get; set; }
		public DbSet<Bet> Bets { get; set; }


		protected readonly IConfiguration Configuration;



        public ApplicationDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
			optionsBuilder.EnableSensitiveDataLogging(true);
			base.OnConfiguring(optionsBuilder);
		}


		public override void Dispose()
		{
			SaveChanges();
			base.Dispose();
		}


		public override ValueTask DisposeAsync()
		{
			Task.WhenAny(SaveChangesAsync());
			return base.DisposeAsync();
		}
	}
}
