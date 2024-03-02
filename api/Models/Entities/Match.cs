using System.ComponentModel.DataAnnotations;


namespace api.Models.Entities
{
	public class Match
	{
		[Key] public int Id { get; set; }

		[Required] public Team Team1 { get; set; }
		[Required] public Team Team2 { get; set; }

		public Tournament? Tournament { get; set; }

		public int ScoreTeam1 { get; set; } = 0;
		public int ScoreTeam2 { get; set; } = 0;

		public DateTime StartedAt { get; set; } = DateTime.MinValue;
		public DateTime FinishedAt { get; set; } = DateTime.MinValue;
	}
}
