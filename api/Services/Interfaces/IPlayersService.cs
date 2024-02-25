using api.Models.Dtos.Player;
using api.Services.Interfaces.Base;


namespace api.Services.Interfaces
{
    public interface IPlayersService : IIdDependantService<PlayerDto, PlayerCreationDto>
	{
		public Task<PlayerDto> Create(PlayerCreationDto playerCreationDto);
		public Task<IEnumerable<PlayerDto>> GetAll();
	}
}
