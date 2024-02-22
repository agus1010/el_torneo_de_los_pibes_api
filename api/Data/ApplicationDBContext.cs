using Microsoft.EntityFrameworkCore;

using api.Models;


namespace api.Data
{
	public class ApplicationDBContext : DbContext
	{
		protected readonly IConfiguration Configuration;


		public ApplicationDBContext(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// Connect to postgres with connection string from app settings
			optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
			base.OnConfiguring(optionsBuilder);
		}

		public DbSet<Player> Players { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<Match> Matches { get; set; }
		public DbSet<Tournament> Tournaments { get; set; }
		public DbSet<Bet> Bets { get; set; }
	}
}
