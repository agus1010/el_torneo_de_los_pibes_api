using System.ComponentModel.DataAnnotations;


namespace api.Models
{
	public class Tournament
	{
		[Key] public int Id { get; set; }

		public string Name { get; set; }
		public string Reward { get; set; }
		public DateTime StartedAt { get; set; }
		public DateTime FinishedAt { get; set; }

		public ICollection<Match> Matches { get; set; }
		public TournamentStats Stats { get; set; }
	}
}
