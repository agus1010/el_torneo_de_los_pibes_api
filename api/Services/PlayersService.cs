using AutoMapper;

using api.Models.Dtos.Player;
using api.Models.Entities;
using api.Repositories.Interfaces;
using api.Services.Interfaces;


namespace api.Services
{
    public class PlayersService : IPlayersService
	{
		protected readonly IBaseCRUDRepository<Player> _repo;
		protected readonly IMapper _mapper;


		public PlayersService(IBaseCRUDRepository<Player> playersRepository, IMapper mapper)
		{
			_repo = playersRepository;
			_mapper = mapper;
		}


		public async Task<PlayerDto> Create(PlayerCreationDto playerCreationDto)
		{
			Player persistedPlayer = await _repo.Create(_mapper.Map<Player>(playerCreationDto));
			PlayerDto newPlayer = _mapper.Map<PlayerDto>(persistedPlayer);
			return newPlayer;
		}


		public async Task<PlayerDto?> GetById(int id)
		{
			Player target = await getWithId(id);
			return _mapper.Map<PlayerDto>(target);
		}


		public async Task<IEnumerable<PlayerDto>> GetAll()
		{
			return _mapper.Map<IEnumerable<PlayerDto>>(await _repo.ReadMany());
		}


		public async Task DeleteWithId(int id)
		{
			Player target = await getWithId(id);
			await _repo.Delete(target);
		}


		public async Task UpdateWith(PlayerDto playerDto)
		{
			await _repo.Update(_mapper.Map<Player>(playerDto));
		}



		private async Task<Player> getWithId(int id)
		{
			Player? player = await _repo.ReadSingle(filter: p => p.Id == id, tracked: false);
			if (player == null)
				throw new Exception();
			return player!;
		}
	}
}