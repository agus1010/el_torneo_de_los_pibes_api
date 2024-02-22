using System.ComponentModel.DataAnnotations;


namespace api.Models.Entities
{
	public class TournamentsPlayersScores
	{
		[Key] public int Id { get; set; }
		public Player Player { get; set; }
		public int Points { get; set; } = 0;
		public int AssistedMatches { get; set; } = 0;
	}
}
