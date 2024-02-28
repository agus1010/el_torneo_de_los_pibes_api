using AutoMapper;

using api.Models.Dtos.Player;
using api.Models.Entities;
using api.Repositories.Interfaces;
using api.Services.Interfaces;


namespace api.Services
{
    public class PlayersService : BaseService<Player, PlayerDto>, IPlayersService
	{
		public PlayersService(IBaseCRUDRepository<Player> repository, IMapper mapper) : base(repository, mapper)
		{ }


		public async Task<PlayerDto> Create(PlayerCreationDto creationDto)
			 => await Create(_mapper.Map<PlayerDto>(creationDto));


		public async Task<PlayerDto?> GetById(int id)
			=> (await Get(p => p.Id == id, trackEntities: false)).FirstOrDefault();


		public async Task<IEnumerable<PlayerDto>> GetById(IEnumerable<int> ids)
		{
			var players = new List<PlayerDto>();
            foreach (var id in ids)
            {
				var player = await _repo.ReadSingle(p => p.Id == id, tracked: false);
				if (player != null)
					players.Add(_mapper.Map<PlayerDto>(player));
            }
			return players;
        }


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