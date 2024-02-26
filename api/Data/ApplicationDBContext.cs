using Microsoft.EntityFrameworkCore;

using api.Models.Entities;


// https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many
namespace api.Data
{
	public class ApplicationDBContext : DbContext
	{
		public DbSet<Player> Players { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<Match> Matches { get; set; }
		public DbSet<Tournament> Tournaments { get; set; }
		public DbSet<Bet> Bets { get; set; }

		
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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			
			modelBuilder.Entity<Team>()
				.HasMany(t => t.Players)
				.WithMany();
		}
	}
}
