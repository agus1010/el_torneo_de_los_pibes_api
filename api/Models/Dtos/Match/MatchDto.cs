using api.Models.Dtos.Team;
using api.Models.Entities;


namespace api.Models.Dtos.Match
{
	public class MatchDto
	{
		public int Id { get; set; }

		public TeamDto Team1 { get; set; } = new();
		public TeamDto Team2 { get; set; } = new();

		public Tournament? Tournament { get; set; }

		public int ScoreTeam1 { get; set; } = 0;
		public int ScoreTeam2 { get; set; } = 0;

		public DateTime StartedAt { get; set; } = DateTime.MinValue;
		public DateTime FinishedAt { get; set; } = DateTime.MinValue;
	}
}
