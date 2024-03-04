using api.Models.Dtos.Team;


namespace api.Models.Dtos.Match
{
	public class PlayersOnlyTournamentMatchCreationDto
	{
		public TeamCreationDto Team1Dto { get; set; }
		public TeamCreationDto Team2Dto { get; set; }

		public int TournamentId { get; set; }
	}
}
