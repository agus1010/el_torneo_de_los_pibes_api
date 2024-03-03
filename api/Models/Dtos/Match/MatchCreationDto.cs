namespace api.Models.Dtos.Match
{
	public class MatchCreationDto
	{
		public int Team1Id { get; set; }
		public int Team2Id { get; set; }

		public int TournamentId { get; set; } = -1;
	}
}