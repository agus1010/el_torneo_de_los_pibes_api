using AutoMapper;

using api.Models.Dtos.Player;
using api.Models.Entities;
using api.Repositories.Interfaces;
using api.Services.Interfaces;


namespace api.Services
{
    public class PlayersService : BaseService<Player, PlayerDto, PlayerCreationDto>, IPlayersService
	{
		public PlayersService(IBaseCRUDRepository<Player> repository, IMapper mapper) : base(repository, mapper)
		{ }


		public async Task<PlayerDto?> GetById(int id)
			=> (await Get(p => p.Id == id)).FirstOrDefault();


		public async Task<IEnumerable<PlayerDto>> GetAll()
			=> await Get();


		public async Task DeleteWithId(int id)
		{
			PlayerDto? target = await GetById(id);
			if (target == null)
				throw new Exception();
			await Delete(target);
		}
	}
}