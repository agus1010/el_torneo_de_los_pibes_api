using api.Models.Dtos.Tournament;


namespace api.Services.Interfaces
{
	public interface ITournamentsService
	{
		Task<TournamentDto?> GetById(int id);
	}
}
