using System.ComponentModel.DataAnnotations;


namespace api.Models
{
	public class TournamentStats
	{
		[Key] public int Id { get; set; }
		public Player Player { get; set; }
		public int Points { get; set; } = 0;
		public int AssistedMatches { get; set; } = 0;
	}
}
