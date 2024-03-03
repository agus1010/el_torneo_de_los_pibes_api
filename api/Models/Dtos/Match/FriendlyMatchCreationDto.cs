using api.Models.Dtos.Team;


namespace api.Models.Dtos.Match
{
	public class FriendlyMatchCreationDto
	{
		public TeamCreationDto Team1Dto { get; set; } = new();
		public TeamCreationDto Team2Dto { get; set; } = new();
	}
}
